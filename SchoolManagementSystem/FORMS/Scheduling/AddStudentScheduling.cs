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


    public partial class AddStudentScheduling : Form
    {
        string teachid;
        studentSched sched = new studentSched();
        StudentScheduling addDatagrid;
     
        public AddStudentScheduling(StudentScheduling addDatagrid)
        {
            InitializeComponent();
            this.addDatagrid = addDatagrid;
        }

        private void AddStudentScheduling_Load(object sender, EventArgs e)
        {
            displayDataCmb();
        }

        public void displayDataCmb()
        {
            var values = DBContext.GetContext().Query("schedule").Where("status","available").Get();


            foreach (var value in values)
            {
                dgvSched.Rows.Add(value.schedID, value.subjectCode, value.subjectTitle, value.roomID, value.date, value.timeStart, value.timeEnd);
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSched_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            

        }



        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var value = DBContext.GetContext().Query("tuitioncategory").Where("category", comboBox1.Text).First();
            //string getid = value.tuitionCatID.ToString();

            //dgvSched.Rows.Clear();

            //sched.category = getid;

            //dgvSched.Columns[5].DefaultCellStyle.Format = "hh:mm tt";
            //dgvSched.Columns[6].DefaultCellStyle.Format = "hh:mm tt";
            //sched.display();

            //foreach (DataRow Drow in sched.dt.Rows)
            //{
            //    int num = dgvSched.Rows.Add();
            //    dgvSched.Rows[num].Cells[0].Value = Drow["SchedID"].ToString();
            //    dgvSched.Rows[num].Cells[1].Value = Drow["SubjectCode"].ToString();
            //    dgvSched.Rows[num].Cells[2].Value = Drow["SubjectTitle"].ToString();
            //    dgvSched.Rows[num].Cells[3].Value = Drow["RoomName"].ToString();
            //    dgvSched.Rows[num].Cells[4].Value = Drow["Day"].ToString();
            //    dgvSched.Rows[num].Cells[5].Value = Convert.ToDateTime(Drow["Timestart"].ToString());
            //    dgvSched.Rows[num].Cells[6].Value = Convert.ToDateTime(Drow["Timeend"].ToString());
            //    dgvSched.Rows[num].Cells[7].Value = Drow["MaxStudent"].ToString();
            //    dgvSched.Rows[num].Cells[8].Value = Drow["Status"].ToString();
            //    dgvSched.Rows[num].Cells[9].Value = Drow["lablec"].ToString();
            //}
        }

        private void dgvSched_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewRow row in addDatagrid.dgvStudentSched.Rows)
            {
                if ((string)row.Cells[0].Value == dgvSched.SelectedRows[0].Cells[0].Value.ToString())
                {
                    Validator.AlertDanger("Subject existed");
                    return;
                }
            }
            addDatagrid.dgvStudentSched.Rows.Add(dgvSched.SelectedRows[0].Cells[0].Value.ToString(), dgvSched.SelectedRows[0].Cells[1].Value.ToString(),
            dgvSched.SelectedRows[0].Cells[2].Value.ToString(), dgvSched.SelectedRows[0].Cells[3].Value.ToString(), dgvSched.SelectedRows[0].Cells[4].Value,
            dgvSched.SelectedRows[0].Cells[5].FormattedValue.ToString(), dgvSched.SelectedRows[0].Cells[6].FormattedValue.ToString());
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

