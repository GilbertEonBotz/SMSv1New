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
    public partial class AddFeeCategory : Form
    {

        FeeManagement reloadDatagrid;
        public AddFeeCategory(FeeManagement reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            TextBox[] inputs = { txtCategory };

            if (btnAddCategory.Text.Equals("Save"))
            {
                if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {
                    DBContext.GetContext().Query("categoryfee").Insert(new
                    {
                        category = txtCategory.Text,
                    });
                    Validator.AlertSuccess("successfully added");
                    reloadDatagrid.displayData();
                    this.Close();
                }
            }
        }

    }
}

