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
    public partial class AddUserRole : Form
    {
        UserRole reloadDatagrid;
        public AddUserRole(UserRole reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TextBox[] inputs = { txtRole };

            if (btnSave.Text.Equals("Update"))
            {
                if (Validator.isEmpty(inputs) && Validator.UpdateConfirmation())
                {

                    DBContext.GetContext().Query("role").Where("roomId", lblIDD.Text).Update(new
                    {
                        roletype = txtRole.Text,
                    });
                    //reloadDatagrid.displayData();
                    this.Close();
                }
            }
            else if (btnSave.Text.Equals("Save"))
            {
                if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {
                    try
                    {
                        DBContext.GetContext().Query("role").Where("roletype", txtRole.Text).First();
                        Validator.AlertDanger("Role type exist");
                    }
                    catch (Exception)
                    {
                        DBContext.GetContext().Query("role").Insert(new
                        {
                            roletype = txtRole.Text,
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
    }
}
