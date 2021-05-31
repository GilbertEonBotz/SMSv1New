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
using MySql.Data.MySqlClient;
namespace SchoolManagementSystem.FORMS.Scheduling
{
    public partial class Payment : Form
    {
        studentPaymentDisplay disp = new studentPaymentDisplay();
        public string billingid;
        Form1 display;
        MySqlCommand cmd;
        MySqlDataReader dr;
        MySqlConnection conn;
        Connection connect = new Connection();
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
     
            disp.studentID = studentid.Text;
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
         




            conn = connect.getcon();
            conn.Open();
            cmd = new MySqlCommand("select sum(b.amount) ,a.total  from Billing a, payment b where a.billingid = b.billingid  and a.billingid ='" + billingid + "'", conn);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
              double number =  Convert.ToDouble(dr[0].ToString()) + Convert.ToDouble(txtAmount.Text);

                if(number > Convert.ToDouble(dr[1].ToString()))
                {
                    MessageBox.Show("please input only equal or less than the total amount");
                }
                else
                {
                    disp.billingid = billingid;
                    MessageBox.Show(billingid);
                    disp.amount = txtAmount.Text;
                    disp.remarks = txtRemarks.Text;
                    disp.status = "paid";
                    disp.paymentMethod = cmbpaymentMethod.Text;

                    disp.insertpayment();
                    MessageBox.Show("success");
                
                }
            }

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {   

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
      
         if (comboBox2.Text =="PRELIM")

            {
                
            }            
            
            
        }
    }
}
