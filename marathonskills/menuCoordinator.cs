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
    public partial class menuCoordinator : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        string typeWindow;
        public menuCoordinator(string name)
        {
            InitializeComponent();
            timerDay.Start();
            date = Convert.ToDateTime("24.11.2020 06:00:00");
            typeWindow = name;
        }

        private void menuCoordinator_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose)
                Application.Exit();
        }

        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label2.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            variables.gUserLogin = "";
            variables.gUserId = "";
            variables.gUserRole = "";
            variables.gUserPassword = "";
            Form1 mainform = new Form1();
            if (mainform.Visible == false) mainform.Visible = true;
            checkClose = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (typeWindow == "Form1")
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
    }
}
