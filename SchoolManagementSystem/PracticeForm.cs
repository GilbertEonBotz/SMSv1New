﻿using System;
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
    public partial class PracticeForm : Form
    {
        public PracticeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void PracticeForm_Load(object sender, EventArgs e)
        {


           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double total = 0;

            total = Convert.ToDouble(textBox1.Text) + Convert.ToDouble(textBox2.Text);

            label1.Text = total.ToString("N2");
        }
    }
}
