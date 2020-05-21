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
    public partial class menuRunner : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        string typeWindow;
        public menuRunner(string name)
        {
            InitializeComponent();
            timerDay.Start();
            date = Convert.ToDateTime("24.11.2020 06:00:00");
            typeWindow = name;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            contactform cf = new contactform();
            if(cf.ShowDialog()== DialogResult.OK)
            {

            }
        }

        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label2.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void menuRunner_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose)
                Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            variables.gUserLogin = "";
            variables.gUserId = "";
            variables.gUserRole = "";
            Form1 mainform = new Form1();
            if (mainform.Visible == false) mainform.Visible = true;
            checkClose = false;
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(typeWindow == "Form1")
            {
                Form1 fm = new Form1();
                if (fm.Visible == false) fm.Visible = true;
                        
            }
            else if (typeWindow == "ChooserRunners")
            {
                ChooseRunners cr = new ChooseRunners();
                cr.Show();
            }
            checkClose = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            regMarathon rm = new regMarathon();
            rm.Show();
            checkClose = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            updateRunner ur = new updateRunner();
            ur.Show();
            checkClose = false;
            this.Close();
        }
    }
}
