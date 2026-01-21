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
        public Dashboard(Main main)
        {
            this.mainForm = main;
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.mainForm.Show();
            this.Close();
        }
    }
}
