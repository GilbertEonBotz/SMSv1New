﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EonBotzLibrary;
using SchoolManagementSystem.UITools;
using SqlKata.Execution;

namespace SchoolManagementSystem
{
    public partial class CourseCode : Form
    {
        public CourseCode()
        {
            InitializeComponent();
        }

        private void CourseCode_Load(object sender, EventArgs e)
        {
            displayData();
        }

        public void displayData()
        {
            var values = DBContext.GetContext().Query("coursecode")
                .Join("course", "course.courseId", "coursecode.courseId")
                .Get();

            dgvCourseCode.Rows.Clear();
            foreach(var value in values)
            {
                dgvCourseCode.Rows.Add(value.coursecodeId, value.coursecode, value.description,  value.remarks);
            }
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            var myfrm = new addCourseCode(this);
            FormFade.FadeForm(this, myfrm);
        }

        private void dgvCourseCode_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Focus);
            e.Handled = true;
        }

        private void dgvCourseCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCourseCode.Columns[e.ColumnIndex].Name;

            if (colName.Equals("edit"))
            {
                var myfrm = new addCourseCode(this);
                int id = Convert.ToInt32(dgvCourseCode.Rows[dgvCourseCode.CurrentRow.Index].Cells[0].Value);

                myfrm.lblIDD.Text = id.ToString();
                myfrm.cmbDepartment.Text = dgvCourseCode.SelectedRows[0].Cells[2].Value.ToString();
                myfrm.txtCourseCode.Text = dgvCourseCode.SelectedRows[0].Cells[1].Value.ToString();
                myfrm.txtRemarks.Text = dgvCourseCode.SelectedRows[0].Cells[3].Value.ToString();
                myfrm.btnSave.Text = "Update";
                myfrm.ShowDialog();
            }
        }
    }
}
