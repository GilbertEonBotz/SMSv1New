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
namespace SchoolManagementSystem
{


    public partial class AddStudentScheduling : Form
    {
        string teachid;
        studentSched sched = new studentSched();
        StudentScheduling addDatagrid;
        string allScheds;
        Connection connect = new Connection();
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        int aa;
        string numberOfStudent;
        public AddStudentScheduling(StudentScheduling addDatagrid)
        {
            InitializeComponent();
            this.addDatagrid = addDatagrid;
        }

        private void AddStudentScheduling_Load(object sender, EventArgs e)
        {
            displayDataCmb();

            //var getSched = DBContext.GetContext().Query("studentSched").Where("studentID", "1").First();
            //double[] catID = { };

            //int strucID = 0;
            //int getCategoryID = 0;
            //string str = getSched.schedId;
            //var words = str.Split(' ');

            //for (int i = 0; i < words.Length; i++)
            //{

            //    allScheds = words[i];
            //    MessageBox.Show(allScheds);






            //}
        }

        public void displayDataCmb()
        {
            string wew = "";
            string allScheds = "";
            conn = connect.getcon();
            conn.Open();
            cmd = new MySqlCommand("SELECT schedid FROM studentSched", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                allScheds = dr[0].ToString();
               var  words = allScheds.Split(' ');
              

                for (int i = 0; i < Convert.ToInt32(words.Length); i++)
                {
                   wew += words[i]+" ";
               
                }
            }
   
            var wordd = wew.Split(' ');

            for (int i = 0; i < Convert.ToInt32(wordd.Length);)
            {
               

                conn = connect.getcon();
                conn.Open();
                cmd = new MySqlCommand("select a.schedid, a.subjectcode, a.maxStudent - count(b.schedId)  from schedule a left join studentSched b on a.schedid = '" + wordd[i] + "' and b.schedID regexp '[1 2 3]' group by a.schedid  ", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dgvSched.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                }

                MessageBox.Show(wordd[i].ToString());
                i++;
           
             
            }

    






            //for (int i = 0; i < Convert.ToInt32(words.Length); i++)
            //{




            //    // conn = connect.getcon();
            //    //conn.Open();
            //    //cmd = new MySqlCommand("select a.schedid, a.subjectcode, a.maxStudent - count(b.schedId)  from schedule a left join studentSched b on a.schedid = '" + allScheds + "' and b.schedID regexp '[" + allScheds + "]' group by a.schedid  ", conn);
            //    //dr = cmd.ExecuteReader();
            //    //while (dr.Read())
            //    //{
            //    //    dgvSched.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            //    //}
            //}

        }
 

        //conn = connect.getcon();
        //conn.Open();
        //cmd = new MySqlCommand("select a.schedid, a.subjectcode, a.maxStudent -count(b.schedId)  from schedule a , studentSched b where a.schedid = '" + numberOfStudent + "'  and b.schedId ='" + numberOfStudent + "'  group by a.schedid ", conn);
        //dr = cmd.ExecuteReader();
        //while (dr.Read())
        //{
        //    dgvSched.Rows.Add(dr[0].ToString(),dr[1].ToString(), dr[2].ToString());
        //}


        //var values = DBContext.GetContext().Query("schedule").Where("status","available").Get();


        //foreach (var value in values)
        //{
        //    dgvSched.Rows.Add(value.schedID, value.subjectCode, value.subjectTitle, value.roomID, value.date, value.timeStart, value.timeEnd);
        //}


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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


        private void textBox1_TextChanged(object sender, EventArgs e)
        {




            dgvSched.Rows.Clear();
            conn = connect.getcon();
            conn.Open();
            cmd = new MySqlCommand("select schedid , subjectcode,subjectTitle, roomID ,date,timestart,timeend,maxstudent from schedule where status  ='available' and subjectcode like '%" + textBox1.Text + "%' or subjectTitle like '%" + textBox1.Text + "%'", conn);

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvSched.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
        }
    }
}

