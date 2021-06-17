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
        finalDashboard display;
        MySqlCommand cmd;
        MySqlDataReader dr;
        MySqlConnection conn;
        Connection connect = new Connection();

        double amount = 0;
        double amountprelim = 0;
        double amountmid = 0;
        double amountsemi = 0;
        double amountfinal = 0;
        double amountDown = 0;
        public Payment(finalDashboard display)
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
            if (txtAmount.Text == "")
            {
                MessageBox.Show("please input an amount");
            }
            else
            {
                double number = 0;

                conn = connect.getcon();
                conn.Open();
                cmd = new MySqlCommand("select sum(b.amount) ,a.total  from Billing a, payment b where  b.status ='paid' and a.billingid = b.billingid  and a.billingid ='" + billingid + "'", conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    number = Convert.ToDouble(dr[0].ToString() + 0) + Convert.ToDouble(txtAmount.Text);

                    if (number > Convert.ToDouble(dr[1].ToString()))
                    {
                        disp.billingid = billingid;

                        double total = Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(lblpaymentfor.Text);

                        disp.amount = lblpaymentfor.Text.ToString();
                        disp.remarks = txtRemarks.Text;
                        disp.status = "paid";
                        disp.paymentMethod = cmbpaymentMethod.Text;

                        disp.insertpayment();
                        MessageBox.Show("success");
                    }
                    else
                    {

                        if (checkBox1.Checked)
                        {
                            disp.billingid = billingid;

                            double total = Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(lblpaymentfor.Text);

                            disp.amount = lblpaymentfor.Text.ToString();
                            disp.remarks = txtRemarks.Text;
                            disp.status = "paid";
                            disp.paymentMethod = cmbpaymentMethod.Text;

                            disp.insertpayment();
                            MessageBox.Show("success");

                        }
                        else
                        {
                            if (comboBox2.Text == "FINAL" && Convert.ToDouble(txtAmount.Text) > Convert.ToDouble(lblpaymentfor.Text))
                            {
                                disp.billingid = billingid;
                                disp.amount = lblpaymentfor.Text;
                                disp.remarks = txtRemarks.Text;
                                disp.status = "paid";
                                disp.paymentMethod = cmbpaymentMethod.Text;

                                disp.insertpayment();
                                MessageBox.Show("success");
                            }
                            else
                            {
                                disp.billingid = billingid;

                                disp.amount = txtAmount.Text;
                                disp.remarks = txtRemarks.Text;
                                disp.status = "paid";
                                disp.paymentMethod = cmbpaymentMethod.Text;

                                disp.insertpayment();
                                MessageBox.Show("success");
                            }

                        }
                    }
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

            if (txtAmount.Text == "")
            {
                txtchange.Text = "00.00";
            }

            else if (Convert.ToDouble(txtAmount.Text) < Convert.ToDouble(lblpaymentfor.Text))
            {
                txtchange.Text = "00.00";
            }
            else
            {
                //if (Convert.ToDouble(txtAmount.Text) > Convert.ToDouble(lblpaymentfor.Text))
                //{
                //    txtAmount.Text = lblpaymentfor.Text;
                //}    
                double aa = Convert.ToDouble(lblpaymentfor.Text) - Convert.ToDouble(txtAmount.Text);
                txtchange.Text = aa.ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "DOWNPAYMENT")
            {
                if (Convert.ToDouble(txt0.Text) > Convert.ToDouble(lbldownpayment.Text))
                {
                    amountDown = Convert.ToDouble(txt0.Text) - Convert.ToDouble(lbldownpayment.Text);
                    lblpaymentfor.Text = amountDown.ToString();
                }
                else
                {
                    lblpaymentfor.Text = 0.ToString();
                }
            }
            else if (comboBox2.Text == "PRELIM")
            {
                if (Convert.ToDouble(txt0.Text) + Convert.ToDouble(txt1.Text) > Convert.ToDouble(lbldownpayment.Text) + Convert.ToDouble(lblpre.Text))
                {
                    amountprelim = (Convert.ToDouble(txt1.Text) + (Convert.ToDouble(txt0.Text)) - (Convert.ToDouble(lbldownpayment.Text) + Convert.ToDouble(lblpre.Text)));
                    lblpaymentfor.Text = amountprelim.ToString();

                }
                else
                {
                    lblpaymentfor.Text = 0.ToString();
                }
            }
            else if (comboBox2.Text == "MIDTERM")
            {
                if (Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt0.Text) > Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text)))
                {
                    //amountmid = amount -Convert.ToDouble(lblmid.Text);
                    //amount = amountmid;
                    amountmid = (Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + (Convert.ToDouble(txt0.Text)) - (Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text))));

                    lblpaymentfor.Text = amountmid.ToString();
                }
                else
                {
                    lblpaymentfor.Text = "0";
                }

            }

            else if (comboBox2.Text == "SEMI-FINAL")
            {
                if (Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt0.Text) > Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text)))
                {
                    //amountmid = amount -Convert.ToDouble(lblmid.Text);

                    //amount = amountmid;
                    amountsemi = (Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + (Convert.ToDouble(txt0.Text)) - (Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text))));


                    lblpaymentfor.Text = amountsemi.ToString();
                }
                else
                {
                    lblpaymentfor.Text = "0";
                }

            }
            else if (comboBox2.Text == "FINAL")
            {
                if (Convert.ToDouble(txt0.Text) + Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt4.Text) > (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text) + Convert.ToDouble(lbldownpayment.Text) + Convert.ToDouble(lblfin.Text)))
                {


                    amountfinal = (Convert.ToDouble(txt0.Text) + Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt4.Text)) - (Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text) + Convert.ToDouble(lblfin.Text)));


                    lblpaymentfor.Text = amountfinal.ToString();
                }
                else
                {
                    lblpaymentfor.Text = "0";
                }

            }






        }

        private void txtAmount_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn = connect.getcon();
            conn.Open();
            cmd = new MySqlCommand("update payment set status ='void' where paymentid = '" + dgv.SelectedRows[0].Cells[0].Value + "'", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("succesfully void");
        }
    }
}
