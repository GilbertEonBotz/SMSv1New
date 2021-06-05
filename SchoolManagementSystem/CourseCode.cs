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
    public partial class CourseCode : Form
    {
        public CourseCode()
        {
            InitializeComponent();
        }

        private void CourseCode_Load(object sender, EventArgs e)
        {
            displayData();
        }

        public void displayData()
        {
            var values = DBContext.GetContext().Query("coursecode").Get();

            dgvCourseCode.Rows.Clear();
            foreach(var value in values)
            {
                dgvCourseCode.Rows.Add(value.courseId, value.courseId, value.coursecode, value.remarks);
            }
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            var myfrm = new addCourseCode(this);
            myfrm.ShowDialog();
        }
    }
}
