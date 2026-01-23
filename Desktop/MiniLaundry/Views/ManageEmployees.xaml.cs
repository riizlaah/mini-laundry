using Microsoft.EntityFrameworkCore;
using MiniLaundry.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
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
    public partial class ManageEmployees : UserControl
    {
        private DBHelper DBHelper;
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ManageEmployees(DBHelper DBH)
        {
            DBHelper = DBH;
            var _l = DBH.Employees.Include(e => e.Job).AsSplitQuery().ToList();
            Employees = DBHelper.Employees.Local.ToObservableCollection();
            InitializeComponent();
            employeeTable.ItemsSource = Employees;
            Debug.WriteLine(Employees[0].Job);
        }
    }
}
