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
            tuitionfee tui = new tuitionfee();
            tui.id = tuitionID;
            tui.selectQuery();
 
            foreach (DataRow Drow in tui.dt.Rows)
            {
                int num = dgvCategories.Rows.Add();

                dgvCategories.Rows[num].Cells[0].Value = Drow["subjectcode"].ToString();
                dgvCategories.Rows[num].Cells[1].Value = Drow["amount"].ToString();
            }

            tui.selectQuery2();
            textBox1.Text = tui.total;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
