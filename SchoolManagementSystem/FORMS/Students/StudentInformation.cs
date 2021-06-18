using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EonBotzLibrary;
using SqlKata.Execution;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.FORMS;
using SchoolManagementSystem.UITools;

namespace SchoolManagementSystem
{

    public partial class StudentInformation : Form
    {
        Connection connect = new Connection();
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader mdr;
        MySqlDataAdapter msda;

        public StudentInformation()
        {
            InitializeComponent();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            AddStudent myfrm = new AddStudent(this);
            FormFade.FadeForm(this, myfrm);
        }
        private void StudentInformation_Load(object sender, EventArgs e)
        {

            displayData();
        }


        public void displayData()
        {
            try
            {
                var values = DBContext.GetContext().Query("student").Get();
                dgvStudents.Rows.Clear();
                foreach (var value in values)
                {
                    dgvStudents.Rows.Add(value.studentId, $"{value.lastname}, {value.firstname} {value.middlename}", value.gender, value.presentAddress, value.course);
                }
            }
            catch (Exception)
            {
                Validator.AlertDanger("Data conflicts for student course. Please contact your MSID");
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            var values = DBContext.GetContext().Query("student").WhereLike("studentId", $"%{txtSearch.Text}%").OrWhereLike("firstname", $"%{txtSearch.Text}%").OrWhereLike("lastname", $"%{txtSearch.Text}%").OrWhereLike("presentAddress", $"%{txtSearch.Text}%").Get();

            dt.Columns.Clear();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("GENDER");
            dt.Columns.Add("ADDRESS");
            dt.Columns.Add("COURSE");

            foreach (var value in values)
            {
                string id = value.course;
                var course = DBContext.GetContext().Query("course").Where("courseId", id).First();
                dt.Rows.Add(value.studentId, $"{value.lastname}, {value.firstname} {value.middlename}", value.gender, value.presentAddress, course.abbreviation);
            }

            dgvStudents.Rows.Clear();
            foreach(DataRow drow in dt.Rows)
            {
                int num = dgvStudents.Rows.Add();

                dgvStudents.Rows[num].Cells[0].Value = drow["ID"].ToString();
                dgvStudents.Rows[num].Cells[1].Value = drow["NAME"].ToString();
                dgvStudents.Rows[num].Cells[2].Value = drow["GENDER"].ToString();
                dgvStudents.Rows[num].Cells[3].Value = drow["ADDRESS"].ToString();
                dgvStudents.Rows[num].Cells[4].Value = drow["COURSE"].ToString();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {






        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvStudents.Columns[e.ColumnIndex].Name;

            if (colName.Equals("view"))
            {
                var myfrm = new studentActivation();
                myfrm.lblName.Text = dgvStudents.SelectedRows[0].Cells[1].Value.ToString();
                myfrm.lblCourse.Text = dgvStudents.SelectedRows[0].Cells[4].Value.ToString();
                var aa = DBContext.GetContext().Query("academicyear").Where("status", "Deactivate").First();
                myfrm.label8.Text = aa.year1 +"-"+aa.year2+ ",  "+aa.term;
                myfrm.studentid  = dgvStudents.SelectedRows[0].Cells[0].Value.ToString();
                myfrm.ShowDialog();

           
            }
        }
    }
}
