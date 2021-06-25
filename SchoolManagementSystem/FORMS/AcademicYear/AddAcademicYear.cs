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
                    try
                    {
                        DBContext.GetContext().Query("academicyear").Where("year1", txtYear1.Text).Where("year2", txtYear2.Text).Where("term", cmbTerm.Text).First();
                        Validator.AlertDanger("academic year existed!");
                    }
                    catch (Exception)
                    {
                        DBContext.GetContext().Query("academicyear").Update(new
                        {
                            year1 = txtYear1.Text.Trim(),
                            year2 = txtYear2.Text.Trim(),
                            term = cmbTerm.Text.Trim()
                        });

                        DBContext.GetContext().Query("academicyear").Insert(new
                        {
                            year1 = txtYear1.Text.Trim(),
                            year2 = txtYear2.Text.Trim(),
                            term = cmbTerm.Text.Trim()
                        });
                        reloadDatagrid.displayData();
                        this.Close();
                    }
                }
            }
        }

        private void txtYear1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtYear2.Text = (long.Parse(txtYear1.Text) + 1).ToString();
            }
            catch (Exception)
            {
                txtYear2.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
