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
    public partial class userManagment : Form
    {

        TimeSpan d = new TimeSpan();
        DateTime date = new DateTime();
        bool checkClose = true;
        int countUser = 0;
        string role = "";
        List<string[]> data = new List<string[]>();
        string typeUpdate = "";
        string typeUpdateRole = "";
        string typeUpdateRow = "";
        public userManagment()
        {
            InitializeComponent();
            timerDay.Start();
            date = Convert.ToDateTime("24.11.2020 06:00:00");
            fill();

        }
        void fill()
        {
            try
            {
            sql.GetDBConnection();
            variables.sql = "select distinct `RoleId` from `user`";
            variables.cmd = new MySqlCommand(variables.sql, variables.conn);
            variables.reader = variables.cmd.ExecuteReader();
            while (variables.reader.Read())
            {
                    switch (variables.reader.GetString("RoleId"))
                    {
                        case "R":
                            role = "Бегун";
                            break;
                        case "C":
                            role = "Координатор";
                            break;
                        case "A":
                            role = "Администратор";
                            break;
                    }
                    comboBox1.Items.Add(role);
            }
                variables.reader.Close();
                variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user`" , variables.conn);
                variables.reader = variables.cmd.ExecuteReader();
                while (variables.reader.Read())
                { 
                    data.Add(new string[4]);
                    data[data.Count - 1][0] = variables.reader.GetString("FirstName");
                    data[data.Count - 1][1] = variables.reader.GetString("LastName");
                    data[data.Count - 1][2] = variables.reader.GetString("Email");
                    data[data.Count - 1][3] = role;
                    countUser++;
                }
                variables.reader.Close();
                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);
                label8.Text = countUser.ToString();
                
            }
            catch(Exception ex)
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

        private void userManagment_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDay.Stop();
            if (checkClose) Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menuAdmin ma = new menuAdmin("userManagment");
            ma.Show();
            checkClose = false;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            updateFilter(); 
        }
        void updateFilter()
        {
            try
            {
                sql.GetDBConnection();
                bool checkBox1 = false;
                bool checkBox2 = false;
                typeUpdateRole = "";
                typeUpdateRow = "";
                if (comboBox1.SelectedItem != null) checkBox1 = true;
                if (checkBox1)
                {
                    if (comboBox1.SelectedItem == "Бегун")
                        typeUpdateRole = "run";
                    else if (comboBox1.SelectedItem == "Координатор")
                        typeUpdateRole = "coor";
                    else if (comboBox1.SelectedItem == "Администратор")
                        typeUpdateRole = "adm";
                }
                if (comboBox2.SelectedItem != null) checkBox2 = true;
                if (checkBox2)
                {
                    if (comboBox2.SelectedItem == "Имени")
                        typeUpdateRow = "name";
                    else if (comboBox2.SelectedItem == "Фамилии")
                        typeUpdateRow = "fam";
                    else if (comboBox2.SelectedItem == "Почте")
                        typeUpdateRow = "mail";
                    else if (comboBox2.SelectedItem == "Роле")
                        typeUpdateRow = "role";
                }
                if (checkBox1 && !checkBox2) typeUpdate = typeUpdateRole;
                if (!checkBox1 && checkBox2) typeUpdate = typeUpdateRow;
                if (checkBox1 && checkBox2) typeUpdate = typeUpdateRole + " " + typeUpdateRow;
                if (!checkBox1 && !checkBox2) typeUpdate = "";
                fillGrid(typeUpdate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            variables.conn.Close();
        }
        void fillGrid(string type)
        {
            dataGridView1.Rows.Clear();
            data.Clear();
            switch (type)
            {
                case "adm":
                   variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'A'", variables.conn);
                    break;
                case "run":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'R'", variables.conn);
                    break;
                case "coor":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'C'", variables.conn);
                    break;
                case "name":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` order by `FirstName`", variables.conn);
                    break;
                case "fam":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` order by `LastName`", variables.conn);
                    break;
                case "mail":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` order by `Email`", variables.conn);
                    break;
                case "role":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` order by `RoleId`", variables.conn);
                    break;
                case "adm name":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'A' order by `FirstName`", variables.conn);
                    break;
                case "adm fam":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'A' order by `LastName` ", variables.conn);
                    break;
                case "adm mail":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'A' order by `Email` ", variables.conn);
                    break;
                case "adm role":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'A' order by `RoleId` ", variables.conn);
                    break;
                case "run name":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'R' order by `FirstName` ", variables.conn);
                    break;
                case "run fam":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'R' order by `LastName` ", variables.conn);
                    break;
                case " run mail":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'R' order by `Email` ", variables.conn);
                    break;
                case "run role":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'R' order by `RoleId` ", variables.conn);
                    break;
                case "coor name":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'C' order by `FirstName` ", variables.conn);
                    break;
                case "coor fam":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'C' order by `LastName` ", variables.conn);
                    break;
                case "coor mail":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'C' order by `Email` ", variables.conn);
                    break;
                case "coor role":
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` where `RoleId` = 'C' order by `RoleId` ", variables.conn);
                    break;
                default:
                    variables.cmd = new MySqlCommand("select `Email`, `FirstName`, `LastName`, `RoleId` from `user` ", variables.conn);
                    break;
            }
            variables.reader = variables.cmd.ExecuteReader();
            while (variables.reader.Read())
            {
                switch (variables.reader.GetString("RoleId"))
                {
                    case "R":
                        role = "Бегун";
                        break;
                    case "C":
                        role = "Координатор";
                        break;
                    case "A":
                        role = "Администратор";
                        break;
                }
                data.Add(new string[4]);
                data[data.Count - 1][0] = variables.reader.GetString("FirstName");
                data[data.Count - 1][1] = variables.reader.GetString("LastName");
                data[data.Count - 1][2] = variables.reader.GetString("Email");
                data[data.Count - 1][3] = role;
                countUser++;
            }
            variables.reader.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }
    }
}
