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
    public partial class AddFeeStructure : Form
    {
        int aa;
        public AddFeeStructure()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            TextBox[] inputs = { txtStructure , txtDescription};
            if (btnAddCourse.Text.Equals("Save"))
            {
                if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {
                    DBContext.GetContext().Query("feestructure").Insert(new
                    {
                        structurename = txtStructure.Text,
                        description = txtDescription.Text,

                    });
                    var rooms = DBContext.GetContext().Query("feestructure").Where("structureName", txtStructure.Text).First();
                        
                    aa = rooms.structureID;

                    DBContext.GetContext().Query("totalfee").Insert(new
                    {
                        structureID = aa,
                    });
                    this.Close();
                }
            }


        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
