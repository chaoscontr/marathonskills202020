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
using System.IO;
using System.Text.RegularExpressions;

namespace marathonskills
{
    public partial class updateRunner : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        string tempPhoto;
        bool checkImage = false;
        public updateRunner()
        {
            InitializeComponent();
            timerDay.Start();
            date = Convert.ToDateTime("24.11.2020 06:00:00");
            DateTime t1 = DateTime.Parse(DateTime.Today.ToString());
            DateTime t2 = t1.AddYears(-10);
            dateTimePicker1.MaxDate = t2;
            try
            {
                sql.GetDBConnection();
                fillBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            variables.conn.Close();
            
        }
        void fillBox()
        {
            try
            {
                variables.cmd = new MySqlCommand("select user.FirstName, user.LastName, runner.Gender, runner.DateOfBirth, country.CountryName, runner.Image from user, runner, country where user.Email = '" + variables.gUserLogin + "' and country.CountryCode = runner.CountryCode and user.Email = runner.Email", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    mailBox.Text = variables.gUserLogin;
                    nameBox.Text = variables.reader.GetString("FirstName");
                   FamBox.Text= variables.reader.GetString("LastName");
                   polBox.SelectedItem = variables.reader.GetString("Gender");
                    dateTimePicker1.Value = variables.reader.GetDateTime("DateOfBirth");
                    comboBox1.SelectedItem = variables.reader.GetString("CountryName");
                    textBox6.Text = variables.reader.GetString("Image");
                    tempPhoto = "res//" + variables.gUserLogin + "//" + textBox6.Text;
                    FileInfo f = new FileInfo(tempPhoto);
                    if (f.Exists)
                    {
                        pictureBox1.Load(f.FullName);
                        textBox6.Text = f.Name;
                    }
                    else
                    {
                        pictureBox1.Load("res//null.png");
                        textBox6.Text = "null";
                    }
                }
                variables.reader.Close();
                mailBox.Text = variables.gUserLogin;
                variables.cmd = new MySqlCommand("select * from `gender`", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    polBox.Items.Add(variables.reader.GetString("Gender"));
                }
                variables.reader.Close();

                variables.cmd = new MySqlCommand("select * from `country`", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    comboBox1.Items.Add(variables.reader.GetString("CountryName"));
                }
                variables.reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label12.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void updateRunner_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose) Application.Exit();
        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            menuRunner mr = new menuRunner("updateRunner");
            mr.Show();
            checkClose = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                textBox6.Text = openFileDialog1.SafeFileName;
                tempPhoto = openFileDialog1.FileName;
                checkImage = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = 0;
            string pass = textBox2.Text;
            if(textBox2.Text != "")
        { 
            if (pass.Length < 6)
            {
                MessageBox.Show("Длина пароля должны быть не меньше 6 символов");
                return;
            }
            if (!pass.Any(c => char.IsUpper(c)))
            {
                MessageBox.Show("Введите в пароль заглавную букву");
                return;
            }
            if (!pass.Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Введите в пароль цифру ");
                return;
            }
            Regex r = new Regex(@"[!@#$%^]");
            Match m = r.Match(pass);
            while (m.Success)
            {
                count++;
                m = m.NextMatch();
            }
            if (count == 0)
            {
                MessageBox.Show("В пароле нет ни одного из следующих символов: ! @ # $ % ^");
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
                variables.gUserPassword = textBox2.Text;
         }
            if (nameBox.Text == "") { MessageBox.Show("Поле с Именем не заполнено"); return; }
            if (FamBox.Text == "") { MessageBox.Show("Поле с Фамилией не заполнено"); return; }
            if (polBox.SelectedItem == null) { MessageBox.Show("Выберите пол"); return; }
            if (textBox6.Text == "") { MessageBox.Show("Загрузите фотографию"); return; }
            if (comboBox1.SelectedItem == null) { MessageBox.Show("Выберите страну"); return; }
            runnerUpdate();
        }
        void runnerUpdate()
        {
            try
            {
                sql.GetDBConnection();
                variables.cmd = new MySqlCommand("update user, runner set " +
                    "user.Password='" + variables.gUserPassword +"',"+
                    "user.FirstName ='" + nameBox.Text + "'," +
                    "user.LastName= '" + FamBox.Text + "', " +
                    "runner.Gender = '" + polBox.SelectedItem + "'," +
                    "runner.DateOfBirth = '" + dateTimePicker1.Value.Year+ "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "'," +
                    "runner.CountryCode =(select CountryCode from country where CountryName = '"+comboBox1.SelectedItem +"'),"+
                    "runner.Image='"+ textBox6.Text+"' where user.Email= '" + variables.gUserLogin+ "';", variables.conn);
                variables.cmd.ExecuteNonQuery();
                DirectoryInfo directoryinfo = new DirectoryInfo("res//" + variables.gUserLogin);
                FileInfo fileinfo = new FileInfo(tempPhoto);
                string road = "res//" + variables.gUserLogin + "//" + textBox6.Text;
                if (directoryinfo.Exists )   
                {
                    if (checkImage)
                    {
                        fileinfo.CopyTo(road);
                    }
                } 
                else
                {
                    directoryinfo.Create();
                    if(fileinfo.Exists)fileinfo.CopyTo(road);
                }
                menuRunner mr = new menuRunner("updateRunner");
                mr.Show();
                checkClose = false;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            variables.conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menuRunner mr = new menuRunner("updateRunner");
            mr.Show();
            checkClose = false;
            this.Close();
        }
    }
}
