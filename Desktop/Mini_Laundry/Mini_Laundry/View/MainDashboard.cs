using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Laundry.View
{
    public partial class MainDashboard : UserControl
    {
        public MainDashboard()
        {
            InitializeComponent();
        }

        public void sayHello(string who)
        {
            label1.Text = $"Halo {who}";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
