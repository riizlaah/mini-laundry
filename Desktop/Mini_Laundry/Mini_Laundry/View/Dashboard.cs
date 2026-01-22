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
        ManageEmployee manageEmployee;
        bool logout = false;
        public Dashboard(Main main)
        {
            mainForm = main;
            mainDashboard = new MainDashboard();
            manageEmployee = new ManageEmployee();
            manageEmployee.Hide();
            InitializeComponent();
            mainDashboard.sayHello(DataConnection.currUser["name"].ToString());
            panel1.Controls.Add(mainDashboard);
            panel1.Controls.Add(manageEmployee);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            logout = true;
            Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            manageEmployee.Show();
            mainDashboard.Hide();
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!logout)
            {
                mainForm.Close();
            }
        }
    }
}
