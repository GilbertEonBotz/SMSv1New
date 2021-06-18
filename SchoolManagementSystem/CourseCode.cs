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
using SchoolManagementSystem.UITools;
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
            var values = DBContext.GetContext().Query("coursecode")
                .Join("course", "course.courseId", "coursecode.courseId")
                .Get();

            dgvCourseCode.Rows.Clear();
            foreach(var value in values)
            {
                dgvCourseCode.Rows.Add(value.courseId, value.coursecode, value.description,  value.remarks);
            }
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            var myfrm = new addCourseCode(this);
            FormFade.FadeForm(this, myfrm);
        }

        private void dgvCourseCode_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Focus);
            e.Handled = true;
        }
    }
}
