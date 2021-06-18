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
    public partial class FeeManagement : Form
    {
        public FeeManagement()
        {
            InitializeComponent();
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            var myfrm = new AddFeeCategory(this);
            FormFade.FadeForm(this, myfrm);
        }

        private void FeeManagement_Load(object sender, EventArgs e)
        {
            displayData();
        }
        
        public void displayData()
        {

          
            dgvCategories.Rows.Clear();
            var values = DBContext.GetContext().Query("categoryfee").Get();
            
            foreach (var value in values)
            {
                dgvCategories.Rows.Add(value.categoryID, value.category);
            }

        }

        private void dgvCategories_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategories.Columns[e.ColumnIndex].Name;

            if (colName.Equals("delete"))
            {
                if (Validator.DeleteConfirmation())
                {
                    DBContext.GetContext().Query("categoryfee").Where("categoryID", dgvCategories.SelectedRows[0].Cells[0].Value).Delete();
                    DBContext.GetContext().Query("totalfee").Where("categoryID", dgvCategories.SelectedRows[0].Cells[0].Value).Delete();
                    displayData();
                }
            }
        }
    }
}
