using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace marathonskills
{
    
    public partial class AuthificationForm : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        string typeWindow;
        public AuthificationForm(string type)
        {
            InitializeComponent();
            timerDay.Start();
            date = Convert.ToDateTime("24.11.2020 06:00:00");
            typeWindow = type;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(typeWindow == "Form1")
            {
                Form1 fm = new Form1();
                if (fm.Visible == false) fm.Visible = true;
            }
            else if (typeWindow == "ChooserRunners")
            {
                ChooseRunners choosform = new ChooseRunners();
                choosform.Show();
            }
            checkClose = false;
            this.Close();
        }

        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label5.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void AuthificationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose)
                Application.Exit();
        }

        private void AuthificationForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    sql.GetDBConnection();
                    variables.cmd = new MySqlCommand("select Password, RoleId from user where Email = '" + textBox1.Text + "';", variables.conn);
                    MySqlDataReader accReader = variables.cmd.ExecuteReader();
                    while (accReader.Read())
                    {
                        variables.gUserPassword = accReader.GetString("Password");
                        variables.gUserRole = accReader.GetString("RoleId");
                    }
                    accReader.Close();
                    if (textBox2.Text != variables.gUserPassword) MessageBox.Show("Пароль введен неверно!");
                    else
                    {
                        switch (variables.gUserRole)
                        {
                            case "R":
                                variables.gUserLogin = textBox1.Text;
                                variables.gUserRole = "R";
                                menuRunner mR = new menuRunner(typeWindow);
                                mR.Show();
                                break;
                            case "C":
                                variables.gUserLogin = textBox1.Text;
                                variables.gUserRole = "C";
                                menuCoordinator mC = new menuCoordinator(typeWindow);
                                mC.Show();
                                break;
                            case "A":
                                variables.gUserLogin = textBox1.Text;
                                variables.gUserRole = "A";
                                menuAdmin mA = new menuAdmin(typeWindow);
                                mA.Show();
                                break;
                            default:
                                break;
                        }
                        checkClose = false;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                variables.conn.Close();
            }
            else MessageBox.Show("Все поля должны быть заполнены!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 mf = new Form1();
            if (mf.Visible == false) mf.Visible = true;
            checkClose = false;
            this.Close();
        }
    }
}
