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
    public partial class addCourseCode : Form
    {
        CourseCode reloadDatagrid;
        public addCourseCode(CourseCode reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
        }

        int selCourseID;
        private void btnSave_Click(object sender, EventArgs e)
        {
            ComboBox[] cmb = { cmbDepartment };

            //if (btnSave.Text.Equals("Update"))
            //{
            //    if (Validator.isEmptyCmb(cmb))
            //    {
            //        DBContext.GetContext().Query("course").Where("courseId", lblIDD.Text).Update(new
            //        {
            //            courseCode = txtCourseCode.Text,
            //            description = txtRemarks.Text,
            //            abbreviation = txtAbbreviation.Text,

            //        });
            //        reloadDatagrid.displayData();
            //        this.Close();
            //    }
            //}
            if (btnSave.Text.Equals("Save"))
            {
                if (Validator.isEmptyCmb(cmb))
                {
                    try
                    {
                        DBContext.GetContext().Query("coursecode").Where("coursecode", txtCourseCode.Text).First();
                        Validator.AlertDanger("Course code already existed!");
                    }
                    catch (Exception)
                    {
                        DBContext.GetContext().Query("coursecode").Insert(new
                        {
                            courseId = selCourseID,
                            coursecode = txtCourseCode.Text,
                            remarks = txtRemarks.Text,
                            status = "enable"
                        });
                        Validator.AlertSuccess("Course code inserted");
                        reloadDatagrid.displayData();
                        this.Close();
                    }
                }
                else
                {
                }
            }
        }

        private void addCourseCode_Load(object sender, EventArgs e)
        {
            displayData();
        }

        public void displayData()
        {
            var values = DBContext.GetContext().Query("course").Get();

            foreach (var value in values)
            {
                cmbDepartment.Items.Add(value.abbreviation);
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = DBContext.GetContext().Query("course").Where("abbreviation", cmbDepartment.Text).First();
            selCourseID = value.courseId;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(selCourseID.ToString());
        }
    }
}
