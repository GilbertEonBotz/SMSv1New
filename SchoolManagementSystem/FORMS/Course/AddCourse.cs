﻿using System;
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
    public partial class AddCourse : Form
    {
        Course course = new Course();
        CourseInformation reloadDatagrid;
        public AddCourse(CourseInformation reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
        }


        private void displayDepartments()
        {
            var values = DBContext.GetContext().Query("department").Get();

            foreach (var value in values)
            {
                cmbDepartment.Items.Add(value.description);
            }
        }
        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            TextBox[] inputs = { txtDescription, txtAbbreviation };

            var value = DBContext.GetContext().Query("department").Where("description", cmbDepartment.Text).First();
            
            if (btnSave.Text.Equals("Update"))
            {
                if (Validator.isEmpty(inputs) && Validator.UpdateConfirmation())
                {
                    DBContext.GetContext().Query("course").Where("courseId", lblIDD.Text).Update(new
                    {
                        courseCode = txtCourseCode.Text,
                        description = txtDescription.Text,
                        abbreviation = txtAbbreviation.Text,
                        
                    });
                    reloadDatagrid.displayData();
                    this.Close();
                }
            }
            else if (btnSave.Text.Equals("Save"))
            {
                if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {
                    DBContext.GetContext().Query("course").Insert(new
                    {
                        courseCode = txtCourseCode.Text,
                        description = txtDescription.Text,
                        abbreviation = txtAbbreviation.Text,
                        deptID = value.deptID,
                        status = "enable"
                    });
                    reloadDatagrid.displayData();
                    this.Close();

                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddCourse_Load(object sender, EventArgs e)
        {
            displayDepartments();
        }
    }
}
