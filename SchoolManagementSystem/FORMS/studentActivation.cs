﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlKata.Execution;
using EonBotzLibrary;
namespace SchoolManagementSystem.FORMS
{
    public partial class studentActivation : Form
    {
     public  string studentid;

        double discount;
     
        public studentActivation()
        {
            InitializeComponent();
        }

        private void studentActivation_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAcademicYear_Click(object sender, EventArgs e)
        {
          
            if (btnAddAcademicYear.Text == "Activate")
            {
                DBContext.GetContext().Query("studentActivation").Insert(new
                {
                    studentID = studentid,
                    downpayment = textBox1.Text,
                    note = comboBox2.Text,
                    paymentMethod = comboBox1.Text,
                    status = "Activated",
                    discount = discount,
                    discountDescription = comboBox3.Text

                }) ;
                MessageBox.Show("Success");
            }

            else { MessageBox.Show("else"); }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "blood related")
            {
                discount = 20;
            }
            else if (comboBox3.Text == "loyalty")
            {
                discount = 100;
            }
            else if (comboBox3.Text == "employee")
            {
                discount = 20;
            }
        }
    }
}
