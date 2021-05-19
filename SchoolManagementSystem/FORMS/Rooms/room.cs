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
    public partial class room : Form
    {
        public room()
        {
            InitializeComponent();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            var myrfrm = new AddRoom(this);
            myrfrm.ShowDialog();
        }

        private void room_Load(object sender, EventArgs e)
        {
            displayData();
        }
                
        public void displayData()
        {
            dgvRooms.Rows.Clear();
            var rooms = DBContext.GetContext().Query("rooms").Get();
            
            foreach (var room in rooms)
            {
                dgvRooms.Rows.Add(room.roomId,room.name, room.description);         
            }
        }

        private void dgvRooms_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var myfrm = new AddRoom(this);
            int id = Convert.ToInt32(dgvRooms.Rows[dgvRooms.CurrentRow.Index].Cells[0].Value);
            var rooms = DBContext.GetContext().Query("rooms").Where("roomId", id).First();

            myfrm.lblIDD.Text = id.ToString();
            myfrm.txtDescription.Text = rooms.description;
            myfrm.txtName.Text = rooms.name;
            myfrm.btnAddRoom.Text = "Update";
            myfrm.ShowDialog();
        }
    }
}
