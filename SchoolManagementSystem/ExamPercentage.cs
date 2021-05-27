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
            var values = DBContext.GetContext().Query("percentage").Get();

            foreach (var value in values)
            {
                dgvPercentage.Rows.Add(value.id, value.prelim, value.midterm, value.semiFinals, value.finals);
            }
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            var myfrm = new AddExamPercentage(this);
            myfrm.ShowDialog();
        }

        private void dgvPercentage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dgvPercentage.Rows.Count; i++)
            {
                if (dgvPercentage.SelectedRows[0].Cells[5].Value.ToString() == "Activate")
                {
                    MessageBox.Show("wew");
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    linkCell.Value = "Deactivate";
                    dgvPercentage.SelectedRows[0].Cells[5] = linkCell;
                }
                else
                {
                    MessageBox.Show("bbb");
                    foreach (DataGridViewRow row in dgvPercentage.Rows)
                    {
                        DataGridViewLinkCell linkCells = new DataGridViewLinkCell();
                        linkCells.Value = "Activate";
                        row.Cells[5] = linkCells;
                    }
                }
                MessageBox.Show("aaa");
            }
        }
    }
}
