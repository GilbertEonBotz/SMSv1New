using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EonBotzLibrary;
using SqlKata.Execution;

namespace SchoolManagementSystem
{
    public partial class AddSubject : Form
    {
        Subjects subj = new Subjects();

        Subject reloadDatagrid;
        public AddSubject(Subject reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
        }

        private void btnAddSubjects_Click(object sender, EventArgs e)
        {
        
            TextBox[] inputs = { txtSubjectCode , txtDescriptiveTitle, txtLec, txtLab, txtTotalUnits };

            if (btnAddSubjects.Text.Equals("Update"))
            {
                if (Validator.isEmpty(inputs) && Validator.UpdateConfirmation())
                {
                    DBContext.GetContext().Query("subjects").Where("subjectId", lblIDD.Text).Update(new
                    {
                         subjectCode = txtSubjectCode.Text,
                         subjectTitle = txtDescriptiveTitle.Text,
                    });
                    reloadDatagrid.displayData();
                    this.Close();
                }
            }
            else if (btnAddSubjects.Text.Equals("Save"))
            {
                if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {
                    DBContext.GetContext().Query("subjects").Insert(new
                    {
                        subjectCode = txtSubjectCode.Text,
                        subjectTitle = txtDescriptiveTitle.Text,
                        lec = txtLec.Text,
                        lab = txtLab.Text,
                        totalUnits = txtTotalUnits.Text,
                        prereq = cmbPreReq.Text,
                        status = "Avail",
                        totalLecprice = lblLectotal.Text,
                        totalLabprice = lblabTotal.Text,
                        labprice = txtLabprice.Text,
                        lecprice = txtLecPrice.Text,
                        totalprice = TotalPrice.Text
                    });
                    MessageBox.Show("success");
                 //   reloadDatagrid.displayData();
                    //this.Close();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void txtLab_TextChanged(object sender, EventArgs e)
        {
           if (txtLab.Text =="")
            {
                txtTotalUnits.Text = txtLec.Text;
     
            }
            else if(txtLec.Text == "")
                        
            {
                txtTotalUnits.Text = txtLab.Text;
            }
           else
            {
                int result = Convert.ToInt32(txtLab.Text) + Convert.ToInt32(txtLec.Text);
                txtTotalUnits.Text = result.ToString();

            }
        }

        private void txtLec_TextChanged(object sender, EventArgs e)
        {
            if (txtLab.Text == "")
            {
                txtTotalUnits.Text = txtLec.Text;

            }
            else if (txtLec.Text == "")

            {
                txtTotalUnits.Text = txtLab.Text;
            }
            else
            {


                int result = Convert.ToInt32(txtLab.Text) + Convert.ToInt32(txtLec.Text);
                txtTotalUnits.Text = result.ToString();

            }
            //txtTotalUnits.Text = txtLec.Text;


            //if (!string.IsNullOrEmpty(txtLec.Text) && !string.IsNullOrEmpty(txtLab.Text))
            //    txtTotalUnits.Text = (Convert.ToInt32(txtLec.Text) + Convert.ToInt32(txtLab.Text)).ToString();
        }

        private void AddSubject_Load(object sender, EventArgs e)
        {
        }

        private void txtLec_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void txtLab_MouseLeave(object sender, EventArgs e)
        {
        }

        private void txtLec_Leave(object sender, EventArgs e)
        {
        }

        private void txtLab_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtLec_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTotalUnits_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLecPrice_TextChanged(object sender, EventArgs e)
        {
            if(txtLecPrice.Text == "" || txtLecPrice.Text == "0")
            {
                lblLectotal.Text = "0";
                TotalPrice.Text = lblabTotal.Text;
            }
            else
            {

                //double total;
                //total = Convert.ToDouble(txtLecPrice.Text) * Convert.ToDouble(txtLec.Text);

                //lblLectotal.Text = total.ToString();
                double total;
                double total2;
                double num1;
                double num2;

                total = Convert.ToDouble(txtLecPrice.Text) * Convert.ToDouble(txtLec.Text);
                lblLectotal.Text = total.ToString();
                num1 = Convert.ToDouble(lblabTotal.Text);

                num2 = Convert.ToDouble(lblLectotal.Text);

                total2 = num1 + num2;
                TotalPrice.Text = total2.ToString();


            }
        }

        private void txtLabprice_TextChanged(object sender, EventArgs e)
        {
            if (txtLabprice.Text == "" || txtLabprice.Text == "0")
            {
                lblabTotal.Text = "0";
                TotalPrice.Text = lblLectotal.Text;
            }
            else
            {

                double total;
                     double total2;
                double num1;
                double num2;
         
                total = Convert.ToDouble(txtLabprice.Text) * Convert.ToDouble(txtLab.Text);
                lblabTotal.Text = total.ToString();
                num1 =Convert.ToDouble(lblabTotal.Text);

                num2 = Convert.ToDouble(lblLectotal.Text);
              
                total2 = num1 + num2;
                TotalPrice.Text = total2.ToString();


            }
        }
    }
}
