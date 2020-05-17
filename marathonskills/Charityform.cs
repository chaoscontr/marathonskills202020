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
    public partial class Charityform : Form
    {
        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        int count = 1;
        public Charityform()
        {
            InitializeComponent();
            timerDay.Start();
            date = Convert.ToDateTime("24.11.2020 06:00:00");
            try
            {
                sql.GetDBConnection();
                fillPicture();
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            variables.conn.Close();
        }

        private void Charityform_Load(object sender, EventArgs e)
        {

        }

        private void timerDay_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            d = date - datenow;
            label2.Text = "Осталось " + d.Days + " д. " + d.Hours + " ч. " + d.Minutes + " мин. ";
        }

        private void Charityform_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose)
                Application.Exit();
        }
        void fillPicture()
        {
            try
            {
                variables.cmd = new MySqlCommand("select * from `charity`", variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                {
                    variables.gCharityDescription = variables.reader.GetString("CharityDescription");
                    variables.gCharityImage = "res//charity//" + variables.reader.GetString("CharityLogo");
                    loadImage();
                    count++;
                }
                variables.reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void loadImage()
        {
            switch (count)
            {
                case 1:
                    pictureBox1.Load(variables.gCharityImage);
                    richTextBox15.Text = variables.gCharityDescription;
                    break;
                case 2:
                    pictureBox2.Load(variables.gCharityImage);
                    richTextBox14.Text = variables.gCharityDescription;
                    break;
                case 3:
                    pictureBox5.Load(variables.gCharityImage);
                    richTextBox13.Text = variables.gCharityDescription;
                    break;
                case 4:
                    pictureBox6.Load(variables.gCharityImage);
                    richTextBox12.Text = variables.gCharityDescription;
                    break;
                case 5:
                    pictureBox7.Load(variables.gCharityImage);
                    richTextBox11.Text = variables.gCharityDescription;
                    break;
                case 6:
                    pictureBox8.Load(variables.gCharityImage);
                    richTextBox10.Text = variables.gCharityDescription;
                    break;
                case 7:
                    pictureBox9.Load(variables.gCharityImage);
                    richTextBox9.Text = variables.gCharityDescription;
                    break;
                case 8:
                    pictureBox10.Load(variables.gCharityImage);
                    richTextBox1.Text = variables.gCharityDescription;
                    break;
                case 9:
                    pictureBox11.Load(variables.gCharityImage);
                    richTextBox2.Text = variables.gCharityDescription;
                    break;
                case 10:
                    pictureBox12.Load(variables.gCharityImage);
                    richTextBox3.Text = variables.gCharityDescription;
                    break;
                case 11:
                    pictureBox13.Load(variables.gCharityImage);
                    richTextBox4.Text = variables.gCharityDescription;
                    break;
                case 12:
                    pictureBox14.Load(variables.gCharityImage);
                    richTextBox5.Text = variables.gCharityDescription;
                    break;
                case 13:
                    pictureBox15.Load(variables.gCharityImage);
                    richTextBox6.Text = variables.gCharityDescription;
                    break;
                case 14:
                    pictureBox16.Load(variables.gCharityImage);
                    richTextBox7.Text = variables.gCharityDescription;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            moreinfo moreinfo = new moreinfo();
            moreinfo.Show();
            checkClose = false;
            this.Close();
        }
    }
}
