using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlKata.Execution;
using EonBotzLibrary;
using MySql.Data.MySqlClient;
namespace SchoolManagementSystem
{
    public partial class addSectioning : Form
    {

        Connection connect = new Connection();
        MySqlCommand cmd;
        MySqlConnection conn;
        MySqlDataReader dr;
        string id;
        public addSectioning(string val)
        {
            InitializeComponent();
            id = val;
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addSectioning_Load(object sender, EventArgs e)
        {
            var values = DBContext.GetContext().Query("schedule").Get();
            foreach (var value in values)
            {
                dgvSched.Rows.Add(value.schedID, value.subjectCode, value.subjectTitle, value.roomID, value.date, value.timeStart, value.timeEnd);
            }
            conn = connect.getcon();
            conn.Open();
            cmd = new MySqlCommand("select a.schedid,a.subjectcode,a.subjectTitle  from schedule a, Sectioning b, sectionCategory c where a.schedID = b.schedID and b.SectionCategoryID = '"+ id+ "'Group by b.sectionId", conn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dgvCategories.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
        }
       
        

        //public void displaySectioning()
        //{
        //    var values = DBContext.GetContext().Query("sectioning").Where("SectionCategoryID", ).Get
        //}
        private void dgvSched_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            dgvCategories.Rows.Add(dgvSched.SelectedRows[0].Cells[0].Value, dgvSched.SelectedRows[0].Cells[1].Value);
            DBContext.GetContext().Query("Sectioning").Insert(new
            {
                SectionCategoryID = id,
               schedID = dgvSched.SelectedRows[0].Cells[0].Value
            });
            MessageBox.Show("success");
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvSched_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
