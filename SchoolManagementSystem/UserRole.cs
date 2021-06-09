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
            var myfrm = new AddUserRole(this, idd);
            myfrm.ShowDialog();
        }

        private void UserRole_Load(object sender, EventArgs e)
        {
            displayData();
        }
        public void displayData()
        {
            var values = DBContext.GetContext().Query("role").Where("status", "activate").Get();

            dgvUsersRole.Rows.Clear();
            foreach (var value in values)
            {
                dgvUsersRole.Rows.Add(value.roleId, value.roletype);
            }
        }

        private void dgvUsersRole_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUsersRole.Columns[e.ColumnIndex].Name;

            if (colName.Equals("delete"))
            {
                if (Validator.DeleteConfirmation())
                {
                    DBContext.GetContext().Query("role").Where("roleId", dgvUsersRole.SelectedRows[0].Cells[0].Value).Update(new
                    {
                        status = "deactivate"
                    });
                    displayData();
                }
            }
        }
        string idd;
        private void dgvUsersRole_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(dgvUsersRole.Rows[dgvUsersRole.CurrentRow.Index].Cells[0].Value);
            idd = id.ToString();
            var myfrm = new AddUserRole(this, idd);
            var value = DBContext.GetContext().Query("role").Where("roleId", id).First();
            myfrm.txtRole.Text = value.roletype;
            myfrm.btnSave.Text = "Update";
            myfrm.ShowDialog();
        }
    }
}
