using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EonBotzLibrary;
using SqlKata.Execution;

namespace SchoolManagementSystem
{
    public partial class ExamPercentage : Form
    {
        public ExamPercentage()
        {
            InitializeComponent();
        }

        private void ExamPercentage_Load(object sender, EventArgs e)
        {
            displayData();
        }
        public void displayData()
        {
            dgvPercentage.Rows.Clear();


            var values = DBContext.GetContext().Query("percentage").Get();
            foreach (var value in values)
            {
                dgvPercentage.Rows.Add(value.id, value.prelim, value.midterm, value.semiFinals, value.finals, value.status);
            }

            foreach (DataGridViewRow row in dgvPercentage.Rows)
            {
                if (Convert.ToString(row.Cells[5].Value) == "Activate")
                {
                    row.Cells[5].Style.ForeColor = Color.Blue;
                    row.Cells[5].Style.SelectionForeColor = Color.Blue;
                }
                else
                {
                    row.Cells[5].Style.ForeColor = Color.Red;
                    row.Cells[5].Style.SelectionForeColor = Color.Red;
                }
            }

        }




        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            var myfrm = new AddExamPercentage(this);
            myfrm.ShowDialog();
        }

        private void dgvPercentage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colName = dgvPercentage.Columns[e.ColumnIndex].Name;

            if (dgvPercentage.SelectedRows[0].Cells[5].Value.ToString() == "Activate")
            {
                DBContext.GetContext().Query("percentage").Update(new
                {
                    status = "Activate"
                });

                int id = Convert.ToInt32(dgvPercentage.SelectedRows[0].Cells[0].Value);
                DBContext.GetContext().Query("percentage").Where("id", id).Update(new
                {
                    status = "Deactivate"
                });
                displayData();
            }
            else if(dgvPercentage.SelectedRows[0].Cells[5].Value.ToString() == "Deactivate")
            {
                DBContext.GetContext().Query("percentage").Update(new
                {
                    status = "Activate"
                });
                displayData();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }
    }
}