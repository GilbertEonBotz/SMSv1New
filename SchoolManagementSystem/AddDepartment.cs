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
    public partial class AddDepartment : Form
    {
        Department reloadDatagrid;
        public AddDepartment(Department reloadDatagrid)
        {
            this.reloadDatagrid = reloadDatagrid;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TextBox[] inputs = { txtDeptName };

            if (btnSave.Text.Equals("Update"))
            {
                if (Validator.isEmpty(inputs) && Validator.UpdateConfirmation())
                {
                    DBContext.GetContext().Query("department").Where("deptID", lblIDD.Text).Update(new
                    {
                        description = txtDeptName.Text,
                    });
                    reloadDatagrid.displayData();
                    this.Close();
                }
            }
            else if (btnSave.Text.Equals("Save"))
            {
                if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {

                    DBContext.GetContext().Query("department").Insert(new
                    {
                        description = txtDeptName.Text,
                    });
                    reloadDatagrid.displayData();
                    this.Close();

                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
