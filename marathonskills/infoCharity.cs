using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace marathonskills
{
    public partial class infoCharity : Form
    {
        public infoCharity()
        {
            InitializeComponent();
            label1.Text = variables.gCharityName;
            richTextBox1.Text = variables.gCharityDescription;
            pictureBox1.Load("res//charity//" + variables.gCharityImage);
        }

        private void infoCharity_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void infoCharity_Load(object sender, EventArgs e)
        {

        }
    }
}
