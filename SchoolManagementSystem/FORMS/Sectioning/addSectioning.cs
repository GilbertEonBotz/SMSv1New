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
namespace SchoolManagementSystem
{
    public partial class addSectioning : Form
    {

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
            MessageBox.Show(id);
            var values = DBContext.GetContext().Query("schedule").Get();
            foreach (var value in values)
            {
                dgvSched.Rows.Add(value.schedID, value.subjectCode, value.subjectTitle, value.roomID, value.date, value.timeStart, value.timeEnd);
            }


        }

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
    }
}
