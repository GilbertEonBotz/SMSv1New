using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EonBotzLibrary;
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

            scheds.Schedule();
            
            cbSubjCode.DataSource = scheds.datafill;
            CbRoomNO.DataSource = scheds.datafillroom;
            cbCourse.DataSource = scheds.datafillcourse;

            cbSubjCode.DataSource.Equals("");
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
                    Validator.AlertSuccess("Schedule saved");

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
            catch(Exception)
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
                cbCourse.Text = scheds.course;
                scheds.insertSched();
        }

        private void CbRoomNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                scheds.roomdesc= CbRoomNO.Text;

        }

        private void cbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            scheds.course = cbCourse.Text;
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
        
           dtpTimstart= dateTimePicker1.Value.ToString("H:mm:ss");
        }

        private void cbmon_CheckedChanged(object sender, EventArgs e)
        {
            if(cbmon.Checked)
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