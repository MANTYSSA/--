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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
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
            string LoginUser = textBox1.Text;
            string PasswordUser = textBox2.Text;
            DataBase DB = new DataBase();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @UL AND `password` = @PU", DB.getConnection());
            
            command.Parameters.Add("@UL", MySqlDbType.VarChar).Value = LoginUser;
            command.Parameters.Add("@PU", MySqlDbType.VarChar).Value = PasswordUser;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if(table.Rows.Count > 0)
            {
                this.Hide();
                if (table.Rows[0]["post"].ToString().Equals("Администратор"))
                {
                    MessageBox.Show("Вы администратор");
                    Form3 Admin = new Form3();
                    Admin.Show();
                    this.Hide();
                }
                else if (table.Rows[0]["post"].ToString().Equals("Сотрудник"))
                {
                    MessageBox.Show("Вы сотрудник");
                    Form4 Employee = new Form4();
                    Employee.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Неверные данные");
            }
        }

        private void reg_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 RegForm = new Form2();
            RegForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
