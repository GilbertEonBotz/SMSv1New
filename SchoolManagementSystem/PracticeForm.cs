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
    public partial class PracticeForm : Form
    {
        public PracticeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void PracticeForm_Load(object sender, EventArgs e)
        {
            var values = DBContext.GetContext().Query("rooms").Get();

            foreach(var value in values)
            {
                dataGridView1.Rows.Add(value.name, value.description);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }
    }
}
