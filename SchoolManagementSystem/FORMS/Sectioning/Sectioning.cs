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
using SchoolManagementSystem.UITools;

namespace SchoolManagementSystem.FORMS.Sectioning
{
    public partial class Sectioning : Form
    {
        public Sectioning()
        {
            InitializeComponent();
        }

        private void btnAddDept_Click(object sender, EventArgs e)
        {
            //var myfrm = new addSectionCategory();
            //FormFade.FadeForm(this, myfrm);
        }

        private void Sectioning_Load(object sender, EventArgs e)
        {
            var values = DBContext.GetContext().Query("sectionCategory").Get();
            foreach (var value in values)
            {
                dgvDepartment.Rows.Add(value.SectionCategoryID,value.sectionName, value.Description);
            }
        }

        private void dgvDepartment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            addSectioning add = new addSectioning(dgvDepartment.SelectedRows[0].Cells[0].Value.ToString());
            FormFade.FadeForm(this, add);
        }

        private void textboxWatermark1_TextChanged(object sender, EventArgs e)
        {
            //var values = DBContext.GetContext().Query("subjects").WhereLike("subjectId", $"{textboxWatermark1.Text}")
            // .OrWhereLike("subjectCode", $"%{textboxWatermark1.Text}%")
            // .OrWhereLike("subjectTitle", $"%{textboxWatermark1.Text}%")
            // .Get();

            //dgvSubjects.Rows.Clear();
            //foreach (var value in values)
            //{
            //    dgvSubjects.Rows.Add(value.subjectId, value.subjectCode, value.subjectTitle, value.lec, value.lab, value.totalUnits, value.prereq);
            //}
        }

        private void dgvDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
