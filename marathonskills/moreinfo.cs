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
    public partial class moreinfo : Form
    {
        bool checkClose = true;
        public moreinfo()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            if (mainForm.Visible == false) mainForm.Visible = true;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            infoCharity infocharity = new infoCharity();
            infocharity.Show();
            checkClose = false;
            this.Close();
        }
    }
}
