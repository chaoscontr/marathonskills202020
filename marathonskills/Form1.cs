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
    public partial class Form1 : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        public Form1()
        {
            InitializeComponent();
            timerDay.Start();
            date = Convert.ToDateTime("24.11.2020 06:00:00");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label3.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseRunners frm = new ChooseRunners();
            frm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (variables.gUserLogin == "" || variables.gUserLogin == null)
            {
                AuthificationForm af = new AuthificationForm("Form1");
                af.Show();
                this.Hide();
            }
            else
            {
                switch (variables.gUserRole)
                {
                    case "R":
                        menuRunner menurunner = new menuRunner("Form1");
                        menurunner.Show();
                        break;
                    case "C":
                        menuCoordinator mc = new menuCoordinator("Form1");
                        mc.Show();
                        break;
                    case "A":
                        menuAdmin ma = new menuAdmin("Form1");
                        ma.Show();
                        break;
                    default:
                        break;
                }
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SponsorForm sp = new SponsorForm();
            sp.Show();
            timerDay.Stop();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            moreinfo moreinfo = new moreinfo();
            moreinfo.Show();
            timerDay.Stop();
            this.Visible = false;
        }
    }
}
