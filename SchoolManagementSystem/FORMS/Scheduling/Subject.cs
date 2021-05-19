using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EonBotzLibrary;
using SqlKata.Execution;

namespace SchoolManagementSystem
{
    public partial class Subject : Form
    {
        public Subject()
        {
            InitializeComponent();
        }

        private void btnAddSybject_Click(object sender, EventArgs e)
        {
            var myfrm = new AddSubject(this);
            myfrm.ShowDialog();
        }

        private void dgvSubjects_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var myfrm = new AddSubject(this);
            int id = Convert.ToInt32(dgvSubjects.Rows[dgvSubjects.CurrentRow.Index].Cells[0].Value);
            var value = DBContext.GetContext().Query("subjects").Where("subjectId", id).First();

            myfrm.lblIDD.Text = id.ToString();
            myfrm.txtSubjectCode.Text = value.subjectCode;
            myfrm.txtDescriptiveTitle.Text = value.subjectTitle;
            myfrm.txtLec.Text = Convert.ToString(value.lec);
            myfrm.txtLab.Text = Convert.ToString(value.lab);
            myfrm.txtTotalUnits.Text = Convert.ToString(value.unit);
            myfrm.cmbPreReq.Text = value.prereq;

            myfrm.btnAddSubjects.Text = "Update";
            myfrm.ShowDialog();
        }

        private void Subject_Load(object sender, EventArgs e)
        {
            displayData();
        }

        public void displayData()
        {
            dgvSubjects.Rows.Clear();
            var values = DBContext.GetContext().Query("subjects").Get();

            foreach (var value in values)
            {
                dgvSubjects.Rows.Add(value.subjectId, value.subjectCode, value.subjectTitle, value.lec, value.lab, value.unit, value.prereq);
            }
        }

    }
}
