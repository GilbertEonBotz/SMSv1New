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
    public partial class Sched : Form
    {
        public Sched()
        {
            InitializeComponent();
        }

        private void Sched_Load(object sender, EventArgs e)
        {
            schedule sched = new schedule();

            dgvSched.Columns[6].DefaultCellStyle.Format = "hh:mm tt";
            dgvSched.Columns[7].DefaultCellStyle.Format = "hh:mm tt";
            sched.viewsched();

            dgvSched.Rows.Clear();
            foreach (DataRow Drow in sched.dt.Rows)
            {
                int num = dgvSched.Rows.Add();

                dgvSched.Rows[num].Cells[0].Value = Drow["SchedID"].ToString();
                dgvSched.Rows[num].Cells[1].Value = Drow["SubjectCode"].ToString();
                dgvSched.Rows[num].Cells[2].Value = Drow["roomID"].ToString();
                dgvSched.Rows[num].Cells[3].Value = Drow["courseID"].ToString();
                dgvSched.Rows[num].Cells[4].Value = Drow["date"].ToString();
                dgvSched.Rows[num].Cells[5].Value = Drow["maxStudent"].ToString();
                dgvSched.Rows[num].Cells[6].Value = Convert.ToDateTime(Drow["time start"].ToString());
                dgvSched.Rows[num].Cells[7].Value = Convert.ToDateTime(Drow["time end"].ToString());
                dgvSched.Rows[num].Cells[8].Value = Drow["status"].ToString();
            }

            foreach (DataGridViewRow Myrow in dgvSched.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                if (Convert.ToString(Myrow.Cells[6].Value) == "available")// Or your condition 
                {
                    // Myrow.DefaultCellStyle.BackColor = Color.IndianRed;
                    Myrow.DefaultCellStyle.BackColor = Color.White;
                }

                else if (Convert.ToString(Myrow.Cells[6].Value) == "full")
                {
                    Myrow.DefaultCellStyle.BackColor = Color.Orange;
                }
            }
        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            addSchedule s = new addSchedule();
            s.Show();
        }
    }
}
