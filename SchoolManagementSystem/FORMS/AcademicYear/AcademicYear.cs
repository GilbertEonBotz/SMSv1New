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
    public partial class AcademicYear : Form
    {
        public AcademicYear()
        {
            InitializeComponent();
        }

        private void AcademicYear_Load(object sender, EventArgs e)
        {
            displayData();
           
        }
        public void displayData()
        {
            dgvAcademicYear.Rows.Clear();
            var values = DBContext.GetContext().Query("academicyear").Get();

            foreach (var value in values)
            {
                dgvAcademicYear.Rows.Add(value.academicId, $"{value.year1}-{value.year2} {value.term}", $"{value.year1}-{value.year2 }", value.term);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            var myfrm = new AddAcademicYear(this);
            myfrm.ShowDialog();
        }

        private void dgvAcademicYear_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(dgvAcademicYear.Rows[dgvAcademicYear.CurrentRow.Index].Cells[0].Value);
        }
    }
}
