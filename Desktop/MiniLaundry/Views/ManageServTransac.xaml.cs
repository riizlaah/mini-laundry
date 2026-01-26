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
    public partial class ManageServTransac : UserControl
    {
        private DBHelper DBHelper;
        private List<DetailTransaction> DetailTransacs { get; set; } = new List<DetailTransaction>();
        public ManageServTransac(DBHelper DBH)
        {
            DBHelper = DBH;
            InitializeComponent();
            RefreshData();
            serviceName.ItemsSource = DBH.Services.ToList();
            serviceTable.ItemsSource = DetailTransacs;

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
            float unitVal = float.Parse(unit.Text);
            if (unitVal < 1)
            {
                MessageBox.Show("The number of service is not valid");
                return;
            }
            DetailTransaction dtlTransac = new DetailTransaction
            {
                ServiceId = (int)serviceName.SelectedValue,
                TotalUnit = float.Parse(unit.Text),
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
    }
}
