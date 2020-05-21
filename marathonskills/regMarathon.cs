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
    public partial class regMarathon : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        int cashP = 0;
        int cashPfree = 0;
        int cashPdef = 0;
        char kitType = 'A';
        bool marathonFM = false;
        bool marathonFR = false;
        bool marathonHM = false;
        bool checkFM = false;
        bool checkFR = false;
        bool checkHM = false;
        int countFM = 0;
        int countFR = 0;
        int countHM = 0;
        public regMarathon()
        {
            InitializeComponent();
            timerDay.Start();
            date = Convert.ToDateTime("24.11.2020 06:00:00");
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            cashP -= cashPdef;
            if (checkBox2.Checked) { cashPdef += 75; marathonHM = true; }
            else
            {
                if (cashPdef <= 75)
                    cashPdef = 0;
                else
                    cashPdef -= 75;
                marathonHM = false;
            }
            cashP += cashPdef;
            label10.Text = cashP.ToString() + "$";
        }
        void fillBox()
        {
            try
            {
                // Заполнение комбобокса фондов
                variables.cmd = new MySqlCommand("SELECT * FROM charity", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    comboBox1.Items.Add(variables.reader.GetString("CharityName"));
                }
                variables.reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void regMarathon_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            menuRunner mr = new menuRunner("regMarathon");
            mr.Show();
            checkClose = false;
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                informationCharity();
            }
        }
        void informationCharity()
        {
            try
            {
                sql.GetDBConnection();
                variables.cmd = new MySqlCommand("select * from `charity` where `CharityName` = '" + comboBox1.SelectedItem + "'", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    variables.gCharityDescription = variables.reader.GetString("CharityDescription");
                    variables.gCharityImage = variables.reader.GetString("CharityLogo");
                }
                variables.reader.Close();
                infoCharity infoCharity = new infoCharity();
                if (infoCharity.ShowDialog() == DialogResult.OK)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            variables.conn.Close();
        }

        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label2.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void regMarathon_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose) Application.Exit();
        }

        void checktext(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
            if (textBox1.Text.Length == 0)
                if (e.KeyChar == '0') e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            checktext(e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cashP -= cashPdef;
            if (checkBox1.Checked) { cashPdef += 145; marathonFM = true; }
            else
            {
                if (cashPdef <= 145)
                    cashPdef = 0;
                else
                    cashPdef -= 145;
                marathonFM = false;
            }
            cashP += cashPdef;
            label10.Text = cashP.ToString() + "$";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            cashP -= cashPdef;
            if (checkBox3.Checked) { cashPdef += 20; marathonFR = true; }
            else
            {
                if (cashPdef <= 20)
                    cashPdef = 0;
                else
                    cashPdef -= 20;
                marathonFR = false;
            }
            cashP += cashPdef;
            label10.Text = cashP.ToString() + "$";
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            cashP -= cashPdef;
            if (radioButton3.Checked) { cashPdef += 45; kitType = 'C'; }
            else
            {
                if (cashPdef <= 45)
                    cashPdef = 0;
                else
                    cashPdef -= 45;
            }
            cashP += cashPdef;
            label10.Text = cashP.ToString() + "$";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cashP -= cashPdef;
            if (radioButton2.Checked) { cashPdef += 20; kitType = 'B'; }
            else
            {
                if (cashPdef <= 20)
                    cashPdef = 0;
                else
                    cashPdef -= 20;
            }
            cashP += cashPdef;
            label10.Text = cashP.ToString() + "$";
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                cashP -= cashPfree;
                if (cashP <= 0) cashP = 0;
                if (textBox1.Text.Length == 0) cashPfree = 0;
                try
                {
                    cashPfree = Convert.ToInt32(textBox1.Text);
                }
                catch (Exception ex) { }
                cashP += cashPfree;
                label10.Text = cashP.ToString() + "$";
            }
            catch (Exception ex)
            {
                textBox1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked) { MessageBox.Show("Вы не выбрали ни одного марафона!"); return; ; }
            if(comboBox1.SelectedItem == null) { MessageBox.Show("Выберите спонсора"); return;}
            insertRegMar();
        }
       

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) kitType = 'A';
        }
        void insertRegMar()
        {
            try
            {
                sql.GetDBConnection();
                int count = 0;
                variables.cmd = new MySqlCommand("select * from registration where RunnerId = " +
                    "(select RunnerId from runner where Email = '" + variables.gUserLogin + "')", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    count++;
                }
                variables.reader.Close();

                if (count == 0)
                {
                    variables.cmd = new MySqlCommand(
                            "insert into registration (RunnerId, RaceKitOptionId, RegistrationStatusId, Cost, CharityId, SponsorshipTarget) " +
                            "values (" +
                            "(select RunnerId from runner where Email = '" + variables.gUserLogin + "'), " +
                            "'" + kitType + "', " +
                            "'1', " +
                            "'" + cashPdef + "', " +
                            "(select CharityId from charity where CharityName = '" + comboBox1.SelectedItem + "'), " +
                            "'" + cashPfree + "'" +
                            ");"
                            , variables.conn);
                    variables.cmd.ExecuteNonQuery();
                    MessageBox.Show("User add [registration]");
                }
                checkMarathonReg();
                insertRegEvent();
                thxRunnerReg trr = new thxRunnerReg();
                trr.Show();
                checkClose = false;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            variables.conn.Close();
        }
        void insertRegEvent()
        {
            try
            {
                checkreguser();
                if (marathonFM && !checkFM)
                {
                    variables.cmd = new MySqlCommand(
                        "insert into registrationevent (`RegistrationId`, `EventId`, `BibNumber`) " +
                        "values (" +
                        "(select `RegistrationId` from `registration` where `RunnerId` = (select `RunnerId` from `runner` where `Email` = '" + variables.gUserLogin + "')), " +
                        "'20_2FM', " +
                        "'" + countFM + "');"
                        , variables.conn);
                    variables.cmd.ExecuteNonQuery();
                }
                if (marathonFR && !checkFR)
                {
                    variables.cmd = new MySqlCommand(
                        "insert into registrationevent (`RegistrationId`, `EventId`, `BibNumber`) " +
                        "values (" +
                        "(select `RegistrationId` from `registration` where `RunnerId` = (select `RunnerId` from `runner` where `Email` = '" + variables.gUserLogin + "')), " +
                        "'20_2FR', " +
                        "'" + countFR + "');"
                        , variables.conn);
                    variables.cmd.ExecuteNonQuery();
                }
                if (marathonHM && !checkHM)
                {
                    variables.cmd = new MySqlCommand(
                        "insert into registrationevent (`RegistrationId`, `EventId`, `BibNumber`) " +
                        "values (" +
                        "(select `RegistrationId` from `registration` where `RunnerId` = (select `RunnerId` from `runner` where `Email` = '" + variables.gUserLogin + "')), " +
                        "'20_2HM', " +
                        "'" + countHM + "');"
                        , variables.conn);
                    variables.cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        void checkMarathonReg()
        {
            try
            {
                variables.cmd = new MySqlCommand("select BibNumber from registrationevent where EventId = '20_2FM'", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    countFM = variables.reader.GetInt32("BibNumber");
                }
                variables.reader.Close();
                countFM++;
                variables.cmd = new MySqlCommand("select BibNumber from registrationevent where EventId = '20_2FR'", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    countFM = variables.reader.GetInt32("BibNumber");
                }
                variables.reader.Close();
                countFR++;
                variables.cmd = new MySqlCommand("select BibNumber from registrationevent where EventId = '20_2HM'", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    countFM = variables.reader.GetInt32("BibNumber");
                }
                variables.reader.Close();
                countHM++;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        void checkreguser()
        {
            try
            {
                // Проверка на регистрацию в марафоне
                variables.cmd = new MySqlCommand("select * from registrationevent where EventId = '20_2FM' and " +
                    "(select RegistrationId from registration where RunnerId = (select RunnerId from runner where Email = '" + variables.gUserLogin + "'))", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    checkFM = true;
                }
                variables.reader.Close();
                //
                variables.cmd = new MySqlCommand("select * from registrationevent where EventId = '20_2FR' and " +
                    "(select RegistrationId from registration where RunnerId = (select RunnerId from runner where Email = '" + variables.gUserLogin + "'))", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    checkFR = true;
                }
                variables.reader.Close();
                //
                variables.cmd = new MySqlCommand("select * from registrationevent where EventId = '20_2HM' and " +
                    "(select RegistrationId from registration where RunnerId = (select RunnerId from runner where Email = '" + variables.gUserLogin + "'))", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    checkHM = true;
                }
                variables.reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
