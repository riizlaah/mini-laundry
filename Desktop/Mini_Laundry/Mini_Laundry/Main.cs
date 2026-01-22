using Mini_Laundry.Helper;
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
            DataTable user = DataConnection.getData($"SELECT * FROM employees WHERE email = '{textBox1.Text}'");
            if (user.Rows.Count == 0)
            {
                MessageBox.Show("Akun tidak ditemukan!");
                return;
            }
            if (user.Rows[0]["password"].ToString() != textBox2.Text)
            {
                MessageBox.Show("Password salah!");
                return;
            }
            DataConnection.currUser = user.Rows[0];
            Dashboard dashboard = new Dashboard(this);
            this.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            dashboard.Show();
        }
    }
}
