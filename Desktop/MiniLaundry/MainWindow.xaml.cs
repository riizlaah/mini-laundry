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
            if (email.Text.Length == 0)
            {
                MessageBox.Show("Email tidak boleh kosong!");
                return;
            }
            if (password.Password.Length == 0)
            {
                MessageBox.Show("Password tidak boleh kosong!");
                return;
            }
            List<Employee> emps = _context.Employees.Where(em => em.Email == email.Text).ToList();
            if (emps.Count == 0)
            {
                MessageBox.Show("Akun tidak ada!");
                return;
            }
            Employee emp = emps[0];
            if(emp.Password != password.Password)
            {
                MessageBox.Show("Password salah!");
                return;
            }
            email.Text = "";
            password.Password = "";
            Dashboard dashb = new Dashboard(this, emp, _context);
            this.Hide();
            dashb.Show();
        }
    }
}