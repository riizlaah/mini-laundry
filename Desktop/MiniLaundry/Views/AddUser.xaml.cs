using MiniLaundry.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MiniLaundry.Views
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private DBHelper DBHelper;
        public AddUser(DBHelper DBH)
        {
            this.DBHelper = DBH;
            InitializeComponent();
        }

        private void save(object sender, RoutedEventArgs e)
        {
            if(name.Text.Length == 0)
            {
                MessageBox.Show("Nama tidak boleh kosong!");
                return;
            }
            if(!Regex.IsMatch(phoneNum.Text, @"^\+\d+$"))
            {
                MessageBox.Show("No. HP tidak boleh kosong!");
                return;
            }
            if(address.Text.Length == 0)
            {
                MessageBox.Show("Alamat tidak boleh kosong!");
                return;
            }
            DBHelper.Customers.Add(new Customer()
            {
                Name = name.Text,
                PhoneNum = phoneNum.Text,
                Address = address.Text,
            });
            DBHelper.SaveChanges();
            DialogResult = true;
            Close();
        }
    }
}
