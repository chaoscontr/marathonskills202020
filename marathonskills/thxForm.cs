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
    public partial class thxForm : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        public thxForm()
        {
            InitializeComponent();
            timerDay.Start();
            label5.Text = "Фонд - " + variables.gCharityName;
            label4.Text = "Бегун - " + variables.gNameRunner;
            label6.Text = variables.gCharityCash.ToString() +"$";
            date = Convert.ToDateTime("24.11.2020 06:00:00");
        }

        private void thxForm_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label2.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void thxForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            if (mainForm.Visible == false) mainForm.Visible = true;
            checkClose = false;
            this.Close();
        }
    }
}
