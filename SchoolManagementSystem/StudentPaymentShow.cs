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
        double totaldownpaymentplus;
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

            foreach (DataRow DROW in spd.dt.Rows)
            {
                int num = dgvStudents.Rows.Add();

                dgvStudents.Rows[num].Cells[0].Value = DROW["StudentID"].ToString();
                dgvStudents.Rows[num].Cells[1].Value = DROW["Name"].ToString();
                dgvStudents.Rows[num].Cells[2].Value = DROW["Total"].ToString();
        
            }
        }


        public static double ComputePercentage(double _tuition, string bDecimal, string calFraction, double wholeNum)
        {
            string result = Convert.ToString(_tuition);

            var regex = new System.Text.RegularExpressions.Regex("(?<=[\\.])[0-9]+");
            if (regex.IsMatch(result))
            {
                string decimalPlaces = regex.Match(result).Value;

                if (Convert.ToInt64(decimalPlaces) > 0)
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i] == '.')
                        {
                            bDecimal += result[i - 1];
                        }

                    }
                    calFraction = $"{bDecimal}.{decimalPlaces}";
                }
                else
                {
                }
            }
            else
            {
                calFraction = "0";
            }

            //return Convert.ToDouble(calFraction);
            return wholeNum = Convert.ToDouble(result) - Convert.ToDouble(calFraction);
        }
        public static double ComputeDecimals(double _tuition, string bDecimal, string calFraction, double wholeNum)
        {
            string result = Convert.ToString(_tuition);

            var regex = new System.Text.RegularExpressions.Regex("(?<=[\\.])[0-9]+");
            if (regex.IsMatch(result))
            {
                string decimalPlaces = regex.Match(result).Value;

                if (Convert.ToInt64(decimalPlaces) > 0)
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i] == '.')
                        {
                            bDecimal += result[i - 1];
                        }
                    }
                    calFraction = $"{bDecimal}.{decimalPlaces}";
                }
                else
                {
                }
            }
            else
            {
                calFraction = "0";
            }
            return Convert.ToDouble(calFraction);
        }
        public  void showStudent()
        
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
            myForm.dgv.Rows.Add("nulll", spd.studentdownpayment, spd.remarksFordown, spd.dateForDown);

            double finalss = Convert.ToDouble(spd.totalpaid) + 0;



            led.percent();
             totaldownpaymentplus = Convert.ToDouble(spd.total) + Convert.ToDouble(led.downpayment);
            myForm.textBox15.Text = totaldownpaymentplus.ToString(); 
            myForm.txt1.Text = spd.prelim;
            myForm.txt2.Text = spd.midterm;
            myForm.txt3.Text = spd.semi;
            myForm.txt4.Text = spd.final;
            myForm.billingid = spd.billingid;

            led.percent();
            //
            myForm.txt0.Text = led.downpayment;

            spd.viewtransaction();

            foreach (DataRow DROW in spd.dt.Rows)
            {
                int num = myForm.dgv.Rows.Add();
                myForm.dgv.Rows[num].Cells[0].Value = DROW["paymentid"].ToString();
                myForm.dgv.Rows[num].Cells[1].Value = DROW["paymentanomount"].ToString();
                myForm.dgv.Rows[num].Cells[2].Value = DROW["paymentremarks"].ToString();
                myForm.dgv.Rows[num].Cells[3].Value = DROW["paymentdate"].ToString();

            }

            myForm.lbltotal.Text = spd.totalpaid.ToString();
            double current = Convert.ToDouble(myForm.textBox15.Text) - Convert.ToDouble(spd.totalpaid);
            myForm.txtcurrentBal.Text = current.ToString();

            try
            {

                if (Convert.ToDouble(led.downpayment) <= finalss)
                {
                    amount = finalss - Convert.ToDouble(led.downpayment);

                    myForm.comboBox2.Items.Remove("DOWNPAYMENT");

                    if (Convert.ToDouble(myForm.txt1.Text) <= amount)
                    {

                        amount = amount - Convert.ToDouble(myForm.txt1.Text);
                        myForm.comboBox2.Items.Remove("PRELIM");
                        if (Convert.ToDouble(myForm.txt2.Text) <= amount)
                        {
                            amount = amount - Convert.ToDouble(myForm.txt2.Text);
                            myForm.comboBox2.Items.Remove("MIDTERM");
                            if (Convert.ToDouble(myForm.txt3.Text) <= amount)
                            {
                                amount = amount - Convert.ToDouble(myForm.txt3.Text);

                                myForm.comboBox2.Items.Remove("SEMI-FINAL");
                                if (Convert.ToDouble(myForm.txt4.Text) <= amount)
                                {
                                    amount = amount - Convert.ToDouble(myForm.txt4.Text);
                                    myForm.lbldownpayment.Text = led.downpayment.ToString();
                                    myForm.lblpre.Text = myForm.txt1.Text;
                                    myForm.lblmid.Text = myForm.txt2.Text;
                                    myForm.lblsemi.Text = myForm.txt3.Text;
                                    myForm.lblfin.Text = myForm.txt4.Text;
                                    myForm.comboBox2.Items.Remove("FINALE");
                                }
                                else
                                {
                                    myForm.lbldownpayment.Text = led.downpayment.ToString();
                                    myForm.lblpre.Text = myForm.txt1.Text;
                                    myForm.lblmid.Text = myForm.txt2.Text;
                                    myForm.lblsemi.Text = myForm.txt3.Text;
                                    myForm.lblfin.Text = amount.ToString();
                                }
                            }
                            else
                            {
                                myForm.lbldownpayment.Text = led.downpayment.ToString();
                                myForm.lblpre.Text = myForm.txt1.Text;
                                myForm.lblmid.Text = myForm.txt2.Text;
                                myForm.lblsemi.Text = amount.ToString();
                            }
                        }
                        else
                        {
                            myForm.lbldownpayment.Text = led.downpayment.ToString();
                            myForm.lblpre.Text = myForm.txt1.Text;
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

                    myForm.lbldownpayment.Text = finalss.ToString();
                }
            }
            catch (Exception)
            {

                myForm.txtcurrentBal.Text = spd.total;
            }
        }
        private void dgvStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showStudent();
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

