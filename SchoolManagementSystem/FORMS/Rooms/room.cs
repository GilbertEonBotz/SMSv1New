using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EonBotzLibrary;
using SchoolManagementSystem.UITools;
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
            var myfrm = new AddRoom(this);
            FormFade.FadeForm(this,myfrm);

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
            
        }

        private void dgvRooms_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Focus);
            e.Handled = true;
        }

        private void dgvRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvRooms.Columns[e.ColumnIndex].Name;

            if (colName.Equals("edit"))
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
}
