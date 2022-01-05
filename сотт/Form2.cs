using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace сотт
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Boolean CheckUser()
        {
            DataBase DB = new DataBase();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @UL", DB.getConnection());
            
            command.Parameters.Add("@UL", MySqlDbType.VarChar).Value = textBox1.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой пользователь уже зарегистрирован");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void buttonRegistred_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (CheckUser())
                return;

            DataBase DB = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `post`) VALUES (@Log, @pass, @post)", DB.getConnection());
            command.Parameters.Add("@Log", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBox2.Text;
            
            if (checkBox1.Checked == true)
            {
                command.Parameters.Add("@post", MySqlDbType.VarChar).Value = "Администратор";
            }
            else if(checkBox2.Checked == true)
            {
                command.Parameters.Add("@post", MySqlDbType.VarChar).Value = "Сотрудник";
            }
            else
            {
                MessageBox.Show("Выберите должность");
                return;
            }

            DB.openConnection();
            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Successful");
            else
                MessageBox.Show("failed");
            DB.closeConnection();

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 LogForm = new Form1();
            LogForm.Show();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
