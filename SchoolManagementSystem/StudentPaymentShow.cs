using SchoolManagementSystem.FORMS.Scheduling;
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

namespace SchoolManagementSystem
{
    public partial class StudentPaymentShow : Form
    {

        studentPaymentDisplay spd = new studentPaymentDisplay();
        Form1 display;
        public StudentPaymentShow(Form1 display)
        {
            InitializeComponent();
            this.display = display;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var myForm = new Payment(display);
            display.pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            display.pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void StudentPaymentShow_Load(object sender, EventArgs e)
        {
            displayData();
        }

        public void displayData()
        {
            spd.display();

            foreach(DataRow DROW in spd.dt.Rows)
            {
                int num = dgvStudents.Rows.Add();

                dgvStudents.Rows[num].Cells[0].Value = DROW["StudentID"].ToString();
                dgvStudents.Rows[num].Cells[1].Value = DROW["Name"].ToString();
                dgvStudents.Rows[num].Cells[2].Value = DROW["Total"].ToString();
            }
        }
    }
}
