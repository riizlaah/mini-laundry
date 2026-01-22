using Mini_Laundry.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Laundry.View
{
    public partial class AddEmployeeForm : Form
    {
        private ManageEmployee mngEmployee;
        public AddEmployeeForm(ManageEmployee mngEmployee)
        {
            InitializeComponent();
            this.mngEmployee = mngEmployee;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nama.Text.Length == 0)
            {
                MessageBox.Show("Nama tidak boleh kosong!");
                return;
            }
            if (!email.Text.Contains('@'))
            {
                MessageBox.Show("Email tidak valid!");
                return;
            }
            if (password.Text.Length == 0)
            {
                MessageBox.Show("Password tidak boleh kosong!");
                return;
            }
            if (tglLahir.Value == null)
            {
                MessageBox.Show("Tanggal lahir tidak boleh kosong!");
                return;
            }
            if (pekerjaan.Text.Length == 0)
            {
                MessageBox.Show("Pekerjaan tidak boleh kosong!");
                return;
            }
            if (alamat.Text.Length == 0)
            {
                MessageBox.Show("Alamat tidak boleh kosong!");
                return;
            }
            if (gaji.Text.Length == 0)
            {
                MessageBox.Show("Gaji tidak boleh kosong!");
                return;
            }
            if (!gaji.Text.All(char.IsDigit))
            {
                MessageBox.Show("Gaji harus berupa angka!");
                return;
            }
            Employee.create(nama.Text, email.Text, password.Text, tglLahir.Value.ToString("yyyy-MM-dd"), pekerjaan.Text, alamat.Text, int.Parse(gaji.Text));
            mngEmployee.refreshData();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
