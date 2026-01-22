using Mini_Laundry.Helper;
using Mini_Laundry.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Laundry.View
{
    public partial class ManageEmployee : UserControl
    {
        private int currId = -1;
        public ManageEmployee()
        {
            InitializeComponent();
            refreshData();
            employeeTable.Columns["salary"].DefaultCellStyle.Format = "c";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIdx = e.RowIndex;
            currId = int.Parse(employeeTable.Rows[rowIdx].Cells[0].Value.ToString());
            Debug.WriteLine(currId);
        }

        private void create_Click(object sender, EventArgs e)
        {
            AddEmployeeForm addEmployee = new AddEmployeeForm(this);
            addEmployee.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            refreshData();
        }
        public void refreshData()
        {
            employeeTable.DataSource = DataConnection.getData("SELECT * FROM employees");
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (currId < 0) return;
            Employee.delete(currId);
            currId = -1;
            refreshData();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (currId < 0) return;
            EditEmployeeForm editEmployee = new EditEmployeeForm(this, currId);
            editEmployee.ShowDialog();
        }
    }
}
