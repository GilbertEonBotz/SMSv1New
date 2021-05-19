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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var myfrm = new AddUser(this);
            myfrm.ShowDialog();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            displayData();
        }

        public void displayData()
        {
            var values = DBContext.GetContext().Query("users").Get();

            dgvUsers.Rows.Clear();
            foreach(var value in values)
            {
                int id = value.userrole;
                var role = DBContext.GetContext().Query("role").Where("roleId", id).First();

                dgvUsers.Rows.Add(value.id, value.name, role.roletype);
            }
        }
    }
}
