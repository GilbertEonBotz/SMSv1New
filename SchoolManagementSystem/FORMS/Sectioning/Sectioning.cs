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
    public partial class Sectioning : Form
    {
        public Sectioning()
        {
            InitializeComponent();
        }

        private void btnAddDept_Click(object sender, EventArgs e)
        {
          
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
            add.Show();
        }
    }
}
