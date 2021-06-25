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
using Microsoft.Reporting.WinForms;
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
        ReportDataSource test = new ReportDataSource();
        private void button1_Click(object sender, EventArgs e)
        {
            var getSched = DBContext.GetContext().Query("studentSched").Where("studentID", "1").First();
            double[] catID = { };

            int strucID = 0;
            int getCategoryID = 0;
            string str = getSched.schedId;
            var words = str.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                allScheds = words[i];
                var values = DBContext.GetContext().Query("studentSched")
                  .CrossJoin("schedule")
                  .Join("Billing", "Billing.studentSchedid", "studentSched.studentSchedID")
                  .Join("student", "student.studentId", "studentSched.studentID")
                  .Join("subjects", "subjects.subjectCode", "schedule.subjectCode")
                  .Where("schedule.schedID", allScheds)
                  .GroupBy("schedule.schedID").Get();

                foreach (var value in values)
                {
                    strucID = value.structureID;

                    var aa = DBContext.GetContext().Query("feestructure").Where("structureID", strucID).First();

                    txtName.Text = $"{value.firstname} {value.lastname}";
                    txtGender.Text = value.gender;
                    txtDateOfRegistration.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                    dgvStudentSched.Rows.Add(allScheds, value.subjectCode, value.subjectTitle, value.date, value.roomID, value.timeStart, value.timeEnd, $"{value.lec}/{value.lab}");
                }
            }

            var tblTotalfee = DBContext.GetContext().Query("totalfee").Where("structureID", strucID).WhereNotNull("total").WhereNotNull("categoryID").Get();

            foreach (var tblFees in tblTotalfee)
            {
                MessageBox.Show(tblFees.categoryID.ToString());
                getCategoryID = tblFees.categoryID;
                var assCategoryID = DBContext.GetContext().Query("categoryfee").Where("categoryID", getCategoryID).Get();

                foreach (var getIds in assCategoryID)
                {
                    MessageBox.Show(getIds.category);
                }
            }

            

            List<EnrolledSubjects> erTest = new List<EnrolledSubjects>();
            erTest.Clear();

            for (int i = 0; i < dgvStudentSched.Rows.Count; i++)
            erTest.Add(new EnrolledSubjects
            {
                name = txtName.Text,
                course = txtCourse.Text,
                gender = txtGender.Text,
                date = DateTime.Now.ToString("dddd, dd MMMM yyyy"),
                schedID = dgvStudentSched.Rows[i].Cells[1].Value.ToString(),
                subjectCode = dgvStudentSched.Rows[i].Cells[2].Value.ToString(),
                room = dgvStudentSched.Rows[i].Cells[3].FormattedValue.ToString(),
            });


            test.Name = "DataSet1";
            test.Value = erTest;

            var frm = new StudentEnrolledViewer();
            frm.reportViewer1.LocalReport.DataSources.Clear();
            frm.reportViewer1.LocalReport.DataSources.Add(test);
            frm.reportViewer1.LocalReport.ReportEmbeddedResource = "SchoolManagementSystem.Report1.rdlc";
            frm.ShowDialog();

        }
    }

    public class EnrolledSubjects
    {
        public string studentNo { get; set; }
        public string name { get; set; }
        public string course { get; set; }
        public string gender { get; set; }
        public string date { get; set; }
        public string schedID { get; set; }
        public string mergeTime { get; set; }
        public string subjectCode { get; set; }
        public string room { get; set; }
        public string capacity { get; set; }
        public string status { get; set; }
        public string lablec { get; set; }
        public string category { get; set; }
        public string amount { get; set; }
        public string totalUnitss { get; set; }

    }
   
}
