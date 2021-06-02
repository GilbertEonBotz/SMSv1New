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
using MySql.Data.MySqlClient;
namespace SchoolManagementSystem
{
    public partial class StudentEnrolledSubjects : Form
    {

        Connection connect = new Connection();
        MySqlCommand cmd;
        MySqlConnection conn;
        MySqlDataReader dr;
        public StudentEnrolledSubjects()
        {
            InitializeComponent();
        }
        string allScheds;

        private void StudentEnrolledSubjects_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var getSched = DBContext.GetContext().Query("studentSched").Where("studentID", "43").First();

            string str = getSched.schedId;
            var words = str.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                allScheds = words[i];
                var values = DBContext.GetContext().Query("studentSched")
                  .CrossJoin("schedule")
                  .Join("subjects", "subjects.subjectCode", "schedule.subjectCode")
                  .Where("schedule.schedID", allScheds)
                  .GroupBy("schedule.schedID").Get();

                foreach (var value in values)
                {
                    dgvStudentSched.Rows.Add(allScheds, value.subjectCode, value.subjectTitle, value.date, value.roomID, value.timeStart, value.timeEnd, $"{value.lec}/{value.lab}");
                }
            }
        }
    }
}
