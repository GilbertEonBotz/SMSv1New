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
    public partial class StudentActivation : Form
    {
        public StudentActivation()
        {
            InitializeComponent();
        }

        private void StudentActivation_Load(object sender, EventArgs e)
        {

        }

        public void displayData()
        {
            var values = DBContext.GetContext().Query("student")
                .Join("studentActivation", "studentActivation.studentID", "student.studentId")
                .Get();

            foreach(var value in values)
            {
                dgvStudents.Rows.Add(); 
            }

        }
    }
}
