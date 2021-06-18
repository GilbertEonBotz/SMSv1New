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

            if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
            {
                if (txtTotal.Text.Equals("0"))
                {
                    Validator.AlertDanger("Total percentage must not be equal to zero");
                }
                else if (Convert.ToInt32(txtTotal.Text) > 100 || Convert.ToInt32(txtTotal.Text) < 100)
                {
                    Validator.AlertDanger("Total percentage must be equal to 100%");
                }
                else
                {
                    DBContext.GetContext().Query("percentage").Update(new
                    {
                        status = "Activate"
                    });

                    var values = DBContext.GetContext().Query("percentage").Insert(new
                    {
                        prelim = $"0.{txtPrelim.Text}",
                        midterm = $"0.{txtMidterm.Text}",
                        semiFinals = $"0.{txtSemi.Text}",
                        finals = $"0.{txtFinal.Text}",
                        downpayment = txtDownpayment.Text
                    });
                    MessageBox.Show("Inserted");
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
            if (!string.IsNullOrEmpty(txtPrelim.Text) && !string.IsNullOrEmpty(txtMidterm.Text) && !string.IsNullOrEmpty(txtSemi.Text) && !string.IsNullOrEmpty(txtFinal.Text))
                txtTotal.Text = (Convert.ToInt32(txtPrelim.Text) + Convert.ToInt32(txtMidterm.Text) + Convert.ToInt32(txtSemi.Text) + Convert.ToInt32(txtFinal.Text)).ToString();
        }

        private void txtMidterm_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrelim.Text) && !string.IsNullOrEmpty(txtMidterm.Text) && !string.IsNullOrEmpty(txtSemi.Text) && !string.IsNullOrEmpty(txtFinal.Text))
                txtTotal.Text = (Convert.ToInt32(txtPrelim.Text) + Convert.ToInt32(txtMidterm.Text) + Convert.ToInt32(txtSemi.Text) + Convert.ToInt32(txtFinal.Text)).ToString();
        }

        private void txtSemi_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrelim.Text) && !string.IsNullOrEmpty(txtMidterm.Text) && !string.IsNullOrEmpty(txtSemi.Text) && !string.IsNullOrEmpty(txtFinal.Text))
                txtTotal.Text = (Convert.ToInt32(txtPrelim.Text) + Convert.ToInt32(txtMidterm.Text) + Convert.ToInt32(txtSemi.Text) + Convert.ToInt32(txtFinal.Text)).ToString();
        }

        private void txtFinal_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrelim.Text) && !string.IsNullOrEmpty(txtMidterm.Text) && !string.IsNullOrEmpty(txtSemi.Text) && !string.IsNullOrEmpty(txtFinal.Text))
                txtTotal.Text = (Convert.ToInt32(txtPrelim.Text) + Convert.ToInt32(txtMidterm.Text) + Convert.ToInt32(txtSemi.Text) + Convert.ToInt32(txtFinal.Text)).ToString();
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
    }
}
