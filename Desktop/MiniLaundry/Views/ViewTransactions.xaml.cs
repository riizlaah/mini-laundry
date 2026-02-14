using Microsoft.EntityFrameworkCore;
using MiniLaundry.Models;
using System;
using System.Collections.Generic;
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

namespace MiniLaundry.Views
{
    /// <summary>
    /// Interaction logic for ViewTransactions.xaml
    /// </summary>
    public partial class ViewTransactions : UserControl
    {
        DBHelper DBHelper;
        public ViewTransactions(DBHelper DBH)
        {
            DBHelper = DBH;
            InitializeComponent();
            RefreshData();
        }

        public void RefreshData(string? src = null)
        {
            if(src == null)
            {
                headerTransacsTable.ItemsSource = DBHelper.HeaderTransactions.Include(h => h.Customer).Include(h => h.Employee).AsSplitQuery().ToList();
            } else
            {
                headerTransacsTable.ItemsSource = DBHelper.HeaderTransactions.Include(h => h.Customer).Include(h => h.Employee)
                    .Where(h => EF.Functions.Like(h.Customer.Name, $"%{src}%") || EF.Functions.Like(h.Employee.Name, $"%{src}%")).AsSplitQuery().ToList();
            }
        }

        private void CompleteDetailTransac(object sender, RoutedEventArgs e)
        {
            if(detailTransacsTable.SelectedItem == null)
            {
                MessageBox.Show("Please, select One Row!");
                return;
            }
            DetailTransacV2 dtlTransacView = detailTransacsTable.SelectedItem as DetailTransacV2;
            DetailTransaction dtlTransac = DBHelper.DetailTransactions.FirstOrDefault(d => d.Id == dtlTransacView.Id);
            Debug.WriteLine(dtlTransac.Id);
            if (dtlTransac != null)
            {
                dtlTransac.CompletedAt = DateTime.Now;
                DBHelper.SaveChanges();
                LoadDetailTransacs();
            }
        }

        private HeaderTransaction GetSelectedHeaderTransac()
        {
            return headerTransacsTable.SelectedItem as HeaderTransaction;
        }

        private void headerTransacSelected(object sender, MouseButtonEventArgs e)
        {
            LoadDetailTransacs();
        }
        private void LoadDetailTransacs()
        {
            List<DetailTransaction> dtlTransacs = DBHelper.DetailTransactions.Include(d => d.Package).Include(d => d.Service)
                .Where(d => d.HeaderTransactionId == GetSelectedHeaderTransac().Id).ToList();
            List<DetailTransacV2> dtlTransacs2 = new List<DetailTransacV2>();
            foreach (DetailTransaction t in dtlTransacs)
            {
                if(t.ServiceId != null)
                {
                    dtlTransacs2.Add(new DetailTransacV2 { Id = t.Id, Name = t.Service.Name, Type = "Layanan", Price = t.Service.Price, TotalUnit = t.TotalUnit, CompletedAt = t.CompletedAt});
                } else
                {
                    dtlTransacs2.Add(new DetailTransacV2 { Id = t.Id, Name = t.Package.Name, Type = "Paket", Price = t.Package.Price, TotalUnit = t.TotalUnit, CompletedAt = t.CompletedAt});
                }
            }
            detailTransacsTable.ItemsSource = dtlTransacs2;
        }

        private void TrySearch(object sender, TextChangedEventArgs e)
        {
            RefreshData(search.Text);
        }
    }
    public partial class DetailTransacV2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public float TotalUnit { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
