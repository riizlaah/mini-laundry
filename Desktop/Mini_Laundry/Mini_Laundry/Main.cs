using Mini_Laundry.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Laundry
{
    public partial class Main : Form
    {
        private string email = "admin@hekerd.org";
        private string password = "password";
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1 == null) return;
            if (textBox2 == null) return;
            if (textBox1.Text != this.email)
            {
                MessageBox.Show("Email salah.");
                return;
            }
            if (textBox2.Text != this.password)
            {
                MessageBox.Show("Password salah.");
                return;
            }
            Dashboard dashboard = new Dashboard(this);
            this.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            dashboard.Show();
        }
    }
}
