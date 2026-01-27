using MiniLaundry.Models;
using MiniLaundry.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace testWPF.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        DispatcherTimer timer;
        MainWindow _mainWindow;
        bool _logout = false;
        Employee _currUser;
        ManageEmployees mngEmployees;
        ManageServices mngService;
        ManageCustomers mngCustomers;
        ManagePackages mngPackages;
        ManageServTransac mngServTransac;
        ManagePkgTransac mngPkgTransac;
        ViewTransactions viewTransactions;
        public Dashboard(MainWindow mWindow, Employee cUser, DBHelper DBH)
        {
            _currUser = cUser;
            Debug.WriteLine(cUser.Job);
            _mainWindow = mWindow;
            mngEmployees = new ManageEmployees(DBH);
            mngService = new ManageServices(DBH);
            mngCustomers = new ManageCustomers(DBH);
            mngPackages = new ManagePackages(DBH);
            mngServTransac = new ManageServTransac(DBH);
            mngPkgTransac = new ManagePkgTransac(DBH);
            viewTransactions = new ViewTransactions(DBH);
            mngPkgTransac.currUser = _currUser;
            mngServTransac.currUser = _currUser;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += clockTick;
            InitializeComponent();
            timer.Start();
            clockTick(null, null);
            greeter.Text = $"Halo {_currUser.Name}!";
        }

        private void clockTick(object sender, EventArgs e)
        {
            timeLb.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            _logout = true;
            this.Close();
            _mainWindow.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (!_logout)
            {
                _mainWindow.Close();
            }
        }

        private void manageEmployeeClicked(object sender, RoutedEventArgs e)
        {
            content1.Content = mngEmployees;
        }

        private void emptyMenu(object sender, MouseButtonEventArgs e)
        {
            content1.Content = null;
        }

        private void manageServiceClicked(object sender, RoutedEventArgs e)
        {
            content1.Content = mngService;
        }

        private void manageCustomersClicked(object sender, RoutedEventArgs e)
        {
            content1.Content = mngCustomers;
        }

        private void managePackagesClicked(object sender, RoutedEventArgs e)
        {
            content1.Content = mngPackages;
        }

        private void manageServiceTransactionClicked(object sender, RoutedEventArgs e)
        {
            content1.Content = mngServTransac;
        }

        private void managePackageTransactionClicked(object sender, RoutedEventArgs e)
        {
            content1.Content = mngPkgTransac;
        }

        private void viewTransactionClicked(object sender, RoutedEventArgs e)
        {
            content1.Content = viewTransactions;
            viewTransactions.RefreshData();
        }
    }
}
