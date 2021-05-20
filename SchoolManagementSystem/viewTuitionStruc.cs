using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EonBotzLibrary;
using SqlKata.Execution;
namespace SchoolManagementSystem
{
    public partial class viewTuitionStruc : Form
    {
        string tuitionID;
        public viewTuitionStruc(string val)
        {
            InitializeComponent();
            tuitionID = val;
        }

        private void viewTuitionStruc_Load(object sender, EventArgs e)
        {
            displaySched();
            displayFee();
        }

        public void displaySched()
        {

            dgvSched.Rows.Clear();
            var values = DBContext.GetContext().Query("schedule").Get();

            foreach (var value in values)
            {
                dgvSched.Rows.Add(value.schedID, value.subjectCode, value.roomID, value.date, value.timeStart, value.timeEnd);
            }
        }


        private void displayFee()
        {
            tuitionfee tui = new tuitionfee();
            tui.id = tuitionID;
            tui.selectQuery();

            foreach (DataRow Drow in tui.dt.Rows)
            {
                int num = dgvCategories.Rows.Add();

                dgvCategories.Rows[num].Cells[0].Value = Drow["schedID"].ToString();
                dgvCategories.Rows[num].Cells[1].Value = Drow["subjectcode"].ToString();
            }

            //tui.selectQuery2();
            //textBox1.Text = tui.total;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSched_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           dgvCategories.Rows.Add(dgvSched.SelectedRows[0].Cells[0].Value.ToString(), dgvSched.SelectedRows[0].Cells[1].Value.ToString(),
           dgvSched.SelectedRows[0].Cells[2].Value.ToString(), dgvSched.SelectedRows[0].Cells[3].Value.ToString(), dgvSched.SelectedRows[0].Cells[4].Value
           );

            DBContext.GetContext().Query("tuition").Insert(new
            {
                subjectCode = dgvSched.SelectedRows[0].Cells[1].Value.ToString(),
                schedID = dgvSched.SelectedRows[0].Cells[0].Value.ToString(),
                tuitionCatID = tuitionID
            });
            MessageBox.Show("success");
        }
    }
}
