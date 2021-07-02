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
    public partial class AddExamPercentage : Form
    {
        ExamPercentage reloadDatagrid;
        public AddExamPercentage(ExamPercentage reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
            txtDownpayment.KeyPress += Validator.ValidateKeypressNumber;
            txtPrelim.KeyPress += Validator.ValidateKeypressNumber;
            txtMidterm.KeyPress += Validator.ValidateKeypressNumber;
            txtSemi.KeyPress += Validator.ValidateKeypressNumber;
            txtFinal.KeyPress += Validator.ValidateKeypressNumber;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TextBox[] inputs = { txtDownpayment, txtPrelim, txtMidterm, txtSemi, txtFinal };

            TextBox[] inputss = {txtPrelim, txtMidterm, txtSemi, txtFinal };

            if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
            {
                if (txtTotal.Text.Equals("0"))
                {
                    Validator.AlertDanger("Total percentage must not be equal to zero");
                }
                else if (Convert.ToDouble(txtTotal.Text) > 100 || Convert.ToDouble(txtTotal.Text) < 100)
                {
                    Validator.AlertDanger("Total percentage must be equal to 100%");
                }
                else
                {
                    if (Validator.removeZero(inputss))
                    {

                    }
                    DBContext.GetContext().Query("percentage").Update(new
                    {
                        status = "Activate"
                    });

                    var values = DBContext.GetContext().Query("percentage").Insert(new
                    {
                        prelim = $"0.{txtPrelim.Text.Trim()}",
                        midterm = $"0.{txtMidterm.Text.Trim()}",
                        semiFinals = $"0.{txtSemi.Text.Trim()}",
                        finals = $"0.{txtFinal.Text.Trim()}",
                        downpayment = txtDownpayment.Text.Trim()
                    });
                    Validator.AlertSuccess("Exam percentage inserted");
                    reloadDatagrid.displayData();
                    this.Close();

                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrelim_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPrelim.Text) && !string.IsNullOrEmpty(txtMidterm.Text) && !string.IsNullOrEmpty(txtSemi.Text) && !string.IsNullOrEmpty(txtFinal.Text))
                    txtTotal.Text = (Convert.ToDouble(txtPrelim.Text) + Convert.ToDouble(txtMidterm.Text) + Convert.ToDouble(txtSemi.Text) + Convert.ToDouble(txtFinal.Text)).ToString();
            }
            catch (Exception)
            {

            }
            
        }

        private void txtMidterm_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrelim.Text) && !string.IsNullOrEmpty(txtMidterm.Text) && !string.IsNullOrEmpty(txtSemi.Text) && !string.IsNullOrEmpty(txtFinal.Text))
                txtTotal.Text = (Convert.ToDouble(txtPrelim.Text) + Convert.ToDouble(txtMidterm.Text) + Convert.ToDouble(txtSemi.Text) + Convert.ToDouble(txtFinal.Text)).ToString();
        }

        private void txtSemi_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrelim.Text) && !string.IsNullOrEmpty(txtMidterm.Text) && !string.IsNullOrEmpty(txtSemi.Text) && !string.IsNullOrEmpty(txtFinal.Text))
                txtTotal.Text = (Convert.ToDouble(txtPrelim.Text) + Convert.ToDouble(txtMidterm.Text) + Convert.ToDouble(txtSemi.Text) + Convert.ToDouble(txtFinal.Text)).ToString();
        }

        private void txtFinal_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrelim.Text) && !string.IsNullOrEmpty(txtMidterm.Text) && !string.IsNullOrEmpty(txtSemi.Text) && !string.IsNullOrEmpty(txtFinal.Text))
                txtTotal.Text = (Convert.ToDouble(txtPrelim.Text) + Convert.ToDouble(txtMidterm.Text) + Convert.ToDouble(txtSemi.Text) + Convert.ToDouble(txtFinal.Text)).ToString();
        }

        private void txtPrelim_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrelim.Text))
            {
                txtPrelim.Text = "0";
            }
        }

        private void txtMidterm_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMidterm.Text))
            {
                txtMidterm.Text = "0";
            }
        }

        private void txtSemi_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSemi.Text))
            {
                txtSemi.Text = "0";
            }
        }

        private void txtFinal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFinal.Text))
            {
                txtFinal.Text = "0";
            }
        }

        private void txtDownpayment_Enter(object sender, EventArgs e)
        {
            txtDownpayment.Text = "";
        }

        private void txtPrelim_Enter(object sender, EventArgs e)
        {
            txtPrelim.Text = "";
        }

        private void txtMidterm_Enter(object sender, EventArgs e)
        {
            txtMidterm.Text = "";
        }

        private void txtSemi_Enter(object sender, EventArgs e)
        {
            txtSemi.Text = "";
        }

        private void txtFinal_Enter(object sender, EventArgs e)
        {
            txtFinal.Text = "";
        }

        private void txtDownpayment_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDownpayment.Text))
            {
                txtDownpayment.Text = "0";
            }
        }

        private void AddExamPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnExit.PerformClick();
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdmissionForm_Click(object sender, EventArgs e)
        {

        }
    }
}
