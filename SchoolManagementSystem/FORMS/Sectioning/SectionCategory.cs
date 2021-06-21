using SchoolManagementSystem.FORMS.Sectioning;
using SchoolManagementSystem.UITools;
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
    public partial class SectionCategory : Form
    {
        public SectionCategory()
        {
            InitializeComponent();
        }

        private void btnAddDept_Click(object sender, EventArgs e)
        {
            var myfrm = new addSectionCategory(this);
            FormFade.FadeForm(this, myfrm);
        }

        public void displayData()
        {
            var values = DBContext.GetContext().Query("sectionCategory").Get();
            foreach (var value in values)
            {
                dgvDepartment.Rows.Add(value.SectionCategoryID, value.sectionName, value.Description);
            }
        }

        private void SectionCategory_Load(object sender, EventArgs e)
        {
            displayData();
        }
    }
}
