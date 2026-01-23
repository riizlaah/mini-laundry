using MiniLaundry.Models;
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
        public Dashboard(MainWindow mWindow, Employee cUser, DBHelper DBH)
        {
            _currUser = cUser;
            Debug.WriteLine(cUser.Job);
            _mainWindow = mWindow;
            mngEmployees = new ManageEmployees(DBH);
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
    }
}
