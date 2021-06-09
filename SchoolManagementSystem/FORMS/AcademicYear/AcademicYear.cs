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
                dgvAcademicYear.Rows.Add(value.id, $"{value.year1}-{value.year2} {value.term}", value.status);
            }

            foreach (DataGridViewRow row in dgvAcademicYear.Rows)
            {
                if (Convert.ToString(row.Cells[2].Value) == "Activate")
                {
                    row.Cells[2].Style.ForeColor = Color.Blue;
                    row.Cells[2].Style.SelectionForeColor = Color.Blue;
                }
                else
                {
                    row.Cells[2].Style.ForeColor = Color.Red;
                    row.Cells[2].Style.SelectionForeColor = Color.Red;
                }
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

        private void dgvAcademicYear_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvAcademicYear.Columns[e.ColumnIndex].Name;

            if (dgvAcademicYear.SelectedRows[0].Cells[2].Value.ToString() == "Activate")
            {
                if (Validator.actYear())
                {
                    DBContext.GetContext().Query("academicyear").Update(new
                    {
                        status = "Activate"
                    });

                    int id = Convert.ToInt32(dgvAcademicYear.SelectedRows[0].Cells[0].Value);
                    DBContext.GetContext().Query("academicyear").Where("id", id).Update(new
                    {
                        status = "Deactivate"
                    });
                    displayData();
                }
            }
            else if (dgvAcademicYear.SelectedRows[0].Cells[2].Value.ToString() == "Deactivate")
            {
                if (Validator.deactYear())
                {
                    DBContext.GetContext().Query("academicyear").Update(new
                    {
                        status = "Activate"
                    });
                    displayData();
                }

            }
        }
    }
}
