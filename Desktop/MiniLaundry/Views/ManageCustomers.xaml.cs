using Microsoft.EntityFrameworkCore;
using MiniLaundry.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testWPF.Views
{
    /// <summary>
    /// Interaction logic for ManageEmployees.xaml
    /// </summary>
    public partial class ManageCustomers : UserControl
    {
        private DBHelper DBHelper;
        private bool editing = false;
        public ManageCustomers(DBHelper DBH)
        {
            DBHelper = DBH;
            InitializeComponent();
            RefreshData();
            detailPanel.Visibility = Visibility.Collapsed;
        }
        private void toggleDetail(bool visib, bool resetInput = false)
        {
            detailPanel.Visibility = visib ? Visibility.Visible : Visibility.Collapsed;
            tambah.Visibility = visib ? Visibility.Collapsed : Visibility.Visible;
            if (resetInput)
            {
                name.Text = "";
                phoneNum.Text = "";
                address.Text = "";
            }
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            editing = false;
            toggleDetail(true, true);
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            if (customerTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            editing = true;
            Customer cust = GetSelectedCustomer();
            name.Text = cust.Name;
            phoneNum.Text = cust.PhoneNum;
            address.Text = cust.Address;
            toggleDetail(true);
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            if(customerTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            Customer cust = GetSelectedCustomer();
            MessageBoxResult res = MessageBox.Show($"Are you Sure Delete {cust.Name}", "Yes No", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                DBHelper.Customers.Remove(cust);
                DBHelper.SaveChanges();
                RefreshData();
            }
        }

        private Customer GetSelectedCustomer()
        {
            return customerTable.SelectedItem as Customer;
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData(string? src = null)
        {
            if(src == null)
            {
                customerTable.ItemsSource = DBHelper.Customers.AsSplitQuery().ToList();
            } else
            {
                customerTable.ItemsSource = DBHelper.Customers.Where(e => 
                    EF.Functions.Like(e.Name, $"%{src}%") || EF.Functions.Like(e.Address, $"%{src}%") || 
                    EF.Functions.Like(e.PhoneNum, $"%{src}%")
                    ).AsSplitQuery().ToList();
            }
        }

        private void saveEmployee(object sender, RoutedEventArgs e)
        {
            if (name.Text.Length == 0)
            {
                MessageBox.Show("Nama tidak boleh kosong!");
                return;
            }
            if (address.Text.Length == 0)
            {
                MessageBox.Show("Alamat tidak boleh kosong!");
                return;
            }
            if (!Regex.IsMatch(phoneNum.Text, @"^\+\d+$"))
            {
                MessageBox.Show("No. HP tidak valid!");
                return;
            }
            if (!editing)
            {
                DBHelper.Customers.Add(new Customer
                {
                    Name = name.Text,
                    Address = address.Text,
                    PhoneNum = phoneNum.Text,
                });
                DBHelper.SaveChanges();

            } else
            {
                var cust = DBHelper.Customers.FirstOrDefault(e => e.Id == GetSelectedCustomer().Id);
                if (cust != null)
                {
                    cust.Name = name.Text;
                    cust.Address = address.Text;
                    cust.PhoneNum = phoneNum.Text;

                    DBHelper.SaveChanges();
                }
            }
            toggleDetail(false);
            editing = false;
            RefreshData();
        }

        private void hideDetail(object sender, RoutedEventArgs e)
        {
            toggleDetail(false);
        }

        private void TrySearch(object sender, TextChangedEventArgs e)
        {
            RefreshData(search.Text);
        }
    }
}
