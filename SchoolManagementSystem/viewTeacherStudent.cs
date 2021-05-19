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
    public partial class viewTeacherStudent : Form
    {
        public viewTeacherStudent( string val)
        {
            InitializeComponent();
            label1.Text = val;
       
        }

        private void viewTeacherStudent_Load(object sender, EventArgs e)
        {
          var values  =DBContext.GetContext().Query("studentSched").Where("schedId", label1.Text).Get();
            foreach (var value in values)
            {
                dataGridView1.Rows.Add(value.studentSchedID);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
