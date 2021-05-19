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
    public partial class UserRole : Form
    {
        public UserRole()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var myfrm = new AddUserRole(this);
            myfrm.ShowDialog();
        }

        private void UserRole_Load(object sender, EventArgs e)
        {
            displayData();
        }
        public void displayData()
        {
            var values = DBContext.GetContext().Query("role").Get();

            dgvUsersRole.Rows.Clear();
            foreach(var value in values)
            {
                dgvUsersRole.Rows.Add(value.roleId, value.roletype);
            }
        }
    }
}
