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


            spd.viewPayment();

            myForm.textBox15.Text = spd.total;

            myForm.txt1.Text = spd.prelim;
            myForm.txt2.Text = spd.midterm;
            myForm.txt3.Text = spd.semi;
            myForm.txt4.Text = spd.final;
            myForm.billingid = spd.billingid;
         
  
          
            spd.viewtransaction();
          

            foreach (DataRow DROW in spd.dt.Rows)
            {
                int num =myForm.dgv.Rows.Add();

               myForm.dgv.Rows[num].Cells[0].Value = DROW["amount"].ToString();
                myForm.dgv.Rows[num].Cells[1].Value = DROW["remarks"].ToString();
                myForm.dgv.Rows[num].Cells[2].Value = DROW["date"].ToString();

            }

            spd.viewPaymentDetailed();
            myForm.lbltotal.Text = spd.totalpaid;
         
            myForm.txtcurrentBal.Text = spd.currentbalance;

            if(myForm.txtcurrentBal.Text =="0.00")
            {
                myForm.button1.Enabled = false;
            }

            Convert.ToDouble(spd.prelim);
            Convert.ToDouble(spd.midterm);
            Convert.ToDouble(spd.semi);
            try
            {
                double finalss = Convert.ToDouble(spd.totalpaid);


                Convert.ToDouble(spd.final);


                for (double i = 0; i <= finalss; i++)
                {
                    if (Convert.ToDouble(spd.prelim) <= finalss)
                    {
                        amount = finalss - Convert.ToDouble(spd.prelim);
                     
                        myForm.comboBox2.Items.Remove("PRELIM");

                        if (Convert.ToDouble(spd.midterm) <= amount)
                        {

                            amount = amount - Convert.ToDouble(spd.midterm);
                            myForm.comboBox2.Items.Remove("MIDTERM");
                            if (Convert.ToDouble(spd.semi) <= amount)
                            {
                             
                                amount = amount - Convert.ToDouble(spd.semi);
                                myForm.comboBox2.Items.Remove("SEMI-FINAL");
                                if (Convert.ToDouble(spd.final) <= amount)
                                {
                                    amount = amount - Convert.ToDouble(spd.final);
                                    myForm.lblpre.Text = spd.prelim;
                                    myForm.lblmid.Text = spd.midterm;
                                    myForm.lblsemi.Text = spd.semi;
                                    myForm.lblfin.Text = spd.final;
                                    myForm.comboBox2.Items.Remove("FINAL");
                                    myForm.panel3.Controls.Clear();
                         
                                }
                                else
                                {
                                    myForm.lblpre.Text = spd.prelim;
                                    myForm.lblmid.Text = spd.midterm;
                                    myForm.lblsemi.Text = spd.semi;
                                    myForm.lblfin.Text = Convert.ToString(amount);
                                }
                            }
                            else
                            {
                                myForm.lblpre.Text = spd.prelim;
                                myForm.lblmid.Text = spd.midterm;
                                myForm.lblsemi.Text = Convert.ToString(amount);
                            }
                        }
                        else
                        {
                            myForm.lblpre.Text = spd.prelim;
                            myForm.lblmid.Text = Convert.ToString(amount);
                        }
                    }
                    else
                    {

                        myForm.lblpre.Text = Convert.ToString(finalss);
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

