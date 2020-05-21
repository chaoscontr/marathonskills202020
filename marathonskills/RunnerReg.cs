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
using System.Text.RegularExpressions;
using System.IO;

namespace marathonskills
{
    public partial class RunnerReg : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        string tempPhoto;
        public RunnerReg()
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        void fillBox()
        {
            try
            {
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
        private void RunnerReg_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose)
                Application.Exit();
        }

        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label12.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                textBox6.Text = openFileDialog1.SafeFileName;
                tempPhoto = openFileDialog1.FileName;
            }
        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            ChooseRunners cr = new ChooseRunners();
            cr.Show();
            checkClose = false;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            checkClose = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = 0;
            string pass = textBox2.Text;
            if (validateEmail(mailBox.Text) == false)
            {
                MessageBox.Show("Некорректно введена почта!");
                return;
            }
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
            if (nameBox.Text == "") { MessageBox.Show("Поле с Именем не заполнено"); return; }
            if (FamBox.Text == "") { MessageBox.Show("Поле с Фамилией не заполнено"); return; }
            if (polBox.SelectedItem == null) { MessageBox.Show("Выберите пол"); return; }
            if (textBox6.Text == "") { MessageBox.Show("Загрузите фотографию"); return; }
            if (comboBox1.SelectedItem == null) { MessageBox.Show("Выберите страну"); return; }
            insertAcc();
        }
        void insertAcc()
        {
            int count = 0;
            try
            {
                sql.GetDBConnection();
                variables.cmd = new MySqlCommand("select * from user where `Email` ='" + mailBox.Text + "';", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    count++;
                }
                variables.reader.Close();
                if (count != 0) { MessageBox.Show("Аккаунт с такой почтой уже существует"); return; }
                try
                {
                    variables.cmd = new MySqlCommand("insert into user (`Email`, `Password`, `FirstName`, `LastName`, `RoleId`) values(" +
                        "'" + mailBox.Text + "'," +
                        "'" + textBox2.Text + "'," +
                        "'" + nameBox.Text + "'," +
                        "'" + FamBox.Text + "'," +
                        "'R');", variables.conn);
                    variables.cmd.ExecuteNonQuery();
                    try
                    {
                        variables.cmd = new MySqlCommand("insert into runner(`Email`, `Gender`,`DateOfBirth`, `CountryCode`, `Image`) values(" +
                            "'" + mailBox.Text + "'," +
                            "'" + polBox.SelectedItem + "'," +
                            "'" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "'," +
                            "(select `CountryCode` from `country` where `CountryName`='" + comboBox1.SelectedItem + "')," +
                            "'" + textBox6.Text + "');", variables.conn);
                        variables.cmd.ExecuteNonQuery();
                        Directory.CreateDirectory("res//" + mailBox.Text);
                        string copyphoto = "res//" + mailBox.Text + "//" + textBox6.Text;
                        FileInfo fileInfo = new FileInfo(tempPhoto);
                        fileInfo.CopyTo(copyphoto);
                        menuRunner mr = new menuRunner("RunnerReg");
                        mr.Show();
                        checkClose = false;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool validateEmail(string email)
        {
            string[] words = email.Split('@');
            if (words.Length != 2)
                return false;
            string[] dotdomen = words[1].Split('.');
            if (dotdomen.Length != 2)
                return false;
            return true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void mailBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void FamBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void polBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
