using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EonBotzLibrary;
using SchoolManagementSystem.FORMS.FeeManagement;

namespace SchoolManagementSystem
{
    public partial class FeeStructure : Form
    {
        feeStruc fee = new feeStruc();
        public FeeStructure()
        {
          
            InitializeComponent();
        }

        private void btnAddFeeStruc_Click(object sender, EventArgs e)
        {
            var myfrm = new AddFeeStructure();
            myfrm.ShowDialog();
        }

        private void FeeStructure_Load(object sender, EventArgs e)
        {
            displayData();
        }

        public void displayData()
        {
            fee.view();
            dgvFee.Rows.Clear();
            foreach (DataRow Drow in fee.dt.Rows)
            {
                int num = dgvFee.Rows.Add();

                dgvFee.Rows[num].Cells[0].Value = Drow["ID"].ToString();
                dgvFee.Rows[num].Cells[1].Value = Drow["structurename"].ToString();
                dgvFee.Rows[num].Cells[2].Value = Drow["Description"].ToString();
                dgvFee.Rows[num].Cells[3].Value = Drow["count"].ToString();
                dgvFee.Rows[num].Cells[4].Value = Drow["total"].ToString();
            }
        }
        private void dgvFee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var add = new addfees(dgvFee.SelectedRows[0].Cells[0].Value.ToString(), dgvFee.SelectedRows[0].Cells[1].Value.ToString(), this);
            add.Show();
        }
    }
}
