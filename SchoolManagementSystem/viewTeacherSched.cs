using System;
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
            dataGridView1.Rows.Clear();

      
            //var values = DBContext.GetContext().Query("student").Where("studentId", comboBox1.Text).Get();

            //foreach (var value in values)
            //{
            //    txtName.Text = value.firstname();
            //}

            var values = DBContext.GetContext().Query("teachersched").Where("teacherId", comboBox1.Text).First();

            string str = values.schedId;
            int name = values.teacherId;
            int aa = values.teacherSchedID;
            var words = str.Split(' ');

            for (int i = 1; i < words.Length; i++)
            {
              
                // dataGridView1.Rows.Add(words[i],aa,name);
                teacherScheds sched = new teacherScheds();
                sched.subjectcode = (words[i].ToString());
                //  MessageBox.Show(words[i]);
                dataGridView1.Columns[3].DefaultCellStyle.Format = "hh:mm tt";
                dataGridView1.Columns[4].DefaultCellStyle.Format = "hh:mm tt";
                sched.viewteachsubj();
                foreach (DataRow Drow in sched.dt.Rows)
                {
                    int num = dataGridView1.Rows.Add();
                    dataGridView1.Rows[num].Cells[1].Value = Drow["subjectcode"].ToString();
                    dataGridView1.Rows[num].Cells[2].Value = Drow["date"].ToString();
                    dataGridView1.Rows[num].Cells[3].Value = Drow["timstart"].ToString();
                    dataGridView1.Rows[num].Cells[4].Value = Drow["timeend"].ToString();
                    dataGridView1.Rows[num].Cells[5].Value = Drow["room"].ToString();
                    dataGridView1.Rows[num].Cells[0].Value = Drow["schedid"].ToString();


                }

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            viewTeacherStudent v = new viewTeacherStudent(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            v.ShowDialog();
        }
    }
}
