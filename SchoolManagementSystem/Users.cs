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
            var myfrm = new AddUser(this, idd);
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
            foreach (var value in values)
            {
                int id = value.userrole;
                var role = DBContext.GetContext().Query("role").Where("roleId", id).First();

                dgvUsers.Rows.Add(value.id, value.name, role.roletype, value.status);
            }
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (Convert.ToString(row.Cells[3].Value) == "Activate")
                {
                    row.Cells[3].Style.ForeColor = Color.Blue;
                    row.Cells[3].Style.SelectionForeColor = Color.Blue;
                }
                else
                {
                    row.Cells[3].Style.ForeColor = Color.Red;
                    row.Cells[3].Style.SelectionForeColor = Color.Red;
                }
            }
        }
        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUsers.Columns[e.ColumnIndex].Name;

            if (colName.Equals("archive"))
            {
                if (dgvUsers.SelectedRows[0].Cells[3].Value.ToString() == "Activate")
                {
                    if (Validator.actYear())
                    {
                        DBContext.GetContext().Query("users").Where("id", dgvUsers.SelectedRows[0].Cells[0].Value).Update(new
                        {
                            status = "Deactivate"
                        });
                        displayData();
                    }
                }
                else if (dgvUsers.SelectedRows[0].Cells[3].Value.ToString() == "Deactivate")
                {
                    if (Validator.deactYear())
                    {
                        DBContext.GetContext().Query("users").Where("id", dgvUsers.SelectedRows[0].Cells[0].Value).Update(new
                        {
                            status = "Activate"
                        });
                        displayData();
                    }
                }
            }
        }

        string idd;
        private void dgvUsers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(dgvUsers.Rows[dgvUsers.CurrentRow.Index].Cells[0].Value);
            idd = id.ToString();
            var myfrm = new AddUser(this, idd);

            var values = DBContext.GetContext().Query("users").Where("id", id).Get();
            foreach(var value in values)
            {
                myfrm.txtName.Text = value.name;
                myfrm.txtUsername.Text = value.username;
                myfrm.txtPassword.Text = value.password;
                myfrm.txtMacAddress.Text = value.macAddress;
            }
            myfrm.ShowDialog();
        }
    }
}