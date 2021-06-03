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
        ExamPercentage reloadDatagrid;
        public AddExamPercentage(ExamPercentage reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DBContext.GetContext().Query("percentage").Update(new { 
                status = "Activate"
            });

            var values = DBContext.GetContext().Query("percentage").Insert(new { 
                prelim = $"0.{txtPrelim.Text}",
                midterm = $"0.{txtMidterm.Text}",
                semiFinals = $"0.{txtSemi.Text}",
                finals = $"0.{txtFinal.Text}"
            });
            MessageBox.Show("Inserted");
            reloadDatagrid.displayData();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
