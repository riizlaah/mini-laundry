using MiniLaundry.Models;
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
using testWPF.Views;

namespace testWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DBHelper _context = new DBHelper();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            if (email.Text.Length == 0 || password.Password.Length == 0)
            {
                MessageBox.Show("Please Try Again, Your Data is Not Valid!");
                return;
            }
            List<Employee> emps = _context.Employees.Where(em => em.Email == email.Text && em.Password == password.Password).ToList();
            if (emps.Count == 0)
            {
                MessageBox.Show("Please Try Again, Your Data is Not Valid!");
                return;
            }
            Employee emp = emps[0];
            email.Text = "";
            password.Password = "";
            Dashboard dashb = new Dashboard(this, emp, _context);
            Hide();
            dashb.Show();
        }
    }
}