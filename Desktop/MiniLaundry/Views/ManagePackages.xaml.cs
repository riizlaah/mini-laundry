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
    public partial class ManagePackages : UserControl
    {
        private DBHelper DBHelper;
        private bool editing = false;
        public ManagePackages(DBHelper DBH)
        {
            DBHelper = DBH;
            InitializeComponent();
            RefreshData();
            addServices.Visibility = Visibility.Collapsed;
            detailPanel.Visibility = Visibility.Collapsed;
            service.ItemsSource = DBH.Services.ToList();
        }
        private void toggleDetail(bool visib, bool resetInput = false, bool addServ = false)
        {
            detailPanel.Visibility = visib ? Visibility.Visible : Visibility.Collapsed;
            tambah.Visibility = visib ? Visibility.Collapsed : Visibility.Visible;
            addServices.Visibility = addServ ? Visibility.Visible : Visibility.Collapsed;
            pkgInputs.Visibility = addServ ? Visibility.Collapsed : Visibility.Visible;
            save.Visibility = addServ ? Visibility.Collapsed : Visibility.Visible;
            cancel.Visibility = addServ ? Visibility.Collapsed : Visibility.Visible;
            if (resetInput)
            {
                name.Text = "";
                description.Text = "";
                service.SelectedIndex = -1;
                qty.Text = "";
            }
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            editing = false;
            toggleDetail(true, true);
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            if (packageTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            editing = true;
            Package pkg = GetSelectedPackage();
            name.Text = pkg.Name;
            description.Text = pkg.Description;
            toggleDetail(true);
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            if(packageTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            Package pkg = GetSelectedPackage();
            MessageBoxResult res = MessageBox.Show($"Are you Sure Delete {pkg.Name}", "Yes No", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                DBHelper.Packages.Remove(pkg);
                DBHelper.SaveChanges();
                RefreshData();
            }
        }

        private Package GetSelectedPackage()
        {
            return packageTable.SelectedItem as Package;
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData(string? src = null)
        {
            if(src == null)
            {
                packageTable.ItemsSource = DBHelper.Packages.Include(e => e.detailPackages).ThenInclude(e => e.Service).AsSplitQuery().ToList();
            } else
            {
                packageTable.ItemsSource = DBHelper.Packages.Include(e => e.detailPackages).ThenInclude(e => e.Service).Where(e => 
                    EF.Functions.Like(e.Name, $"%{src}%")).AsSplitQuery().ToList();
            }
        }

        private void saveEmployee(object sender, RoutedEventArgs e)
        {
            if (name.Text.Length == 0)
            {
                MessageBox.Show("Nama tidak boleh kosong!");
                return;
            }
            if (description.Text.Length == 0)
            {
                MessageBox.Show("Deskripsi tidak boleh kosong!");
                return;
            }

            if (!editing)
            {
                DBHelper.Packages.Add(new Package
                {
                    Name = name.Text,
                    Description = description.Text,

                });
                DBHelper.SaveChanges();

            } else
            {
                var pkg = DBHelper.Packages.FirstOrDefault(e => e.Id == GetSelectedPackage().Id);
                if (pkg != null)
                {
                    pkg.Name = name.Text;
                    pkg.Description = description.Text;

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

        private void DeletePackageDetail(object sender, RoutedEventArgs e)
        {
            if (detailPackageTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            DetailPackage detailPkg0 = detailPackageTable.SelectedItem as DetailPackage;
            var detailPkg = DBHelper.DetailPackages.FirstOrDefault(e => e.Id == detailPkg0.Id);
            if (detailPkg != null)
            {
                DBHelper.DetailPackages.Remove(detailPkg);
                DBHelper.SaveChanges();
                int pkgId = GetSelectedPackage().Id;
                var pkg = DBHelper.Packages.Include(e => e.detailPackages).ThenInclude(e => e.Service).FirstOrDefault(e => e.Id == pkgId);
                if (pkg != null)
                {
                    pkg.Duration = 0;
                    pkg.Price = 0;
                    foreach (var detail in pkg.detailPackages)
                    {
                        pkg.Duration += detail.Service.EstimationDuration * detail.TotalUnitService;
                        pkg.Price += detail.Service.Price * detail.TotalUnitService;
                    }
                    DBHelper.SaveChanges();
                }
            }
            RefreshData();
            RefreshDetailPackages();
            toggleDetail(false, true);
        }

        private void addService(object sender, RoutedEventArgs e)
        {
            if(packageTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            if (service.SelectedItem == null)
            {
                MessageBox.Show("Layanan harus dipilih!");
                return;
            }
            if (qty.Text.Length == 0)
            {
                MessageBox.Show("Kuantitas tidak boleh kosong!");
                return;
            }
            if(int.Parse(qty.Text) == 0)
            {
                MessageBox.Show("Kuantitas tidak boleh kosong!");
                return;
            }
            int pkgId = GetSelectedPackage().Id;
            DBHelper.DetailPackages.Add(new DetailPackage
            {
                PackageId = pkgId,
                ServiceId = (int)service.SelectedValue,
                TotalUnitService = int.Parse(qty.Text),
            });
            DBHelper.SaveChanges();
            var pkg = DBHelper.Packages.Include(e => e.detailPackages).ThenInclude(e => e.Service).FirstOrDefault(e => e.Id == pkgId);
            if (pkg != null)
            {
                pkg.Duration = 0;
                pkg.Price = 0;
                foreach(var detail in pkg.detailPackages)
                {
                    pkg.Duration += detail.Service.EstimationDuration * detail.TotalUnitService;
                    pkg.Price += detail.Service.Price * detail.TotalUnitService;
                }
                DBHelper.SaveChanges();
            }
            RefreshData();
            RefreshDetailPackages();
            toggleDetail(false, true);
        }

        private void detailPackageSelectedCell(object sender, SelectedCellsChangedEventArgs e)
        {
        }

        private void pkgCellSelected(object sender, MouseButtonEventArgs e)
        {
            toggleDetail(true, false, true);
            RefreshDetailPackages();
        }
        private void RefreshDetailPackages()
        {
            detailPackageTable.ItemsSource = DBHelper.DetailPackages.Where(e => e.PackageId == GetSelectedPackage().Id).ToList();
        }
    }
}
