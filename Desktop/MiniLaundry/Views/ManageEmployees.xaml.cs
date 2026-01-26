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
    public partial class ManageEmployees : UserControl
    {
        private DBHelper DBHelper;
        private bool editing = false;
        public ManageEmployees(DBHelper DBH)
        {
            DBHelper = DBH;
            InitializeComponent();
            RefreshData();
            detailPanel.Visibility = Visibility.Collapsed;
            pekerjaan.ItemsSource = DBH.Jobs.ToList();
        }
        private void toggleDetail(bool visib, bool resetInput = false)
        {
            detailPanel.Visibility = visib ? Visibility.Visible : Visibility.Collapsed;
            tambah.Visibility = visib ? Visibility.Collapsed : Visibility.Visible;
            if (resetInput)
            {
                nama.Text = "";
                email.Text = "";
                password.Text = "";
                password2.Text = "";
                tgl_lahir.SelectedDate = DateTime.Now;
                no_hp.Text = "";
                gaji.Text = "";
                pekerjaan.SelectedIndex = -1;
                alamat.Text = "";
            }
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            editing = false;
            toggleDetail(true, true);
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            if (employeeTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            editing = true;
            Employee emp = GetSelectedEmp();
            nama.Text = emp.Name;
            email.Text = emp.Email;
            password.Text = emp.Password;
            password2.Text = emp.Password;
            tgl_lahir.SelectedDate = emp.DateOfBirth;
            no_hp.Text = emp.PhoneNum;
            gaji.Text = emp.Salary.ToString();
            pekerjaan.SelectedValue = emp.Job.Id;
            alamat.Text = emp.Address;
            toggleDetail(true);
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            if(employeeTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            Employee emp = GetSelectedEmp();
            MessageBoxResult res = MessageBox.Show($"Are you Sure Delete {emp.Name}", "Yes No", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                DBHelper.Employees.Remove(emp);
                DBHelper.SaveChanges();
                RefreshData();
            }
        }

        private Employee GetSelectedEmp()
        {
            return employeeTable.SelectedItem as Employee;
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData(string? src = null)
        {
            if(src == null)
            {
                employeeTable.ItemsSource = DBHelper.Employees.Include(e => e.Job).AsSplitQuery().ToList();
            } else
            {
                employeeTable.ItemsSource = DBHelper.Employees.Where(e => 
                    EF.Functions.Like(e.Name, $"%{src}%") || EF.Functions.Like(e.Email, $"%{src}%") || 
                    EF.Functions.Like(e.PhoneNum, $"%{src}%")
                    ).Include(e => e.Job).AsSplitQuery().ToList();
            }
        }

        private void saveEmployee(object sender, RoutedEventArgs e)
        {
            if (nama.Text.Length == 0)
            {
                MessageBox.Show("Nama tidak boleh kosong!");
                return;
            }
            if (!email.Text.Contains("@"))
            {
                MessageBox.Show("Email tidak valid!");
                return;
            }
            if (!password.Text.Any(Char.IsDigit) || !password.Text.Any(Char.IsLetter)
                ||  !password.Text.Any(c => !Char.IsLetterOrDigit(c)))
            {
                MessageBox.Show("Password harus memiliki angka, huruf dan simbol!");
                return;
            }
            if (password.Text != password2.Text)
            {
                MessageBox.Show("Password tidak sama!");
                return;
            }
            if (!Regex.IsMatch(no_hp.Text, @"^\+\d+$"))
            {
                MessageBox.Show("No. HP tidak valid!");
                return;
            }
            if (pekerjaan.SelectedValue == null)
            {
                MessageBox.Show("Karyawan harus memiliki pekerjaan!");
                return;
            }
            if (tgl_lahir.SelectedDate == null)
            {
                MessageBox.Show("Nama tidak boleh kosong!");
                return;
            }
            if (!gaji.Text.All(Char.IsDigit))
            {
                MessageBox.Show("Gaji harus berupa angka!");
                return;
            }
            if(alamat.Text.Length == 0)
            {
                MessageBox.Show("Alamat tidak boleh kosong!");
                return;
            }
            if (!editing)
            {
                Debug.WriteLine("add");
                DBHelper.Employees.Add(new Employee
                {
                    Name = nama.Text,
                    Address = alamat.Text,
                    DateOfBirth = tgl_lahir.SelectedDate ?? new DateTime(2000, 1, 1),
                    Email = email.Text,
                    Password = password.Text,
                    PhoneNum = no_hp.Text,
                    Salary = int.Parse(gaji.Text),
                    JobId = int.Parse(pekerjaan.SelectedValue.ToString())// ?? "2")
                });
                DBHelper.SaveChanges();

            } else
            {
                Debug.WriteLine("update");
                var emp = DBHelper.Employees.FirstOrDefault(e => e.Id == GetSelectedEmp().Id);
                if (emp != null)
                {
                    emp.Name = nama.Text;
                    emp.Address = alamat.Text;
                    emp.DateOfBirth = tgl_lahir.SelectedDate ?? new DateTime(2000, 1, 1);
                    emp.Email = email.Text;
                    emp.Password = password.Text;
                    emp.PhoneNum = no_hp.Text;
                    emp.Salary = int.Parse(gaji.Text);
                    emp.JobId = int.Parse(pekerjaan.SelectedValue.ToString());

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
