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
using SqlKata.Compilers;
using EonBotzLibrary;
namespace SchoolManagementSystem
{
    public partial class addTeacherScheduling : Form
    {
        string teachid;
        // teacherSched sched = new teacherSched();
        studentSched sched = new studentSched();
        teacherSched teachDgv;
        public addTeacherScheduling(teacherSched teachDg)
        {
            InitializeComponent();

            this.teachDgv = teachDg;
        }

    
        public void aa()
        {
            var values = DBContext.GetContext().Query("tuitioncategory").Get();


            foreach (var value in values)
            {
                comboBox1.Items.Add(value.category);
            }
        }
        private void addTeacherScheduling_Load(object sender, EventArgs e)
        {
            aa();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvSched.Rows.Clear();
            sched.category = comboBox1.Text;


            dgvSched.Columns[4].DefaultCellStyle.Format = "hh:mm tt";
            dgvSched.Columns[5].DefaultCellStyle.Format = "hh:mm tt";
            sched.display();

            foreach (DataRow Drow in sched.dt.Rows)
            {
                int num = dgvSched.Rows.Add();
                dgvSched.Rows[num].Cells[0].Value = Drow["SchedID"].ToString();
                dgvSched.Rows[num].Cells[1].Value = Drow["SubjectCode"].ToString();
                dgvSched.Rows[num].Cells[2].Value = Drow["SubjectTitle"].ToString();
                dgvSched.Rows[num].Cells[3].Value = Drow["RoomName"].ToString();
                dgvSched.Rows[num].Cells[4].Value = Drow["Timestart"].ToString();
                dgvSched.Rows[num].Cells[5].Value = Drow["Timeend"].ToString();
                dgvSched.Rows[num].Cells[6].Value = Drow["Day"].ToString();
                dgvSched.Rows[num].Cells[7].Value = Drow["MaxStudent"].ToString();
                dgvSched.Rows[num].Cells[8].Value = Drow["Status"].ToString();
                dgvSched.Rows[num].Cells[9].Value = Drow["lablec"].ToString();
            }
        }

        private void dgvSched_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in teachDgv.dgvStudentSched.Rows)
            {
                if ((string)row.Cells[0].Value == dgvSched.SelectedRows[0].Cells[0].Value.ToString())
                {
                    Validator.AlertDanger("Subject existed");
                    return;
                }
            }
            teachDgv.dgvStudentSched.Rows.Add(dgvSched.SelectedRows[0].Cells[0].Value.ToString(), dgvSched.SelectedRows[0].Cells[1].Value.ToString(),
            dgvSched.SelectedRows[0].Cells[2].Value.ToString(), dgvSched.SelectedRows[0].Cells[3].Value.ToString(), dgvSched.SelectedRows[0].Cells[4].Value,
            dgvSched.SelectedRows[0].Cells[5].Value, dgvSched.SelectedRows[0].Cells[6].Value, dgvSched.SelectedRows[0].Cells[7].Value.ToString(),
            dgvSched.SelectedRows[0].Cells[8].Value.ToString(), dgvSched.SelectedRows[0].Cells[9].Value.ToString());
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            
        }
    }
}
