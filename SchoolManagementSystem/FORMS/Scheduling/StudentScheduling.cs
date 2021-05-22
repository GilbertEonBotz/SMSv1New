using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EonBotzLibrary;
using SqlKata.Execution;
using Microsoft.Reporting.WinForms;

namespace SchoolManagementSystem
{
    public partial class StudentScheduling : Form
    {
        string storeID;
        studentSched sched = new studentSched();
        feeStruc struc = new feeStruc();
        public StudentScheduling()
        {
            InitializeComponent();

        }

        private void StudentScheduling_Load(object sender, EventArgs e)
        {
            pnlBilling.SetBounds(0, 0, 0, 0);
            displayDataCmb();
        }

        public void displayStudents()
        {
            dgvStudentSched.Columns[4].DefaultCellStyle.Format = "hh:mm tt";
            dgvStudentSched.Columns[5].DefaultCellStyle.Format = "hh:mm tt";

            var values = DBContext.GetContext().Query("student").Get();

            foreach (var value in values)
            {
                cmbStudentNo.Items.Add(value.lastname);
            }


        }

        public void displayDataCmb()
        {
            var values = DBContext.GetContext().Query("tuitioncategory").Get();


            foreach (var value in values)
            {
                cmbSubjects.Items.Add(value.category);
            }

            var wew = DBContext.GetContext().Query("student").Get();

            foreach (var value in wew)
            {
                cmbStudentNo.Items.Add(value.studentId);
            }

            var fee = DBContext.GetContext().Query("feestructure").Get();

            foreach (var value in fee)
            {
                cmbCategoryFee.Items.Add(value.structureName);
            }

        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            var myfrm = new AddStudentScheduling(this);
            myfrm.ShowDialog();
        }
        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            var values = DBContext.GetContext().Query("student").Where("studentId", cmbStudentNo.Text).Get();

            foreach (var value in values)
            {
                string id = value.course;
                var desc = DBContext.GetContext().Query("course").Where("courseId", id).First();
                txtName.Text = $"{value.firstname} {value.lastname}";
                txtGender.Text = value.gender;
                txtCourse.Text = desc.abbreviation;
                txtDateOfRegistration.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var values = DBContext.GetContext().Query("student").Where("studentId", comboBox1.Text).Get();

            //foreach (var value in values)
            //{
            //    txtName.Text = value.firstname();
            //}
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = DBContext.GetContext().Query("tuitioncategory").Where("category", cmbSubjects.Text).First();
            string getid = value.tuitionCatID.ToString();



            dgvStudentSched.Rows.Clear();

            sched.category = getid;

            dgvStudentSched.Columns[5].DefaultCellStyle.Format = "hh:mm tt";
            dgvStudentSched.Columns[6].DefaultCellStyle.Format = "hh:mm tt";
            sched.display();

            foreach (DataRow Drow in sched.dt.Rows)
            {
                int num = dgvStudentSched.Rows.Add();
                dgvStudentSched.Rows[num].Cells[0].Value = Drow["SchedID"].ToString();
                dgvStudentSched.Rows[num].Cells[1].Value = Drow["SubjectCode"].ToString();
                dgvStudentSched.Rows[num].Cells[2].Value = Drow["SubjectTitle"].ToString();
                dgvStudentSched.Rows[num].Cells[3].Value = Drow["RoomName"].ToString();
                dgvStudentSched.Rows[num].Cells[4].Value = Drow["Day"].ToString();
                dgvStudentSched.Rows[num].Cells[5].Value = Convert.ToDateTime(Drow["Timestart"].ToString());
                dgvStudentSched.Rows[num].Cells[6].Value = Convert.ToDateTime(Drow["Timeend"].ToString());
                dgvStudentSched.Rows[num].Cells[7].Value = Drow["MaxStudent"].ToString();
                dgvStudentSched.Rows[num].Cells[8].Value = Drow["Status"].ToString();
                dgvStudentSched.Rows[num].Cells[9].Value = Drow["lablec"].ToString();
            }
        }

