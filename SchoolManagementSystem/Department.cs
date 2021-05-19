using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EonBotzLibrary;
using SqlKata.Execution;

namespace SchoolManagementSystem
{
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
        }

        private void btnAddDept_Click(object sender, EventArgs e)
        {
            var myfrm = new AddDepartment(this);
            myfrm.ShowDialog();
        }

        public void displayData()
        {
            dgvDepartment.Rows.Clear();
            var depts = DBContext.GetContext().Query("department").Get();

            foreach (var dept in depts)
            {
                dgvDepartment.Rows.Add(dept.deptID, dept.description);
            }
        }

        private void Department_Load(object sender, EventArgs e)
        {
            displayData();
            
        }

        private void dgvDepartment_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var myfrm = new AddDepartment(this);
            int id = Convert.ToInt32(dgvDepartment.Rows[dgvDepartment.CurrentRow.Index].Cells[0].Value);
            var depts = DBContext.GetContext().Query("department").Where("deptID", id).First();

            myfrm.lblIDD.Text = id.ToString();
            myfrm.txtDeptName.Text = depts.description;
            myfrm.btnSave.Text = "Update";
            myfrm.ShowDialog();
        }
    }
}
