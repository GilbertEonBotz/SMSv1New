﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlKata.Execution;
using EonBotzLibrary;
namespace SchoolManagementSystem
{
    public partial class viewTeacherSched : Form
    {
        public viewTeacherSched()
        {
            InitializeComponent();
        }

        private void viewTeacherSched_Load(object sender, EventArgs e)
        {
            var values = DBContext.GetContext().Query("teachers").Get();

            foreach (var value in values)
            {
                comboBox1.Items.Add(value.teacherId);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            teacherScheds sched = new teacherScheds();
            dgvSched.Rows.Clear();


            var values = DBContext.GetContext().Query("teachersched").Where("teacherId", comboBox1.Text).First();

            string str = values.schedId;
            var words = str.Split(' ');

            for (int i = 1; i < words.Length; i++)
            {
                // dataGridView1.Rows.Add(words[i],aa,name);
                
                sched.subjectcode = (words[i].ToString());
                //  MessageBox.Show(words[i]);
                dgvSched.Columns[4].DefaultCellStyle.Format = "hh:mm tt";
                dgvSched.Columns[5].DefaultCellStyle.Format = "hh:mm tt";
                sched.viewteachsubj();
                foreach (DataRow Drow in sched.dt.Rows)
                {
                    int num = dgvSched.Rows.Add();
                    dgvSched.Rows[num].Cells[0].Value = Drow["SchedID"].ToString();
                    dgvSched.Rows[num].Cells[1].Value = Drow["SubjectCode"].ToString();
                    dgvSched.Rows[num].Cells[2].Value = Drow["Room"].ToString();
                    dgvSched.Rows[num].Cells[3].Value = Drow["Day"].ToString();
                    dgvSched.Rows[num].Cells[4].Value = Convert.ToDateTime(Drow["TimeStart"].ToString());
                    dgvSched.Rows[num].Cells[5].Value = Convert.ToDateTime(Drow["TimeEnd"].ToString());
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvSched_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            viewTeacherStudent v = new viewTeacherStudent(dgvSched.SelectedRows[0].Cells[0].Value.ToString(), comboBox1.Text);
            v.ShowDialog();
        }

        private void dgvSched_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
