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
    public partial class teacherSched : Form
    {
        string num1 = "";
        string storeID;
        string units;
        public teacherSched()
        {

            InitializeComponent();

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            addTeacherScheduling add = new addTeacherScheduling(this);
            add.Show();
        }

        private void teacherSched_Load(object sender, EventArgs e)
        {
            var values = DBContext.GetContext().Query("teachers").Get();

            foreach (var value in values)
            {
                cmbTeacher.Items.Add(value.teacherId);
            }
        }
        public string[] wew;
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < dgvStudentSched.Rows.Count; i++)
            {

                wew = new string[] { dgvStudentSched.Rows[i].Cells[0].Value.ToString() };

                foreach (string aa in wew)
                {
                    storeID += (" " + aa);

                }

            }
            if (storeID == "")
            {

            }
            else
            {

                DBContext.GetContext().Query("teachersched").Insert(new
                {
                    teacherid = cmbTeacher.Text,
                    schedid = storeID
                });
                MessageBox.Show("success");
                storeID = "";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values = DBContext.GetContext().Query("teachers").Where("teacherId", cmbTeacher.Text).Get();

            foreach (var value in values)
            {
                txtName.Text = $"{value.Firstname} {value.Lastname}";
                txtGender.Text = value.Gender;
                txtDateOfRegistration.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            }
        }
    }
}
