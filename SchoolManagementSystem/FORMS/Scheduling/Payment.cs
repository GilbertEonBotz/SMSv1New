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
using Microsoft.Reporting.WinForms;
using SchoolManagementSystem.UITools;


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
        string number2;
        double amount;
        double amountprelim;
        double amountmid;
        double amountsemi = 0;
        double amountfinal = 0;
        double amountDown = 0;
        double amountFull = 0;
        double newtotal = 0;
        double discount = 0;
        double number;
        double total;



        public Payment(finalDashboard display)
        {
            InitializeComponent();
            this.display = display;
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            displayData();
            txtAmount.KeyPress += Validator.ValidateKeypressNumber;
            
        }

        public void displayData()
        {

            disp.studentID = studentid.Text;
            disp.viewPayment();

            // txt5.Text = disp.fullpayment;
            
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
        public void printShow()
        {

            dgv.Rows.Clear();
            //txtTotal.Text = "";
            //txtAmount.Text = "";
            //txtchange.Text = "00.00";

            comboBox2.Text.Trim();

            disp.studentID = studentid.Text;

            disp.studentDOwn();
            dgv.Rows.Add("0", disp.studentfullpayment, disp.remarksFordown, disp.dateForDown);

            disp.billingid = billingid;
            disp.viewtransaction();
            foreach (DataRow Drow in disp.dt.Rows)
            {
                int num = dgv.Rows.Add();
                dgv.Rows[num].Cells[0].Value = Drow["paymentid"].ToString();
                dgv.Rows[num].Cells[1].Value = Drow["paymentanomount"].ToString();
                dgv.Rows[num].Cells[2].Value = Drow["paymentremarks"].ToString();
                dgv.Rows[num].Cells[3].Value = Drow["paymentdate"].ToString();

            }
        }
        ReportDataSource rsRpt = new ReportDataSource();
        ReportDataSource rsCourse = new ReportDataSource();
        ReportDataSource rsPaid = new ReportDataSource();

        public void viewOr()
        {
            try
            {
                ReceiptViewer frm = new ReceiptViewer();
                var maxOr = DBContext.GetContext().Query("payment").Where("billingID", billingid).OrderByDesc("paymentID").First();

                List<Receipt> rcpt = new List<Receipt>();
                rcpt.Clear();

                if (checkBox1.Checked)
                {
                    
                    rcpt.Add(new Receipt
                    {
                        currentBalance = Convert.ToDouble(txtcurrentBal.Text),
                        receiveAmt = Convert.ToDouble(txtAmount.Text),
                        change = Convert.ToDouble(txtchange.Text),
                        name = txtLastname.Text,
                        orNo = Convert.ToInt32(maxOr.orNumber),
                        remarks = txtRemarks.Text,
                        studentNo = studentid.Text
                    });

                    List<PaymentCourse> courseList = new List<PaymentCourse>();
                    courseList.Clear();
                    courseList.Add(new PaymentCourse
                    {
                        course = txtGender.Text
                    });

                    List<TotalPaid> totPaid = new List<TotalPaid>();
                    totPaid.Clear();
                    totPaid.Add(new TotalPaid
                    {
                        paid = Convert.ToDouble(lbltotal.Text)
                    });

                    rsRpt.Name = "DataSet1";
                    rsRpt.Value = rcpt;
                    rsCourse.Name = "DataSet2";
                    rsCourse.Value = courseList;
                    rsPaid.Name = "DataSet3";
                    rsPaid.Value = totPaid;

                    frm.reportViewer1.LocalReport.DataSources.Clear();
                    frm.reportViewer1.LocalReport.DataSources.Add(rsRpt);
                    frm.reportViewer1.LocalReport.DataSources.Add(rsCourse);
                    frm.reportViewer1.LocalReport.DataSources.Add(rsPaid);
                    frm.reportViewer1.LocalReport.ReportEmbeddedResource = "SchoolManagementSystem.Report4.rdlc";
                    frm.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    frm.reportViewer1.ZoomMode = ZoomMode.Percent;
                    frm.reportViewer1.ZoomPercent = 100;
                    FormFade.FadeForm(this, frm);
                }
                else
                {
                    rcpt.Add(new Receipt
                    {
                        currentBalance = Convert.ToDouble(txtcurrentBal.Text),
                        receiveAmt = Convert.ToDouble(txtAmount.Text),
                        change = 0.00,
                        name = txtLastname.Text,
                        orNo = Convert.ToInt32(maxOr.orNumber),
                        remarks = txtRemarks.Text,
                        studentNo = studentid.Text
                    });

                    List<PaymentCourse> courseList = new List<PaymentCourse>();
                    courseList.Clear();
                    courseList.Add(new PaymentCourse
                    {
                        course = txtGender.Text
                    });

                    List<TotalPaid> totPaid = new List<TotalPaid>();
                    totPaid.Clear();
                    totPaid.Add(new TotalPaid
                    {
                        paid = Convert.ToDouble(lbltotal.Text)
                    });

                    rsRpt.Name = "DataSet1";
                    rsRpt.Value = rcpt;
                    rsCourse.Name = "DataSet2";
                    rsCourse.Value = courseList;
                    rsPaid.Name = "DataSet3";
                    rsPaid.Value = totPaid;

                    frm.reportViewer1.LocalReport.DataSources.Clear();
                    frm.reportViewer1.LocalReport.DataSources.Add(rsRpt);
                    frm.reportViewer1.LocalReport.DataSources.Add(rsCourse);
                    frm.reportViewer1.LocalReport.DataSources.Add(rsPaid);
                    frm.reportViewer1.LocalReport.ReportEmbeddedResource = "SchoolManagementSystem.Report4.rdlc";
                    frm.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    frm.reportViewer1.ZoomMode = ZoomMode.Percent;
                    frm.reportViewer1.ZoomPercent = 100;
                    FormFade.FadeForm(this, frm);
                }
                
            }
            catch (Exception)
            {

            }
          
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            insert();
            printShow();
            showw();
            viewOr();

            txtAmount.Text = "";
            txtRemarks.Text = "";
            txtTotal.Text = "0.00";
            
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value.ToString() == "0")
                {
                    foreach (var cell in row.Cells)
                    {
                        DataGridViewLinkCell linkCell = cell as DataGridViewLinkCell;

                        if (linkCell != null)
                        {
                            linkCell.UseColumnTextForLinkValue = false;
                        }
                    }
                }
            }
        }

        public void insert()
        {
            disp.billingid = billingid;
            disp.studentID = studentid.Text;
            disp.viewPayment();
            disp.studentDOwn();

            conn = connect.getcon();
            conn.Open();
            cmd = new MySqlCommand("select sum(a.amount), (select b.total from Billing b where b.billingid = '" + billingid + "') from payment a , Billing b where a.status ='paid'", conn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //number = Convert.ToDouble(dr[0]) + Convert.ToDouble(disp.studentdownpayment) + Convert.ToDouble(txtAmount.Text);
                number = Convert.ToDouble(dr[0].ToString() + 0) + Convert.ToDouble(disp.studentdownpayment) + Convert.ToDouble(txtAmount.Text);
                if (number > Convert.ToDouble(dr[1].ToString()))
                {
                    total = Convert.ToDouble(disp.total) - Convert.ToDouble(dr[0].ToString() + 0) - Convert.ToDouble(disp.studentdownpayment);

                    disp.amount = total.ToString();
                    disp.remarks = txtRemarks.Text;
                    disp.status = "paid";
                    disp.paymentMethod = cmbpaymentMethod.Text;
                    disp.insertpayment();
                    comboBox2.Enabled = true;
                }
                else if (checkBox1.Checked)
                {
                    if (checkBox1.Checked && txtchange.Text == "00.00")
                    {
                        disp.amount = txtAmount.Text.ToString();
                        disp.remarks = txtRemarks.Text;
                        disp.status = "paid";
                        disp.paymentMethod = cmbpaymentMethod.Text;
                        disp.insertpayment();
                    }
                    else
                    {
                        if (txtchange.Text == "00.00")
                        {
                            disp.amount = txtAmount.Text.ToString();
                            disp.remarks = txtRemarks.Text;
                            disp.status = "paid";
                            disp.paymentMethod = cmbpaymentMethod.Text;
                            disp.insertpayment();
                        }
                        else
                        {
                            disp.amount = txtTotal.Text.ToString();
                            disp.remarks = txtRemarks.Text;
                            disp.status = "paid";
                            disp.paymentMethod = cmbpaymentMethod.Text;
                            disp.insertpayment();
                        }
                    }
                }
                else
                {
                    if (comboBox2.Text == "FINAL" && Convert.ToDouble(txtAmount.Text) > Convert.ToDouble(txtTotal.Text))
                    {
                        disp.billingid = billingid;
                        disp.amount = txtTotal.Text;
                        disp.remarks = txtRemarks.Text;
                        disp.status = "paid";
                        disp.paymentMethod = cmbpaymentMethod.Text;
                        disp.insertpayment();
                        comboBox2.SelectedIndex = 1;
                        comboBox2.Enabled = true;
                    }
                    else
                    {
                        disp.billingid = billingid;
                        disp.amount = txtAmount.Text;
                        disp.remarks = txtRemarks.Text;
                        disp.status = "paid";
                        disp.paymentMethod = cmbpaymentMethod.Text;
                        comboBox2.SelectedItem = 1;
                        disp.insertpayment();
                    }
                }
            }
            conn.Close();
            dr.Close();
        }


        public void showw()
        {

           ledgerPercent led = new ledgerPercent();
            led.percent();
            disp.viewPayment();
            disp.viewPaymentDetailed();
            disp.studentDOwn();


            double finalss = Convert.ToDouble(disp.totalpaid) + 0;
            //double current = Convert.ToDouble(textBox15.Text) - Convert.ToDouble(disp.totalpaid);
            textBox15.Text = (Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt4.Text)).ToString();
            lbltotal.Text = disp.totalpaid.ToString();
            txtcurrentBal.Text = lblfullpayment.ToString();
            try
            {

                
                if (Convert.ToDouble(led.downpayment) <= finalss)
                {
                    amount = finalss - Convert.ToDouble(led.downpayment);

                    comboBox2.Items.Remove("DOWNPAYMENT");

                    if (Convert.ToDouble(txt1.Text) <= amount)
                    {
                        amount = amount - Convert.ToDouble(txt1.Text);
                        comboBox2.Items.Remove("PRELIM");
                        if (Convert.ToDouble(txt2.Text) <= amount)
                        {
                            amount = amount - Convert.ToDouble(txt2.Text);
                            comboBox2.Items.Remove("MIDTERM");
                            if (Convert.ToDouble(txt3.Text) <= amount)
                            {
                                amount = amount - Convert.ToDouble(txt3.Text);

                                comboBox2.Items.Remove("SEMI-FINAL");
                                if (Convert.ToDouble(txt4.Text) <= amount)
                                {
                                    amount = amount - Convert.ToDouble(txt4.Text);
                                    lblfullpayment.Text = led.fullpayment.ToString();
                                    lbldownpayment.Text = led.downpayment.ToString();
                                    lblpre.Text = txt1.Text;
                                    lblmid.Text = txt2.Text;
                                    lblsemi.Text = txt3.Text;
                                    lblfin.Text = txt4.Text;
                                    comboBox2.Items.Remove("FINAL");

                                }
                                else
                                {
                                    lblfullpayment.Text = led.fullpayment.ToString();
                                    lbldownpayment.Text = led.downpayment.ToString();
                                    lblpre.Text = txt1.Text;
                                    lblmid.Text = txt2.Text;
                                    lblsemi.Text = txt3.Text;
                                    lblfin.Text = amount.ToString("F2");
                                }
                            }
                            else
                            {
                                lblfullpayment.Text = led.fullpayment.ToString();
                                lbldownpayment.Text = led.downpayment.ToString();
                                lblpre.Text = txt1.Text;
                                lblmid.Text = txt2.Text;
                                lblsemi.Text = amount.ToString("F2");
                            }
                        }
                        else
                        {
                            lblfullpayment.Text = led.fullpayment.ToString();
                            lbldownpayment.Text = led.downpayment.ToString();
                            lblpre.Text = txt1.Text;
                            lblmid.Text = amount.ToString("F2");
                        }
                    }
                    else
                    {
                        lblfullpayment.Text = led.fullpayment.ToString();
                        lbldownpayment.Text = led.downpayment.ToString();
                        lblpre.Text = amount.ToString("F2");
                    }
                }
                else
                {
                    lblfullpayment.Text = led.fullpayment.ToString();
                    lbldownpayment.Text = finalss.ToString("F2");
                    
                }
            }
            catch (Exception)
            {

                txtcurrentBal.Text = disp.total;
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

            try
            {
                if (string.IsNullOrEmpty(txtAmount.Text))
                {
                    txtchange.Text = "00.00";
                }

                else if (Convert.ToDouble(txtAmount.Text) < Convert.ToDouble(txtTotal.Text))
                {
                    txtchange.Text = "00.00";
                }
                else
                {
                    //if (Convert.ToDouble(txtAmount.Text) > Convert.ToDouble(lblpaymentfor.Text))
                    //{
                    //    txtAmount.Text = lblpaymentfor.Text;
                    //}    
                    double change = Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTotal.Text);
                    txtchange.Text = change.ToString();
                }
            }
            catch (Exception)
            {

            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "FULLPAYMENT")
            {
                if (Convert.ToDouble(txt0.Text) + Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt4.Text) + Convert.ToDouble(txt5.Text) > (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text) + Convert.ToDouble(lbldownpayment.Text) + Convert.ToDouble(lblfin.Text)))
                {
                    //amountFull = (Convert.ToDouble(txt0.Text) + Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt4.Text) + Convert.ToDouble(txt5.Text)) - (Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text) + Convert.ToDouble(lblfin.Text)));
                    amountFull = Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt4.Text);
                    txt5.Text = amountFull.ToString();


                    double dis = 0.05;
                    txtRemarks.Text = "5% Discount";
                    newtotal = (amountFull * dis);
                    double finaltotal = amountFull - newtotal;
                    txtTotal.Text = finaltotal.ToString();
                    //txt5discount.Text = newtotal.ToString();
                    //txtTotal.Text = (discount).ToString();

                    //txtTotal.Text = (amountFull - newtotal).ToString();
                    //txtTotal.Text = newtotal


                    //txtTotal = (((float.Parse(newtotal.ToString())) * (float.Parse(discount.ToString())) / 100)).ToString();

                    //txtTotal.Text = 
                    // newtotal = 
                    //  txtTotal.Text =  * 5.ToString();
                }
                else
                {
                    txtTotal.Text = "0";
                }
            }
            else if (comboBox2.Text == "DOWNPAYMENT")
            {
                if (Convert.ToDouble(txt0.Text) > Convert.ToDouble(lbldownpayment.Text))
                {
                    amountDown = Convert.ToDouble(txt0.Text) - Convert.ToDouble(lbldownpayment.Text);
                    txtTotal.Text = amountDown.ToString();
                    txtRemarks.Text = "";
                }
                else
                {
                    txtTotal.Text = 0.ToString();
                }
            }
            else if (comboBox2.Text == "PRELIM")
            {
                if (Convert.ToDouble(txt0.Text) + Convert.ToDouble(txt1.Text) > Convert.ToDouble(lbldownpayment.Text) + Convert.ToDouble(lblpre.Text))
                {
                    amountprelim = (Convert.ToDouble(txt1.Text) + (Convert.ToDouble(txt0.Text)) - (Convert.ToDouble(lbldownpayment.Text) + Convert.ToDouble(lblpre.Text)));
                    txtTotal.Text = amountprelim.ToString();
                    txtRemarks.Text = "";

                }
                else
                {
                    txtTotal.Text = 0.ToString();
                }
            }
            else if (comboBox2.Text == "MIDTERM")
            {
                if (Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt0.Text) > Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text)))
                {
                    //amountmid = amount -Convert.ToDouble(lblmid.Text);
                    //amount = amountmid;
                    amountmid = (Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + (Convert.ToDouble(txt0.Text)) - (Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text))));

                    txtTotal.Text = amountmid.ToString();
                    txtRemarks.Text = "";
                }
                else
                {
                    txtTotal.Text = "0";
                }

            }

            else if (comboBox2.Text == "SEMI-FINAL")
            {
                if (Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt0.Text) > Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text)))
                {
                    //amountmid = amount -Convert.ToDouble(lblmid.Text);

                    //amount = amountmid;
                    amountsemi = (Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + (Convert.ToDouble(txt0.Text)) - (Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text))));


                    txtTotal.Text = amountsemi.ToString();
                    txtRemarks.Text = "";

                }
                else
                {
                    txtTotal.Text = "0";
                }

            }
            else if (comboBox2.Text == "FINAL")
            {
                if (Convert.ToDouble(txt0.Text) + Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt4.Text) > (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text) + Convert.ToDouble(lbldownpayment.Text) + Convert.ToDouble(lblfin.Text)))
                {


                    amountfinal = (Convert.ToDouble(txt0.Text) + Convert.ToDouble(txt1.Text) + Convert.ToDouble(txt2.Text) + Convert.ToDouble(txt3.Text) + Convert.ToDouble(txt4.Text)) - (Convert.ToDouble(lbldownpayment.Text) + (Convert.ToDouble(lblpre.Text) + Convert.ToDouble(lblmid.Text) + Convert.ToDouble(lblsemi.Text) + Convert.ToDouble(lblfin.Text)));


                    txtTotal.Text = amountfinal.ToString();
                    txtRemarks.Text = "";
                }
                else
                {
                    txtTotal.Text = "0";
                }

            }






        }

        private void txtAmount_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
        }
        public string temp = null;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
                if (checkBox1.Checked)
                {
                    temp = txtcurrentBal.Text;
                    txtcurrentBal.Text = "0.00";
                    double n = -1 * Convert.ToDouble(temp);

                    txtchange.Text = n.ToString();
                }
                else
                {
                    txtcurrentBal.Text = temp;
                    txtchange.Text = "";
                }

            
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            conn = connect.getcon();
            conn.Open();
            cmd = new MySqlCommand("update payment set status ='void' where paymentid = '" + dgv.SelectedRows[0].Cells[0].Value + "'", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Void");
            lbldownpayment.Text = "";
            lblpre.Text = "0.00";
            lblsemi.Text = "0.00";
            lblfin.Text = "0.00";
            lblmid.Text = "0.00";
            comboBox2.Items.Add("DOWNPAYMENT");
            comboBox2.Items.Add("PRELIM");
            comboBox2.Items.Add("MIDTERM");
            comboBox2.Items.Add("SEMI-FINAL");
            comboBox2.Items.Add("FINAL");
            printShow();
            showw();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //insert();

            //printShow();
            //showw();

        }

        private void cmbpaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {


        }

        private void Payment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button1.PerformClick();
            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {

                if (row.Cells[0].Value.ToString() == "0")
                {
                    foreach (var cell in row.Cells)
                    {
                        DataGridViewLinkCell linkCell = cell as DataGridViewLinkCell;

                        if (linkCell != null)
                        {
                            linkCell.UseColumnTextForLinkValue = false;
                        }
                    }
                }
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtchange_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgv.Columns[e.ColumnIndex].Name;

            if (colName.Equals("action"))
            {
                var myfrm = new voidNotification(this);
                myfrm.ShowDialog();
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void txt5discount_TextChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click_1(object sender, EventArgs e)
        {

        }

        private void lbldownpayment_Click(object sender, EventArgs e)
        {

        }
    }

    public class Receipt
    {
        public string studentNo { get; set; }
        public string name { get; set; }
        public int orNo { get; set; }
        public string date { get; set; }
        public double currentBalance { get; set; }
        public string remarks { get; set; }
        public string paymentMode { get; set; }
        public double change { get; set; }
        public double receiveAmt { get; set; }
    }

    public class PaymentCourse
    {
        public string course { get; set; }

    }

    public class TotalPaid
    {
        public double paid { get; set; }
    }
}
