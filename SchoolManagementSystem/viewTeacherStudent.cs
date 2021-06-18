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
namespace SchoolManagementSystem
{
    public partial class viewTeacherStudent : Form
    {

        string value2;
        teacherScheds teach = new teacherScheds();
        public viewTeacherStudent(string val, string val2)
        {
            InitializeComponent();
            label1.Text = val;
            value2 = val2;
            

        }

        private void viewTeacherStudent_Load(object sender, EventArgs e)
        {
            teach.teacherID = value2;
            teach.getSchedID = label1.Text ;
            teach.viewstudent();

            foreach (DataRow Drow in teach.dt.Rows)
            {
                int num = dgvSched.Rows.Add();

                dgvSched.Rows[num].Cells[0].Value = Drow["ID"].ToString();
                dgvSched.Rows[num].Cells[1].Value = Drow["Name"].ToString();
                dgvSched.Rows[num].Cells[2].Value = Drow["Course"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSched_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
