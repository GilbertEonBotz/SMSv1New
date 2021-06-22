using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EonBotzLibrary;
using SchoolManagementSystem.UITools;
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
            var myfrm = new AddDepartment(this, idd);
            FormFade.FadeForm(this, myfrm);
        }

        public void displayData()
        {
            dgvDepartment.Rows.Clear();
            var depts = DBContext.GetContext().Query("department").Get();

            foreach (var dept in depts)
            {
                dgvDepartment.Rows.Add(dept.deptID, dept.deptName);
            }
        }

        private void Department_Load(object sender, EventArgs e)
        {
            displayData();
            
        }

        private void dgvDepartment_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dgvDepartment_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Focus);
            e.Handled = true;
        }

        string idd;
        private void dgvDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvDepartment.Columns[e.ColumnIndex].Name;

            if (colName.Equals("edit"))
            {
                int id = Convert.ToInt32(dgvDepartment.Rows[dgvDepartment.CurrentRow.Index].Cells[0].Value);
                idd = id.ToString();
                var myfrm = new AddDepartment(this, idd);

                myfrm.txtDeptName.Text = dgvDepartment.Rows[dgvDepartment.CurrentRow.Index].Cells[1].Value.ToString();
                myfrm.btnSave.Text = "Update";
                myfrm.ShowDialog();
            }
        }
    }
}
