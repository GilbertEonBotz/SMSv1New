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
    public partial class CourseInformation : Form
    {
        public CourseInformation()
        {
            InitializeComponent();
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            var myfrm = new AddCourse(this);
            myfrm.ShowDialog();
        }

        private void CourseInformation_Load(object sender, EventArgs e)
        {
            displayData();
        }

        public void displayData()
        {
            dgvCourse.Rows.Clear();
            var course = DBContext.GetContext().Query("course").Get();

            foreach (var courses in course)
            {
                dgvCourse.Rows.Add(courses.courseId , $"{courses.description}({courses.abbreviation})");
            }
        }

        private void dgvCourse_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var myfrm = new AddCourse(this);
            int id = Convert.ToInt32(dgvCourse.Rows[dgvCourse.CurrentRow.Index].Cells[0].Value);
            var value = DBContext.GetContext().Query("course").Where("courseId", id).First();
            string getDescID = value.deptID;

            var selDept = DBContext.GetContext().Query("department").Where("deptID", getDescID).First();

            myfrm.lblIDD.Text = id.ToString();

            myfrm.cmbDepartment.Text = selDept.description;
            myfrm.txtDescription.Text = value.description;
            myfrm.txtAbbreviation.Text = value.abbreviation;
         
            myfrm.btnSave.Text = "Update";
            myfrm.ShowDialog();
        }
    }
}