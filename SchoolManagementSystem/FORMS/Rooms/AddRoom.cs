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
    public partial class AddRoom : Form
    {
        Room room = new Room();

        room reloadDatagrid;
        public AddRoom(room reloadDatagrid)
        {
            InitializeComponent();
            this.reloadDatagrid = reloadDatagrid;
            
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            TextBox[] inputs = { txtDescription };

            if (btnAddRoom.Text.Equals("Update"))
            {
                if (Validator.isEmpty(inputs) && Validator.UpdateConfirmation())
                {
                    DBContext.GetContext().Query("rooms").Where("roomId", lblIDD.Text).Update(new
                    {
                        description = txtDescription.Text,
                    });
                    reloadDatagrid.displayData();
                    this.Close();
                }
            }
            else if (btnAddRoom.Text.Equals("Save"))
            {
                if (Validator.isEmpty(inputs) && Validator.AddConfirmation())
                {
                    try
                    {
                        DBContext.GetContext().Query("rooms").Where("description", txtDescription.Text).First();
                        Validator.AlertDanger("Room is already existed");
                    }
                    catch (Exception)
                    {
                        DBContext.GetContext().Query("rooms").Insert(new
                        {
                            name = txtName.Text,
                            description = txtDescription.Text,
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

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
