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
    public partial class addSchedule : Form
    {
        string dateequal;
        string dtpTimstart;
        string dtpTimeEnd;
        string monday;
        string tuesday;
        string wednesday;
        string thursday;
        string friday;
        string saturday;
        schedule scheds = new schedule();
        public addSchedule()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void addSchedule_Load(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            TimeSpan ts = new TimeSpan(7, 00, 0);
            dt1 = dt1.Date + ts;

            DateTime dt2 = DateTime.Now;
            TimeSpan ts2 = new TimeSpan(8, 00, 0);
            dt2 = dt2.Date + ts2;

            dateTimePicker1.Value = dt1;
            dateTimePicker2.Value = dt2;


            displayCourseCode();
            scheds.Schedule();


            CbRoomNO.DataSource = scheds.datafillroom;



        }

        private void displayCourseCode()
        {
            var values = DBContext.GetContext().Query("course").Get();

            foreach (var value in values)
            {
                cbCourse.Items.Add(value.courseCode);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            scheds.subjcode = cbSubjCode.Text;

            scheds.Viewdescription();
            txtDescrip.Text = scheds.subjTitle;
        }

        private void txtDescrip_TextChanged(object sender, EventArgs e)
        {

        }
        private CheckBox[] checkboxcontrol;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                checkboxcontrol = new CheckBox[] { cbmon, cbtues, cbwed, cbthu, cbfri, cbsat };

                foreach (CheckBox chk in checkboxcontrol)
                {

                    if (chk.Checked)
                    {
                        dateequal += chk.Text;
                    }
                }

                dateequal = monday + tuesday + wednesday + thursday + friday + saturday;

                dtpTimstart = dateTimePicker1.Value.ToString("HH:mm");
                dtpTimeEnd = dateTimePicker2.Value.ToString("HH:mm");

                scheds.timeStart = dtpTimstart;
                scheds.timeEnd = dtpTimeEnd;
                scheds.subjcode = cbSubjCode.Text;
                scheds.date = dateequal;

                scheds.viewCourseID();
                scheds.Viewdescription();
                scheds.viewroomNum();
                scheds.times();


                if (scheds.timediff == null || scheds.timediff == "")
                {
                    save();
                    Validator.AlertSuccess(" saved");

                }
                else if (scheds.timeEnd == dtpTimstart)
                {
                    // save();
                    MessageBox.Show("aa");
                }
                else
                {
                    Validator.AlertDanger("Schedule existed");
                }
            }
            catch (Exception)
            {
                Validator.AlertDanger("Please fill up the following fields");
            }

            scheds.timediff = "";

            scheds.date = "";
            scheds.timeStart = "";
            scheds.timeEnd = "";
        }

        private void save()
        {
            CbRoomNO.Text = scheds.roomdesc;
            //  scheds.date = txtDate.Text;
            scheds.date = dateequal;
            scheds.timeEnd = dtpTimeEnd;
            scheds.timeStart = dtpTimstart;
            scheds.maxStudent = txtMax.Text;
            cbSubjCode.Text = scheds.subjcode;
            scheds.course = cbCourse.Text;
            scheds.insertSched();
        }

        private void CbRoomNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            scheds.roomdesc = CbRoomNO.Text;
        }

        private void cbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            var values = DBContext.GetContext().Query("subjects").Where("courseCode", cbCourse.Text).Get();
            cbSubjCode.Text = "";
            cbSubjCode.Items.Clear();
            foreach(var value in values)
            {
                cbSubjCode.Items.Add(value.subjectCode);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {





        }


        private void btnAddAccountant_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dateequal);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            dtpTimstart = dateTimePicker1.Value.ToString("H:mm:ss");
        }

        private void cbmon_CheckedChanged(object sender, EventArgs e)
        {
            if (cbmon.Checked)
            {
                monday = "1";
            }
            else
            {
                monday = "";
            }
        }

        private void cbtues_CheckedChanged(object sender, EventArgs e)
        {
            if (cbtues.Checked)
            {
                tuesday = "2";
            }
            else
            {
                tuesday = "";
            }
        }

        private void cbwed_CheckedChanged(object sender, EventArgs e)
        {
            if (cbwed.Checked)
            {
                wednesday = "3";
            }
            else
            {
                wednesday = "";
            }
        }

        private void cbthu_CheckedChanged(object sender, EventArgs e)
        {
            if (cbthu.Checked)
            {
                thursday = "4";
            }
            else
            {
                thursday = "";
            }
        }

        private void cbfri_CheckedChanged(object sender, EventArgs e)
        {
            if (cbfri.Checked)
            {
                friday = "5";
            }
            else
            {
                friday = "";
            }
        }

        private void cbsat_CheckedChanged(object sender, EventArgs e)
        {
            if (cbsat.Checked)
            {
                saturday = "6";
            }
            else
            {
                saturday = "";
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            scheds.times();
            MessageBox.Show(scheds.timediff);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}