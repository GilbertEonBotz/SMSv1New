using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class finalDashboard : Form
    {
        public finalDashboard()
        {
            InitializeComponent();
            hideSubMenu();
        }

        private void hideSubMenu()
        {
            pnlUser.Visible = false;
            pnlLoads.Visible = false;
            pnlEnrollment.Visible = false;
            pnlStudentRecords.Visible = false;
            pnlEmployees.Visible = false;
            pnlFeesMngmnt.Visible = false;
            pnlAcademic.Visible = false;
            pnlSchoolSettings.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnEnrollment_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlEnrollment);
        }

        private void btnStudentRecords_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlStudentRecords);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlEmployees);
        }

        private void btnFeesManagement_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlFeesMngmnt);
        }

        private void btnAcademicMngmt_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlAcademic);
        }



        public void displayStudentScheduling()
        {
            var myForm = new StudentScheduling(this);
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }
        private void btnStudentSchedule_Click(object sender, EventArgs e)
        {
            displayStudentScheduling();
        }

        private void displayDashboard()
        {
            var myForm = new Dashboard();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            displayDashboard();
        }

        private void btnAdmitStudent_Click(object sender, EventArgs e)
        {
            var myForm = new StudentInformation();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnTeacher_Click(object sender, EventArgs e)
        {
            var myForm = new TeacherInformation();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnAccountant_Click(object sender, EventArgs e)
        {
            var myForm = new AccountantInformation();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnLibrarian_Click(object sender, EventArgs e)
        {
            var myForm = new LibrarianInformation();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnFeeCategory_Click(object sender, EventArgs e)
        {
            var myForm = new FeeManagement();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnFeeStructure_Click(object sender, EventArgs e)
        {
            var myForm = new FeeStructure();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnTuitionCategory_Click(object sender, EventArgs e)
        {
            var myForm = new TuitionCategoryInfo();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnTuitionStructure_Click(object sender, EventArgs e)
        {
            var myForm = new tuition();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            var myForm = new StudentPaymentShow(this);
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnSubjects_Click(object sender, EventArgs e)
        {
            var myForm = new Subject();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnClassScgedule_Click(object sender, EventArgs e)
        {
            var myForm = new Sched();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnTerm_Click(object sender, EventArgs e)
        {
            var myForm = new AcademicYear();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            var myForm = new Department();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            var myForm = new CourseInformation();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnCourseCode_Click(object sender, EventArgs e)
        {
            var myForm = new CourseCode();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnClassrooms_Click(object sender, EventArgs e)
        {
            var myForm = new room();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void finalDashboard_Load(object sender, EventArgs e)
        {
            displayDashboard();
        }

        private void btnExamPercentage_Click(object sender, EventArgs e)
        {
            var myForm = new ExamPercentage();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnSchoolSettings_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlSchoolSettings);
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlUser);
        }

        private void btnTeacherLoads_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlLoads);
        }

        private void btnSubjectLoads_Click(object sender, EventArgs e)
        {
            var myForm = new teacherSched();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnHandledStudents_Click(object sender, EventArgs e)
        {
            var myForm = new viewTeacherSched();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnManageRole_Click(object sender, EventArgs e)
        {
            var myForm = new UserRole();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnAddUsers_Click(object sender, EventArgs e)
        {
            var myForm = new Users();
            pnlShow.Controls.Clear();
            myForm.TopLevel = false;
            myForm.AutoScroll = false;
            pnlShow.Controls.Add(myForm);
            myForm.Show();
        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
