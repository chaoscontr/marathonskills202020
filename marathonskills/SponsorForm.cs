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
    public partial class SponsorForm : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        int cashP = 0;
        public SponsorForm()
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
        private void button3_Click(object sender, EventArgs e)
        {
            cashP += 10;
            textBox2.Text = cashP.ToString();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            checktext(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            checktext(e);
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            checktext(e);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            checktext(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            checktext(e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
             cashP = Convert.ToInt32(textBox2.Text);
            cash.Text = cashP.ToString() + "$";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cashP <= 10)
                cashP = 0;
            else cashP -= 10;
            textBox2.Text = cashP.ToString();

        }

        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label13.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void SponsorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose)
                Application.Exit();
        }
        void checktext (KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 mainform = new Form1();
            if (mainform.Visible == false)
                mainform.Visible = true;
            checkClose = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 mainform = new Form1();
            if (mainform.Visible == false)
                mainform.Visible = true;
            checkClose = false;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            variables.gCharityName = comboBox1.SelectedItem.ToString();
        }
        void fillBox()
        {
            try
            {
                // Заполнение комбобокса
                variables.cmd = new MySqlCommand("SELECT * FROM user WHERE RoleId = 'R'", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    runnerBox.Items.Add(variables.reader.GetString("FirstName") + " " + variables.reader.GetString("LastName"));
                }
                variables.reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                // Заполнение спонсорских контор
                variables.cmd = new MySqlCommand("SELECT * FROM charity" , variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                  comboBox1.Items.Add(variables.reader.GetString("CharityName"));
                    //variables.gCharityDescription = variables.reader.GetString("CharityDescription");
                }
                variables.reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            if(comboBox1.SelectedItem != null)
            {
                informationCharity();
            }
        }
        void informationCharity()
        {
            try
            {
                sql.GetDBConnection();
                variables.cmd = new MySqlCommand("select * from `charity` where `CharityName` = '" + comboBox1.SelectedItem+"'", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    variables.gCharityDescription = variables.reader.GetString("CharityDescription");
                    variables.gCharityImage = variables.reader.GetString("CharityLogo");
                }
                variables.reader.Close();
            infoCharity infoCharity = new infoCharity();
                if(infoCharity.ShowDialog()== DialogResult.OK)
                {

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            variables.conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int numberCardLenght = Convert.ToInt32(textBox4.Text.Length);
            int monthCard = Convert.ToInt32(textBox5.Text);
            int monthCardLenght = Convert.ToInt32(textBox5.Text.Length);
            int yearCard = Convert.ToInt32(textBox6.Text);
            int cvcLenght = Convert.ToInt32(textBox7.Text.Length);
            if (textBox1.Text != null && runnerBox.SelectedItem != null && textBox3.Text != null
             && textBox4.Text != null && textBox5.Text != null && textBox6.Text != null && textBox7.Text != null && comboBox1.SelectedItem != null)
            {
                if (numberCardLenght == 16)
                {
                    if ((monthCard <= 9 && monthCardLenght == 1) || monthCardLenght == 2)
                    {
                        if (monthCard <= 9 && monthCardLenght == 1) variables.gMonthCard = "0" + monthCard;
                        else variables.gMonthCard = monthCard.ToString();
                      
                        variables.gMonthCard = "0" + monthCard;
                        if (monthCard < 13 && yearCard > 19)
                        {
                            variables.gYearCard = "20" + yearCard;
                            if (cvcLenght == 3)
                            {
                                donateCharity();
                            }
                            else MessageBox.Show("Некорректный CVC код");
                        }
                        else MessageBox.Show("Некорректные данные срока действия карты");
                    }

                }
                else MessageBox.Show("Некорректный номер карты");

            }
            else MessageBox.Show("Не все данные заполнены");
        }
        void donateCharity()
        {
            try
            { 
            sql.GetDBConnection();
            variables.gNameRunner = runnerBox.SelectedItem.ToString();
            variables.gCharityName = comboBox1.SelectedItem.ToString();
            variables.gCharityCash = Convert.ToInt32(textBox2.Text);
            thxForm thxForm = new thxForm();
            thxForm.Show();
            checkClose = false;
            this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            variables.conn.Close();
        }

        private void SponsorForm_Load(object sender, EventArgs e)
        {

        }
    }
   
}
