using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlKata.Execution;
using EonBotzLibrary;
namespace SchoolManagementSystem.FORMS.Sectioning
{
    public partial class addSectionCategory : Form
    {

        SectionCategory reload;
        public addSectionCategory(SectionCategory reload)
        {
            InitializeComponent();
            this.reload = reload;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            DBContext.GetContext().Query("sectionCategory").Insert(new
            {
                sectionName = txtStructure.Text,Description = txtDescription.Text
            }) ;

            reload.displayData();
            this.Close();
        }
    }
}
