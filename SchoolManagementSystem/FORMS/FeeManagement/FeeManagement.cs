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
    public partial class FeeManagement : Form
    {
        public FeeManagement()
        {
            InitializeComponent();
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            var myfrm = new AddFeeCategory(this);
            myfrm.ShowDialog();
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
    }
}