        private void dgvStudentSched_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public string[] wew;
        ReportDataSource rs = new ReportDataSource();
        ReportDataSource rsBill = new ReportDataSource();
        private void btnPrint_Click(object sender, EventArgs e)
        {
            StudentSchedulesReportViewer frm = new StudentSchedulesReportViewer();

            List<Schedulings> lst = new List<Schedulings>();
            lst.Clear();
          
            for (int i = 0; i < dgvStudentSched.Rows.Count; i++)
            {
                    
                    lst.Add(new Schedulings
                    {
                        studentNo = cmbStudentNo.Text,
                        name = txtName.Text,
                        course = txtCourse.Text,
                        gender = txtGender.Text,
                        date = txtDateOfRegistration.Text,
                        schedID = dgvStudentSched.Rows[i].Cells[1].Value.ToString(),
                        subjectCode = dgvStudentSched.Rows[i].Cells[2].Value.ToString(),
                        room = dgvStudentSched.Rows[i].Cells[3].FormattedValue.ToString(),
                        mergeTime = dgvStudentSched.Rows[i].Cells[4].FormattedValue.ToString() + " " + dgvStudentSched.Rows[i].Cells[5].FormattedValue.ToString() + "-" + dgvStudentSched.Rows[i].Cells[6].FormattedValue.ToString(),
                        capacity = dgvStudentSched.Rows[i].Cells[7].Value.ToString(),
                        status = dgvStudentSched.Rows[i].Cells[8].Value.ToString(),
                        lablec = dgvStudentSched.Rows[i].Cells[9].Value.ToString()
                    });
                
                
            }

            List<feeBillings> bills = new List<feeBillings>();
            bills.Clear();

            for (int i = 0; i < dgvCategories.Rows.Count; i++)
            {
                bills.Add(new feeBillings
                {
                    total = lblTotal.Text,
                    category = dgvCategories.Rows[i].Cells[0].Value.ToString(),
                    amount = dgvCategories.Rows[i].Cells[1].Value.ToString()
                }); 
            }


            rsBill.Name = "DataSet2";
            rsBill.Value = bills;
            rs.Name = "DataSet1";
            rs.Value = lst;
            frm.reportViewer1.LocalReport.DataSources.Clear();
            frm.reportViewer1.LocalReport.DataSources.Add(rs);
            frm.reportViewer1.LocalReport.DataSources.Add(rsBill);
            frm.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            frm.reportViewer1.LocalReport.ReportEmbeddedResource = "SchoolManagementSystem.Report2.rdlc";

            if (Validator.AddConfirmation())
            {
                int i;
                for (i = 0; i < dgvStudentSched.Rows.Count; i++)
                {

                    wew = new string[] { dgvStudentSched.Rows[i].Cells[0].Value.ToString()
    };

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

                    DBContext.GetContext().Query("studentSched").Insert(new
                    {
                        studentID = cmbStudentNo.Text,
                        schedId = storeID
                    });
                    MessageBox.Show("success");
                    storeID = "";
                }
            }

            frm.ShowDialog();
        }

        private void cmbCategoryFee_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvCategories.Rows.Clear();

            var value = DBContext.GetContext().Query("feestructure").Where("structureName", cmbCategoryFee.Text).First();

            string feeId = value.structureID.ToString();

            struc.structureID = feeId;
            struc.dt.Clear();


            struc.viewfees();
            
            foreach (DataRow Drow in struc.dt.Rows)
            {
                
                int num = dgvCategories.Rows.Add();

                dgvCategories.Rows[num].Cells[0].Value = Drow["category"].ToString();
                dgvCategories.Rows[num].Cells[1].Value = Drow["amount"].ToString();
            }

            lblTotal.Text = struc.total;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            pnlBilling.SetBounds(203, 100, 795, 462);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            pnlBilling.SetBounds(0, 0, 0, 0);
        }
    }




    public class Schedulings
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

    }

    public class feeBillings
    {
        public string category { get; set; }
        public string amount { get; set; }
        public string total { get; set; }
    }
}