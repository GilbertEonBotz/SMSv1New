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
namespace SchoolManagementSystem.FORMS.Scheduling
{
    public partial class Payment : Form
    {
        Form1 display;
        public Payment(Form1 display)
        {
            InitializeComponent();
            this.display = display;
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            displayData();

        }

        public void displayData()
        {
            studentPaymentDisplay disp = new studentPaymentDisplay();
            disp.studentID = textBox1.Text;
            disp.viewPayment();

            txt1.Text = disp.prelim;

            txt2.Text = disp.midterm;

            txt3.Text = disp.semi;

            txt4.Text = disp.final;

         
            lbltotal.Text = disp.total;
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label11_Click(object sender, EventArgs e)
        {
       

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            studentPaymentDisplay disp = new studentPaymentDisplay();
            disp.studentID = textBox1.Text;
            disp.viewPayment();

            lblpre.Text = disp.prelim;
            lblmid.Text = disp.midterm;
            lblsemi.Text = disp.semi;
            lblfin.Text = disp.final;
            lbltotal.Text = disp.total;

        }
    }
}
