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
    public partial class AddAcademicYear : Form
    {

        AcademicYear reloadDatagrid;
        public AddAcademicYear(AcademicYear reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAcademicYear_Click(object sender, EventArgs e)
        {
            TextBox[] inputs = { txtYear1, txtYear2 };
            if (btnAddAcademicYear.Text.Equals("Save"))
            {
                if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {
                    DBContext.GetContext().Query("academicyear").Insert(new
                    {
                        year1 = txtYear1.Text,
                        year2 = txtYear2.Text,
                        term = cmbTerm.Text
                    });
                    Validator.AlertSuccess("inserted");
                    reloadDatagrid.displayData();
                }
            }
        }
    }
}
