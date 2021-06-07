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

        private void btnSchoolSettings_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlSchoolSettings);
        }
    }
}
