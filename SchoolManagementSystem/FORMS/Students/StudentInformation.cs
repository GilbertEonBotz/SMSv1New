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

    public partial class StudentInformation : Form
    {
        public StudentInformation()
        {
            InitializeComponent();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            AddStudent myform = new AddStudent(this);
            myform.ShowDialog();
        }
        private void StudentInformation_Load(object sender, EventArgs e)
        {
            displayData();
        }


        public void displayData()
        {
         
            var values = DBContext.GetContext().Query("student").Get();
            dgvStudents.Rows.Clear();
            foreach (var value in values)
            {
                string id = value.course;
                var course = DBContext.GetContext().Query("course").Where("courseId", id).First();
                dgvStudents.Rows.Add(value.studentId, $"{value.lastname}, {value.firstname} {value.middlename}", value.gender, value.presentAddress, course.description);
            }
        }

        private void dgvStudents_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var myfrm = new AddStudent(this);
            int id = Convert.ToInt32(dgvStudents.Rows[dgvStudents.CurrentRow.Index].Cells[0].Value);
            var value = DBContext.GetContext().Query("student").Where("studentId", id).First();

            myfrm.lblID.Text = id.ToString();
            myfrm.txtLastname.Text = value.lastname;
            myfrm.txtFirstname.Text = value.firstname;
            myfrm.txtMiddlename.Text = value.middlename;
            myfrm.cmbSuffix.Text = value.suffix;
            myfrm.dtpDateofbirth.Text = value.dateofbirth;
            myfrm.txtPlaceofbirth.Text = value.placeofbirth;
            myfrm.txtReligion.Text = value.religion;

            myfrm.cmbGender.Text = value.gender;
            myfrm.cmbMaritalStatus.Text = value.maritalstatus;
            myfrm.txtCitizen.Text = value.citizenship;
            myfrm.txtContactNo.Text = Convert.ToString(value.contactno);
            myfrm.txtEmailAddress.Text = value.emailAddress;
            myfrm.cmbCourse.Text = dgvStudents.Rows[dgvStudents.CurrentRow.Index].Cells[4].Value.ToString();
            myfrm.txtAddress.Text = value.presentAddress;
            myfrm.txtHomeAddress.Text = value.homeAddress;
            myfrm.txtFatherLname.Text = value.fatherLastname;
            myfrm.txtFatherFname.Text = value.fatherFirstname;
            myfrm.txtFatherMname.Text = value.fatherMiddlename;
            myfrm.txtFatherOccupation.Text = value.fatherOccupation;
            myfrm.txtMotherLname.Text = value.motherLastname;
            myfrm.txtMotherFname.Text = value.motherFirstname;
            myfrm.txtMotherMname.Text = value.motherMiddlename;
            myfrm.txtMotherOccupation.Text = value.motherOccupation;
            myfrm.txtSchooLast.Text = value.schoolLastAttended;
            myfrm.dtpLast.Text = value.dateLastAttended;

            myfrm.btnAddStudent.Text = "Update";
            myfrm.ShowDialog();
        }
    }
}
