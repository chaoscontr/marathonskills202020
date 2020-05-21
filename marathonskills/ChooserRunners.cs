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
    public partial class ChooseRunners : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        public ChooseRunners()
        {
            InitializeComponent();
            timerDay.Start();
            date = Convert.ToDateTime("24.11.2020 06:00:00");
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if(checkClose)
            Application.Exit();
        }

        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label2.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            if (mainForm.Visible == false)
                mainForm.Visible = true;
            checkClose = false;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(variables.gUserLogin == "" || variables.gUserLogin == null)
            { 
            AuthificationForm af = new AuthificationForm("ChooserRunners");
            af.Show();
            checkClose = false;
            this.Close();
            }
            else
            {
                switch (variables.gUserRole)
                {
                    case "R":
                        menuRunner menurunner = new menuRunner("ChooserRunners");
                        menurunner.Show();
                break;
                    case "C":
                        break;
                    case "A":
                        break;
                    default:
                        break;     
                }
                checkClose = false;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (variables.gUserLogin == "" || variables.gUserLogin == null)
            {
                AuthificationForm af = new AuthificationForm("ChooserRunners");
                af.Show();
                checkClose = false;
                this.Close();
            }
            else
            {
                switch (variables.gUserRole)
                {
                    case "R":
                        menuRunner menurunner = new menuRunner("ChooserRunners");
                        menurunner.Show();
                        break;
                    case "C":
                        menuCoordinator mc = new menuCoordinator("ChooserRunners");
                        mc.Show();
                        break;
                    case "A":
                        menuAdmin ma = new menuAdmin("ChooserRunners");
                        ma.Show();
                        break;
                    default:
                        break;
                }
                checkClose = false;
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (variables.gUserLogin == "" || variables.gUserLogin == null)
            {
                RunnerReg rg = new RunnerReg();
                rg.Show();
                checkClose = false;
                this.Close();
            }
            else
            {
                if (MessageBox.Show("Вы уже авторизованы в системе. Для продолжения выйдите с своего аккаунта.\nВыйти?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    variables.gUserId = "";
                    variables.gUserLogin = "";
                    variables.gUserPassword = "";
                    variables.gUserRole = "";
                    RunnerReg rg = new RunnerReg();
                    rg.Show();
                    checkClose = false;
                    this.Close();
                }
            }
        }
    }
}
