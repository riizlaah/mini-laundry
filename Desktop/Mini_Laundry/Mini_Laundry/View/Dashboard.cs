using Mini_Laundry.Helper;
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
    public partial class Dashboard : Form
    {
        Main mainForm;
        MainDashboard mainDashboard;
        public Dashboard(Main main)
        {
            this.mainForm = main;
            this.mainDashboard = new MainDashboard();
            InitializeComponent();
            DataTable data = DataConnection.getData("SELECT * FROM employee");
            DataRow first = data.Rows[0];
            this.mainDashboard.sayHello(first["name"].ToString());
            panel1.Controls.Add(this.mainDashboard);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.mainForm.Show();
            this.Close();
        }
    }
}
