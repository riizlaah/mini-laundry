using Microsoft.EntityFrameworkCore;
using MiniLaundry.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
using testWPF.Migrations;

namespace testWPF.Views
{
    /// <summary>
    /// Interaction logic for ManageEmployees.xaml
    /// </summary>
    public partial class ManageServTransac : UserControl
    {
        private DBHelper DBHelper;
        private ObservableCollection<DetailTransaction> DetailTransacs { get; set; } = new ObservableCollection<DetailTransaction>();
        private List<Customer> Customers { get; set; }
        public ManageServTransac(DBHelper DBH)
        {
            DBHelper = DBH;
            InitializeComponent();
            RefreshData();
            serviceName.ItemsSource = DBH.Services.ToList();
            serviceTable.ItemsSource = DetailTransacs;
            Customers = DBH.Customers.ToList();
        }
        private void toggleDetail(bool visib, bool resetInput = false)
        {
            //detailPanel.Visibility = visib ? Visibility.Visible : Visibility.Collapsed;
            //tambah.Visibility = visib ? Visibility.Collapsed : Visibility.Visible;
            //if (resetInput)
            //{
            //    name.Text = "";
            //    category.SelectedIndex = -1;
            //    unit.SelectedIndex = -1;
            //    price.Text = "";
            //    duration.Text = "";
            //}
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            //editing = false;
            toggleDetail(true, true);
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            //if (serviceTable.SelectedItem == null)
            //{
            //    MessageBox.Show("Please, select One Row!");
            //    return;
            //}
            //editing = true;
            //Service serv = GetSelectedService();
            //name.Text = serv.Name;
            //category.SelectedValue = serv.CategoryId;
            //unit.SelectedValue = serv.UnitId;
            //price.Text = serv.Price.ToString();
            //duration.Text = serv.EstimationDuration.ToString();
            //toggleDetail(true);
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            if(serviceTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            Service serv = GetSelectedService();
            MessageBoxResult res = MessageBox.Show($"Are you Sure Delete {serv.Name}", "Yes No", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                DBHelper.Services.Remove(serv);
                DBHelper.SaveChanges();
                RefreshData();
            }
        }

        private Service GetSelectedService()
        {
            return serviceTable.SelectedItem as Service;
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData(string? src = null)
        {
            if(src == null)
            {
                serviceTable.ItemsSource = DBHelper.Services.Include(e => e.Unit).Include(e => e.Category).AsSplitQuery().ToList();
            } else
            {
                serviceTable.ItemsSource = DBHelper.Services.Include(e => e.Unit).Include(e => e.Category).Where(e => 
                    EF.Functions.Like(e.Name, $"%{src}%") || EF.Functions.Like(e.Category.Name, $"%{src}%") || 
                    EF.Functions.Like(e.Unit.Name, $"%{src}%") || Convert.ToString(e.Price).Contains(src)
                    ).AsSplitQuery().ToList();
            }
        }

        private void saveEmployee(object sender, RoutedEventArgs e)
        {
            //if (name.Text.Length == 0)
            //{
            //    MessageBox.Show("Nama tidak boleh kosong!");
            //    return;
            //}
            //if (!price.Text.All(Char.IsDigit))
            //{
            //    MessageBox.Show("Harga tidak valid!");
            //    return;
            //}
            //if (!duration.Text.All(Char.IsDigit))
            //{
            //    MessageBox.Show("Estimasi Durasi tidak valid!");
            //    return;
            //}
            //if (category.SelectedValue == null)
            //{
            //    MessageBox.Show("Layanan harus memiliki kategori!");
            //    return;
            //}
            //if (unit.SelectedValue == null)
            //{
            //    MessageBox.Show("Layanan harus memiliki unit!");
            //    return;
            //}
            //if (!editing)
            //{
            //    DBHelper.Services.Add(new Service
            //    {
            //        Name = name.Text,
            //        UnitId = (int)unit.SelectedValue,
            //        CategoryId = (int)unit.SelectedValue,
            //        Price = int.Parse(price.Text),
            //        EstimationDuration = int.Parse(duration.Text)
            //    });
            //    DBHelper.SaveChanges();

            //} else
            //{
            //    var serv = DBHelper.Services.FirstOrDefault(e => e.Id == GetSelectedService().Id);
            //    if (serv != null)
            //    {
            //        serv.Name = name.Text;
            //        serv.UnitId = (int)unit.SelectedValue;
            //        serv.CategoryId = (int)category.SelectedValue;
            //        serv.Price = int.Parse(price.Text);
            //        serv.EstimationDuration = int.Parse(duration.Text);

            //        DBHelper.SaveChanges();
            //    }
            //}
            //toggleDetail(false);
            //editing = false;
            //RefreshData();
        }

        private void hideDetail(object sender, RoutedEventArgs e)
        {
            toggleDetail(false);
        }

        private void TrySearch(object sender, TextChangedEventArgs e)
        {
            //RefreshData(search.Text);
        }

        private void AddNewUser(object sender, RoutedEventArgs e)
        {

        }

        private void save(object sender, RoutedEventArgs e)
        {
            
        }

        private void addService(object sender, RoutedEventArgs e)
        {
            if(serviceName.SelectedItem == null)
            {
                MessageBox.Show("Please, select a service");
                return;
            }
            if(!Regex.IsMatch(unit.Text, @"^\d+\.?\d*$"))
            {
                MessageBox.Show("The number of service is not valid");
                return;
            }
            float unitVal = float.Parse(unit.Text, CultureInfo.InvariantCulture);
            Debug.WriteLine(unitVal);
            if (unitVal < 1)
            {
                MessageBox.Show("The number of service is not valid");
                return;
            }
            DetailTransaction dtlTransac = new DetailTransaction
            {
                Service = serviceName.SelectedItem as Service,
                ServiceId = (int)serviceName.SelectedValue,
                TotalUnit = unitVal,
                Price = (int)((serviceName.SelectedItem as Service).Price * unitVal),
            };
            DetailTransacs.Add(dtlTransac);
        }

        private void delService(object sender, RoutedEventArgs e)
        {
            if (serviceTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            DetailTransacs.Remove(serviceTable.SelectedItem as DetailTransaction);
        }

        private void suggestOnKeyDown(object sender, KeyEventArgs e)
        {
            if (!suggestList.IsVisible) return;
            switch (e.Key)
            {
                case Key.Tab:
                case Key.Enter:
                    ApplySuggestion();
                    e.Handled = true; break;
                case Key.Up:
                    if(suggestList.SelectedIndex == 0)
                    {
                        phoneNum.Focus();
                        e.Handled = true;
                    }
                    break;
                case Key.Escape:
                    HideSuggestions();
                    phoneNum.Focus();
                    e.Handled = true;
                    break;
            }
        }

        private void suggestOnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ApplySuggestion();
        }

        private void phoneNumOnChanged(object sender, TextChangedEventArgs e)
        {
            string input = phoneNum.Text.Trim();
            if(string.IsNullOrWhiteSpace(input))
            {
                HideSuggestions();
                return;
            }
            var suggestions = Customers.Where(e => e.PhoneNum.Contains(input)).ToList();
            ShowSuggestions(suggestions);
        }

        private void ShowSuggestions(List<Customer> items)
        {
            if(items.Any())
            {
                suggestList.ItemsSource = items;
                suggestList.Visibility = Visibility.Visible;
                suggestList.SelectedIndex = 0;
            } else
            {
                HideSuggestions();
            }
        }

        private void HideSuggestions()
        {
            suggestList.Visibility = Visibility.Collapsed;
        }

        private void phoneNumPrevKeyDown(object sender, KeyEventArgs e)
        {
            if (!suggestList.IsVisible) return;
            switch (e.Key)
            {
                case Key.Down:
                    suggestList.Focus();
                    suggestList.SelectedIndex = 0;
                    e.Handled = true; break;
                case Key.Tab:
                case Key.Enter:
                    ApplySuggestion();
                    e.Handled = true; break;
            }
        }

        private void ApplySuggestion()
        {
            if(suggestList.SelectedItem != null)
            {
                Customer cust = suggestList.SelectedItem as Customer;
                phoneNum.Text = cust.PhoneNum;
                custName.Text = cust.Name;
                address.Text = cust.Address;
                phoneNum.CaretIndex = phoneNum.Text.Length;
                HideSuggestions();
                phoneNum.Focus();
            }

        }
    }
}
