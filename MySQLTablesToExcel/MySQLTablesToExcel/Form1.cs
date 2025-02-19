using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MySQLTablesToExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            // Get user input values
            string server = MySQLHostAddress.Text;
            string username = Username.Text;
            string password = Password.Text;
            string port = Port.Text;

            // Create MySQL connection string
            string connectionString = $"Server={server};Port={port};Uid={username};Pwd={password};";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open(); // Open connection
                    MessageBox.Show("MySQL connection successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Open new form and pass connection string
                    DatabaseForm dbForm = new DatabaseForm(connectionString);
                    dbForm.Show();

                    conn.Close(); // Close connection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed!\nError: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Port.Text = "3306";
        }
    }
}
