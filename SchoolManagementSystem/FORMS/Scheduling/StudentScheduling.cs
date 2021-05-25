﻿using System;
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
            //pnlBilling.SetBounds(0, 0, 0, 0);
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

        StudentTuition stud = new StudentTuition();
        Double amount = 0;

        ReportDataSource rs = new ReportDataSource();
        ReportDataSource rsBill = new ReportDataSource();
        ReportDataSource rsTuition = new ReportDataSource();
        private void btnPrint_Click(object sender, EventArgs e)
        {

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

            if (Validator.AddConfirmation())
            {
                for (int i = 0; i < dgvStudentSched.Rows.Count; i++)
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
                    storeID = "";
                    amount = 0;
                    List<feeBillings> bills = new List<feeBillings>();
                    bills.Clear();

                    List<tuitionBilling> tuit = new List<tuitionBilling>();
                    tuit.Clear();
                    
                    
                    var values = DBContext.GetContext().Query("studentSched").Where("studentId", cmbStudentNo.Text).First();
                    string str = values.schedId;

                    var words = str.Split(' ');

                    for (int i = 1; i < words.Length; i++)
                    {
                        string individualSubj = words[i];
                        stud.indsub = individualSubj;

                        stud.viewSubj();
                        foreach (DataRow Drow in stud.dt.Rows)
                        {
                            amount += Convert.ToDouble(Drow["Amount"].ToString());
                        }
                    }

                    for (int i = 0; i < dgvCategories.Rows.Count; i++)
                    {
                        bills.Add(new feeBillings
                        {
                            total = Convert.ToDouble(lblTotal.Text),
                            category = dgvCategories.Rows[i].Cells[0].Value.ToString(),
                            amount = Convert.ToDouble(dgvCategories.Rows[i].Cells[1].Value.ToString()),
                        });
                    }

                    tuit.Add(new tuitionBilling
                    {
                        tuitionTotal = Convert.ToDouble(amount.ToString()),
                    });

                    LocalReport localReport = new LocalReport();
                    localReport.ReportEmbeddedResource = "SchoolManagementSystem.Report2.rdlc";
                    localReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", lst));
                    localReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", bills));
                    localReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", tuit));
                    //localReport.Print();
                }
            }

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

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            pnlBilling.SetBounds(203, 100, 795, 462);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            pnlBilling.SetBounds(0, 0, 0, 0);
        }

        private void btnPreview_Click(object sender, EventArgs e)
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

            if (Validator.AddPreview())
            {
                for (int i = 0; i < dgvStudentSched.Rows.Count; i++)
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
                    storeID = "";

                    List<feeBillings> bills = new List<feeBillings>();
                    bills.Clear();

                    List<tuitionBilling> tuit = new List<tuitionBilling>();


                    amount = 0;

                    var values = DBContext.GetContext().Query("studentSched").Where("studentId", cmbStudentNo.Text).First();
                    string str = values.schedId;

                    var words = str.Split(' ');

                    for (int i = 1; i < words.Length; i++)
                    {
                        string individualSubj = words[i];
                        stud.indsub = individualSubj;

                        stud.viewSubj();
                        foreach (DataRow Drow in stud.dt.Rows)
                        {
                            amount += Convert.ToDouble(Drow["Amount"].ToString());
                        }
                    }
                    
                    for (int i = 0; i < dgvCategories.Rows.Count; i++)
                    {
                        bills.Add(new feeBillings
                        {
                            total = Convert.ToDouble(lblTotal.Text),
                            category = dgvCategories.Rows[i].Cells[0].Value.ToString(),
                            amount = Convert.ToDouble(dgvCategories.Rows[i].Cells[1].Value.ToString()),
                        });
                    }

                    tuit.Add(new tuitionBilling
                    {
                        tuitionTotal = Convert.ToDouble(amount.ToString()),
                    });

                    rs.Name = "DataSet1";
                    rs.Value = lst;
                    rsBill.Name = "DataSet2";
                    rsBill.Value = bills;
                    rsTuition.Name = "DataSet3";
                    rsTuition.Value = tuit;

                    frm.reportViewer1.LocalReport.DataSources.Clear();
                    frm.reportViewer1.LocalReport.DataSources.Add(rs);
                    frm.reportViewer1.LocalReport.DataSources.Add(rsBill);
                    frm.reportViewer1.LocalReport.DataSources.Add(rsTuition);
                    frm.reportViewer1.LocalReport.ReportEmbeddedResource = "SchoolManagementSystem.Report2.rdlc";
                    frm.ShowDialog();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            amount = 0;

            var values = DBContext.GetContext().Query("studentSched").Where("studentId", cmbStudentNo.Text).First();
            string str = values.schedId;

            var words = str.Split(' ');

            for (int i = 1; i < words.Length; i++)
            {
                string individualSubj = words[i];
                stud.indsub = individualSubj;

                stud.viewSubj();
                foreach (DataRow Drow in stud.dt.Rows)
                {
                    amount += Convert.ToDouble(Drow["Amount"].ToString());
                }
            }
            MessageBox.Show(amount.ToString());
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
        public double amount { get; set; }
        public double total { get; set; }
    }

    public class tuitionBilling
    {
        public double tuitionTotal { get; set; }
    }
}