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
using SqlKata.Execution;

namespace SchoolManagementSystem
{
    public partial class StudentPaymentShow : Form
    {
        string id;
        studentPaymentDisplay spd = new studentPaymentDisplay();
        finalDashboard display;
        double amount;
        ledgerPercent led = new ledgerPercent();
        string studentid;
        public StudentPaymentShow(finalDashboard display)
        {
            InitializeComponent();
            this.display = display;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
        
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

        private void dgvStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          

            var myForm = new Payment(display);
            display.pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            display.pnlShow.Controls.Add(myForm);
            myForm.Show();
            myForm.txtLastname.Text = dgvStudents.SelectedRows[0].Cells[1].Value.ToString();
            myForm.studentid.Text = dgvStudents.SelectedRows[0].Cells[0].Value.ToString();
 
            spd.studentID = dgvStudents.SelectedRows[0].Cells[0].Value.ToString();
            led.percent();
            studentid = spd.studentID;
            spd.viewPayment();
            spd.viewPaymentDetailed();
            spd.studentDOwn();


            double finalss = Convert.ToDouble(spd.totalpaid) + 0;


            MessageBox.Show(spd.totalpaid.ToString());
            double minus = Convert.ToDouble(spd.total)- Convert.ToDouble(led.downpayment) ;
            double first;
            double second;
            double third;
            double fourth;
            first = minus * Convert.ToDouble(led.prelim);
            second = minus * Convert.ToDouble(led.midterm);
            third = minus * Convert.ToDouble(led.semi);
            fourth = minus * Convert.ToDouble(led.finals);


            myForm.textBox15.Text = spd.total;
            myForm.txt1.Text =  first.ToString();
            myForm.txt2.Text = second.ToString();
            myForm.txt3.Text = third.ToString();
            myForm.txt4.Text = fourth.ToString();
            myForm.billingid = spd.billingid;

            led.percent();
            //
            myForm.txt0.Text = led.downpayment;
          
            spd.viewtransaction();
  

            foreach (DataRow DROW in spd.dt.Rows)
            {
                int num =myForm.dgv.Rows.Add();

               myForm.dgv.Rows[num].Cells[0].Value = DROW["amount"].ToString();
                myForm.dgv.Rows[num].Cells[1].Value = DROW["remarks"].ToString();
                myForm.dgv.Rows[num].Cells[2].Value = DROW["date"].ToString();

            }

         //   spd.viewPaymentDetailed();
            myForm.lbltotal.Text = spd.totalpaid.ToString();
            double current = Convert.ToDouble(myForm.textBox15.Text) - Convert.ToDouble(spd.totalpaid) ;
            myForm.txtcurrentBal.Text = current.ToString();

            //if(myForm.txtcurrentBal.Text =="0.00")
            //{
            //    myForm.button1.Enabled = false;
            //}


           

            try
            {
                //spd.studentDOwn();
          

                //double finalss = Convert.ToDouble(spd.totalpaid) + Convert.ToDouble(spd.studentdownpayment);


                //MessageBox.Show(finalss.ToString());


                for (double i = 0; i <= finalss; i++)
                {
                    if (Convert.ToDouble(led.downpayment) <= finalss)
                    {
                 
                        amount = finalss - Convert.ToDouble(led.downpayment);
                     
                        myForm.comboBox2.Items.Remove("DOWNPAYMENT");

                        if (Convert.ToDouble(first) <= amount)
                        {

                            amount = amount - first;
                            myForm.comboBox2.Items.Remove("PRELIM");
                            if (Convert.ToDouble(second) <= amount)
                            {
                             
                                amount = amount - second;
                                myForm.comboBox2.Items.Remove("MIDTERM");
                                if (Convert.ToDouble(third) <= amount)
                                {
                                    amount = amount -third;
                               
                                    myForm.comboBox2.Items.Remove("SEMI-FINAL");
                                    if(Convert.ToDouble(fourth) <= amount)
                                    {
                                        amount = amount - fourth;
                                        myForm.lbldownpayment.Text = led.downpayment.ToString();
                                        myForm.lblpre.Text = first.ToString();
                                        myForm.lblmid.Text = second.ToString();
                                        myForm.lblsemi.Text = third.ToString();
                                        myForm.lblfin.Text = fourth.ToString();
                                        myForm.comboBox2.Items.Remove("FINALE");
                                    }
                                    else
                                    {
                                        myForm.lbldownpayment.Text = led.downpayment.ToString();
                                        myForm.lblpre.Text = first.ToString();
                                        myForm.lblmid.Text = second.ToString();
                                        myForm.lblsemi.Text = third.ToString();
                                        myForm.lblfin.Text = amount.ToString();

                                    }
                                
                         
                                }
                                else
                                {
                                    myForm.lbldownpayment.Text = led.downpayment.ToString();
                                    myForm.lblpre.Text = first.ToString();
                                    myForm.lblmid.Text = second.ToString();
                                    myForm.lblsemi.Text = amount.ToString();


                                }
                            }
                            else
                            {
                                myForm.lbldownpayment.Text = led.downpayment.ToString();
                                myForm.lblpre.Text = first.ToString();
                                myForm.lblmid.Text = amount.ToString();
                            }
                        }
                        else
                        {
                            myForm.lbldownpayment.Text = led.downpayment.ToString();
                            myForm.lblpre.Text = amount.ToString();
                        }
                    }
                    else
                    {

                        myForm.lbldownpayment.Text = finalss.ToString() ;
                    }


                }
            }
            catch(Exception)
            {
         
                myForm.txtcurrentBal.Text = spd.total;
            }
            


        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

