using System;
using System.Collections.Generic;
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
using testWPF.Models;

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
        public Dashboard(MainWindow mWindow, Employee cUser)
        {
            _currUser = cUser;
            _mainWindow = mWindow;
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
            this.Close();
            _mainWindow.Show();
        }
    }
}
