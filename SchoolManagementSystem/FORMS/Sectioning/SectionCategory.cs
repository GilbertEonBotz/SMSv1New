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
            var myfrm = new addSectionCategory();
            FormFade.FadeForm(this, myfrm);
        }
    }
}
