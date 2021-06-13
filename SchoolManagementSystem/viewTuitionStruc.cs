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
        tuition reloadDatagrid;
        public viewTuitionStruc(string val, tuition reloadDatagrid)
        {
            InitializeComponent();
            tuitionID = val;
            this.reloadDatagrid = reloadDatagrid;
        }

        private void viewTuitionStruc_Load(object sender, EventArgs e)
        {
            displaySched();
            displayFee();
        }

        public void displaySched()
        {
            schedule sched = new schedule();

            dgvSched.Columns[5].DefaultCellStyle.Format = "hh:mm tt";
            dgvSched.Columns[6].DefaultCellStyle.Format = "hh:mm tt";
            sched.viewsched();

            dgvSched.Rows.Clear();
            foreach (DataRow Drow in sched.dt.Rows)
            {
                int num = dgvSched.Rows.Add();

                dgvSched.Rows[num].Cells[0].Value = Drow["SchedID"].ToString();
                dgvSched.Rows[num].Cells[1].Value = Drow["SubjectCode"].ToString();
                dgvSched.Rows[num].Cells[2].Value = Drow["SubjTitle"].ToString();
                dgvSched.Rows[num].Cells[3].Value = Drow["roomID"].ToString();
                dgvSched.Rows[num].Cells[4].Value = Drow["date"].ToString();
                dgvSched.Rows[num].Cells[5].Value = Convert.ToDateTime(Drow["time start"].ToString());
                dgvSched.Rows[num].Cells[6].Value = Convert.ToDateTime(Drow["time end"].ToString());
            }
        }


        private void displayFee()
        {
            tuitionfee tui = new tuitionfee();
            tui.id = tuitionID;
            tui.selectQuery();

            dgvCategories.Rows.Clear();
            foreach (DataRow Drow in tui.dt.Rows)
            {
                int num = dgvCategories.Rows.Add();

                dgvCategories.Rows[num].Cells[0].Value = Drow["schedID"].ToString();
                dgvCategories.Rows[num].Cells[1].Value = Drow["subjectcode"].ToString();
                dgvCategories.Rows[num].Cells[2].Value = Drow["subjTitle"].ToString();
            }

            //tui.selectQuery2();
            //textBox1.Text = tui.total;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            reloadDatagrid.displaydata();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategories.Columns[e.ColumnIndex].Name;

            if (colName.Equals("delete"))
            {
                if (Validator.DeleteConfirmation())
                {
                    DBContext.GetContext().Query("tuition").Where("schedId", dgvCategories.SelectedRows[0].Cells[0].Value).Delete();
                    displayFee();
                }
               
            }
        }

        private void dgvSched_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                if ((string)row.Cells[1].Value == dgvSched.SelectedRows[0].Cells[1].Value.ToString())
                {
                    Validator.AlertDanger("Subject existed");
                    return;
                }
            }

            dgvCategories.Rows.Add(dgvSched.SelectedRows[0].Cells[0].Value.ToString(), dgvSched.SelectedRows[0].Cells[1].Value.ToString(), dgvSched.SelectedRows[0].Cells[2].Value.ToString());

            DBContext.GetContext().Query("tuition").Insert(new
            {
                subjTitle = dgvSched.SelectedRows[0].Cells[2].Value.ToString(),
                subjectCode = dgvSched.SelectedRows[0].Cells[1].Value.ToString(),
                schedID = dgvSched.SelectedRows[0].Cells[0].Value.ToString(),
                tuitionCatID = tuitionID
            });
        }

        private void dgvSched_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
