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
    public partial class CourseInformation : Form
    {
        public CourseInformation()
        {
            InitializeComponent();
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            var myfrm = new AddCourse(this, idd);
            FormFade.FadeForm(this, myfrm);
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
                dgvCourse.Rows.Add(courses.courseId, $"{courses.description}({courses.abbreviation})");
            }
        }

        private void dgvCourse_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


        }

        private void dgvCourse_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Focus);
            e.Handled = true;
        }

        string idd;

        private void dgvCourse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCourse.Columns[e.ColumnIndex].Name;

            if (colName.Equals("edit"))
            {
                idd = dgvCourse.Rows[dgvCourse.CurrentRow.Index].Cells[0].Value.ToString();

                var value = DBContext.GetContext().Query("course")
                    .Join("department", "department.deptID", "course.deptID")
                    .Where("courseId", idd)
                    .First();

                var myfrm = new AddCourse(this, idd);

                myfrm.cmbDepartment.Text = value.deptName;
                myfrm.txtDescription.Text = value.description;
                myfrm.txtAbbreviation.Text = value.abbreviation;

                myfrm.btnSave.Text = "Update";
                myfrm.ShowDialog();
            }
        }
    }
}