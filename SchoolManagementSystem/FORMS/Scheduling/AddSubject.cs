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
            TextBox[] inputs = { txtSubjectCode, txtDescriptiveTitle, txtLec, txtLab, txtTotalUnits, txtLecPrice, txtLabprice };

            if (btnAddSubjects.Text.Equals("Update"))
            {
                if (Validator.isEmpty(inputs) && Validator.UpdateConfirmation())
                {
                    DBContext.GetContext().Query("subjects").Where("subjectId", lblIDD.Text).Update(new
                    {
                        courseCode = cmbCourseCode.Text,
                        subjectCode = txtSubjectCode.Text,
                        subjectTitle = txtDescriptiveTitle.Text,
                        lec = txtLec.Text,
                        lab = txtLab.Text,
                        totalUnits = txtTotalUnits.Text,
                        prereq = cmbPreReq.Text,
                        totalLecprice = lblLectotal.Text,
                        totalLabprice = lblabTotal.Text,
                        labprice = txtLabprice.Text,
                        lecprice = txtLecPrice.Text,
                        totalprice = TotalPrice.Text
                    });
                    reloadDatagrid.displayData();
                    this.Close();
                }
            }
            else if (btnAddSubjects.Text.Equals("Save"))
            {
                if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {
                    if (string.IsNullOrEmpty(txtTotalUnits.Text) || txtTotalUnits.Text.Equals("0"))
                    {
                        Validator.AlertDanger("Total unit must not be empty");
                    }
                    else if(lblLectotal.Text.Equals("0") && lblabTotal.Text.Equals("0"))
                    {
                        Validator.AlertDanger("Please enter an amount for lecture and lab!");
                    }
                    else if (string.IsNullOrEmpty(cmbCourse.Text))
                    {
                        Validator.AlertDanger("Please select course!");
                    }
                    else if (string.IsNullOrEmpty(cmbCourseCode.Text))
                    {
                        Validator.AlertDanger("Please select course code!");
                    }
                    else
                    {
                        try
                        {
                            DBContext.GetContext().Query("subjects").Where("subjectCode", txtSubjectCode.Text).First();
                            Validator.AlertDanger("Subject code already existed");
                        }
                        catch (Exception)
                        {
                            DBContext.GetContext().Query("subjects").Insert(new
                            {
                                courseCode = cmbCourseCode.Text,
                                subjectCode = txtSubjectCode.Text.ToUpper(),
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
                            reloadDatagrid.displayData();
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtLab_TextChanged(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtLec.Text) && !string.IsNullOrEmpty(txtLab.Text))
                txtTotalUnits.Text = (Convert.ToInt32(txtLec.Text) + Convert.ToInt32(txtLab.Text)).ToString();

            if (string.IsNullOrEmpty(txtLab.Text))
            {
                txtTotalUnits.Text = txtLec.Text;
            }

            if (string.IsNullOrWhiteSpace(txtLab.Text))
            {
                lblabTotal.Text = "0";
            }

            else
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

                    try
                    {
                        total = Convert.ToDouble(txtLabprice.Text) * Convert.ToDouble(txtLab.Text);
                        lblabTotal.Text = total.ToString();
                    }
                    catch (Exception)
                    {

                    }
                    num1 = Convert.ToDouble(lblabTotal.Text);

                    num2 = Convert.ToDouble(lblLectotal.Text);

                    total2 = num1 + num2;
                    TotalPrice.Text = total2.ToString();
                }
            }


        }

        private void txtLec_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLec.Text) && !string.IsNullOrEmpty(txtLab.Text))
                txtTotalUnits.Text = (Convert.ToInt32(txtLec.Text) + Convert.ToInt32(txtLab.Text)).ToString();
            if (string.IsNullOrEmpty(txtLec.Text))
            {
                txtTotalUnits.Text = txtLab.Text;
            }

            if (string.IsNullOrWhiteSpace(txtLec.Text))
            {
                lblLectotal.Text = "0";
            }

            else
            {
                if (txtLecPrice.Text == "" || txtLecPrice.Text == "0")
                {
                    lblLectotal.Text = "0";
                    TotalPrice.Text = lblabTotal.Text;
                }
                else
                {
                    double total;
                    double total2;
                    double num1;
                    double num2;
                    try
                    {
                        total = Convert.ToDouble(txtLecPrice.Text) * Convert.ToDouble(txtLec.Text);
                        lblLectotal.Text = total.ToString();
                    }
                    catch (Exception)
                    {
                    }
                    num1 = Convert.ToDouble(lblabTotal.Text);

                    num2 = Convert.ToDouble(lblLectotal.Text);

                    total2 = num1 + num2;
                    TotalPrice.Text = total2.ToString();
                }
            }


        }

        private void AddSubject_Load(object sender, EventArgs e)
        {
            txtLecPrice.KeyPress += Validator.ValidateKeypressNumber;
            txtLabprice.KeyPress += Validator.ValidateKeypressNumber;
            displayCourse();
        }


        public void displayCourse()
        {
            var values = DBContext.GetContext().Query("course").Get();

            foreach (var value in values)
            {
                cmbCourse.Items.Add(value.description);
            }
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
            if (string.IsNullOrEmpty(txtLec.Text))
            {

            }
            else
            {


                if (txtLecPrice.Text == "" || txtLecPrice.Text == "0")
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
                    try
                    {
                        total = Convert.ToDouble(txtLecPrice.Text) * Convert.ToDouble(txtLec.Text);
                        lblLectotal.Text = total.ToString();
                    }
                    catch (Exception)
                    {
                    }


                    num1 = Convert.ToDouble(lblabTotal.Text);

                    num2 = Convert.ToDouble(lblLectotal.Text);

                    total2 = num1 + num2;
                    TotalPrice.Text = total2.ToString();

                }
            }
        }

        private void txtLabprice_TextChanged(object sender, EventArgs e)
        {

            if (txtLab.Text == "")
            {

            }
            else
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

                    try
                    {
                        total = Convert.ToDouble(txtLabprice.Text) * Convert.ToDouble(txtLab.Text);
                        lblabTotal.Text = total.ToString();
                    }
                    catch (Exception)
                    {

                    }
                    num1 = Convert.ToDouble(lblabTotal.Text);

                    num2 = Convert.ToDouble(lblLectotal.Text);

                    total2 = num1 + num2;
                    TotalPrice.Text = total2.ToString();

                }
            }
        }

        private void cmbCourseCode_SelectedIndexChanged(object sender, EventArgs e)
        {

            var values = DBContext.GetContext().Query("subjects")
                .Join("coursecode", "coursecode.coursecode", "subjects.courseCode")
                .Where("subjects.courseCode", cmbCourseCode.Text).Get();

            cmbPreReq.Items.Clear();
            foreach (var value in values)
            {
                cmbPreReq.Items.Add(value.subjectCode);
            }

        }
        int idd;
        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCourseCode.Text = "";
            var getID = DBContext.GetContext().Query("course").Where("description", cmbCourse.Text).First();

            idd = getID.courseId;

            var values = DBContext.GetContext().Query("coursecode")
                .Join("course", "course.courseId", "coursecode.courseId")
                .Where("coursecode.courseId", idd.ToString())
                .Get();
            cmbCourseCode.Items.Clear();
            foreach(var value in values)
            {
                cmbCourseCode.Items.Add(value.coursecode);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCourse_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            
        }
    }
}
