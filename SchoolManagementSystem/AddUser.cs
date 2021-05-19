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
    public partial class AddUser : Form
    {
        Users reloadDatagrid;
        public AddUser(Users reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ComboBox[] selCmb = { cmbRole };
            TextBox[] inputs = { txtName, txtUsername, txtPassword, txtConfirmPassword, txtMacAddress };


            if (btnSave.Text.Equals("Update"))
            {
                if (Validator.isEmptyCmb(selCmb) && Validator.isEmpty(inputs) && Validator.UpdateConfirmation())
                {
                    DBContext.GetContext().Query("rooms").Where("roomId", lblIDD.Text).Update(new
                    {
                        description = txtUsername.Text,
                    });
                    reloadDatagrid.displayData();
                    this.Close();
                }
            }
            else if (btnSave.Text.Equals("Save"))
            {
                if (Validator.isEmptyCmb(selCmb) && Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {
                    if (txtConfirmPassword.Text != txtPassword.Text)
                    {
                        Validator.AlertDanger("Confirm password doesn't match");
                    }
                    else
                    {
                        var value = DBContext.GetContext().Query("role").Where("roletype", cmbRole.Text).First();
                        DBContext.GetContext().Query("users").Insert(new
                        {
                            name = txtName.Text,
                            username = txtUsername.Text,
                            password = txtPassword.Text,
                            userrole = value.roleId,
                            macAddress = txtMacAddress.Text
                        });
                        reloadDatagrid.displayData();
                        this.Close();
                    }
                   

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

        private void AddUser_Load(object sender, EventArgs e)
        {
            displayRole();
        }

        private void displayRole()
        {
            var values = DBContext.GetContext().Query("role").Get();

            foreach (var value in values)
            {
                cmbRole.Items.Add(value.roletype);
            }
        }

        private void cmbRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
