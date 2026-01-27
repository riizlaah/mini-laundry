using Microsoft.EntityFrameworkCore;
using MiniLaundry.Models;
using MiniLaundry.Views;
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
    public partial class ManagePkgTransac : UserControl
    {
        private DBHelper DBHelper;
        private ObservableCollection<DetailTransaction> DetailTransacs { get; set; } = new ObservableCollection<DetailTransaction>();
        private List<Customer> Customers { get; set; }
        private int custId = -1;
        public Employee currUser = null!;
        public ManagePkgTransac(DBHelper DBH)
        {
            DBHelper = DBH;
            InitializeComponent();
            pkgName.ItemsSource = DBH.Packages.ToList();
            serviceTable.ItemsSource = DetailTransacs;
            Customers = DBH.Customers.ToList();
        }

        private void updateDetail()
        {
            int totalPrice = DetailTransacs.Sum(d => d.Price);
            estPrice.Content = string.Format("{0:Rp#,##0;(Rp#,##0);''}", totalPrice);
            int totalTime = Convert.ToInt32(DetailTransacs.Sum(d => d.Package.Duration * d.TotalUnit));
            estTime.Content = $"{totalTime} Jam";
        }

        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            AddUser addUserDialog = new AddUser(DBHelper);
            bool? res = addUserDialog.ShowDialog();
            if(res == true)
            {
                Customers = DBHelper.Customers.ToList();
                Customer newCust = Customers.Last();
                suggestList.ItemsSource = Customers;
                suggestList.SelectedIndex = Customers.Count - 1;
                ApplySuggestion();
            }
        }

        private void save(object sender, RoutedEventArgs e)
        {
            if(phoneNum.Text.Length == 0 || address.Text.Length == 0 || custName.Text.Length == 0)
            {
                MessageBox.Show("Please, Fill Customer Section!");
                return;
            }
            if(DetailTransacs.Count < 1)
            {
                MessageBox.Show("Paket tidak boleh kosong!");
                return;
            }
            int totalHours = Convert.ToInt32(DetailTransacs.Sum(d => d.Package.Duration * d.TotalUnit));
            HeaderTransaction hdrTransac = new HeaderTransaction
            {
                CustomerId = custId,
                CreatedAt = DateTime.Now,
                EmployeeId = currUser.Id,
                CompleteEstDate = DateTime.Now.AddHours(totalHours)
            };
            DBHelper.HeaderTransactions.Add(hdrTransac);
            DBHelper.SaveChanges();
            foreach (DetailTransaction dtlTransac in DetailTransacs)
            {
                dtlTransac.HeaderTransactionId = hdrTransac.Id;
                DBHelper.DetailTransactions.Add(dtlTransac);
            }
            DBHelper.SaveChanges();
            MessageBox.Show("Transaction Added!");
            serviceTable.ItemsSource = null;
            DetailTransacs.Clear();
            phoneNum.Text = "";
            address.Text = "";
            custName.Text = "";
            unit.Text = "";
            pkgName.SelectedIndex = -1;
            estPrice.Content = "Rp0";
            estTime.Content = "0 Jam";
        }

        private void addService(object sender, RoutedEventArgs e)
        {
            if(pkgName.SelectedItem == null)
            {
                MessageBox.Show("Please, select a service");
                return;
            }
            if(!unit.Text.All(Char.IsDigit))
            {
                MessageBox.Show("The quantity of package is not valid");
                return;
            }
            float unitVal = int.Parse(unit.Text);
            Debug.WriteLine(unitVal);
            if (unitVal < 1)
            {
                MessageBox.Show("The quantity of package is not valid");
                return;
            }
            DetailTransaction dtlTransac = new DetailTransaction
            {
                Package = pkgName.SelectedItem as Package,
                PackageId = (int)pkgName.SelectedValue,
                TotalUnit = unitVal,
                Price = (int)((pkgName.SelectedItem as Package).Price * unitVal),
            };
            DetailTransacs.Add(dtlTransac);
            updateDetail();
        }

        private void delService(object sender, RoutedEventArgs e)
        {
            if (serviceTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            MessageBoxResult res = MessageBox.Show("Yakin?", "Yes No", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                bool r2 = DetailTransacs.Remove(serviceTable.SelectedItem as DetailTransaction);
                if(r2) updateDetail();
            }
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
                custId = cust.Id;
            }

        }
    }
}
