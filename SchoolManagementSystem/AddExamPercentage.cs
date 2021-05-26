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
    public partial class AddExamPercentage : Form
    {
        public AddExamPercentage()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DBContext.GetContext().Query("percentage").Update(new { 
                status = "CLOSE"
            });

            var values = DBContext.GetContext().Query("percentage").Insert(new { 
                prelim = txtPrelim.Text,
                midterm = txtMidterm.Text,
                semiFinals = txtSemi.Text,
                finals = txtFinal.Text
            });
            MessageBox.Show("Inserted");
        }
    }
}
