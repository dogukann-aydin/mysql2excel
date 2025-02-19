using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System.IO;

namespace MySQLTablesToExcel
{
    public partial class DatabaseForm : Form
    {
        private string connectionString;

        public DatabaseForm(string connString)
        {
            InitializeComponent();
            connectionString = connString;
        }

        private void DatabaseForm_Load(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial
            LoadDatabasesAndTables();
        }

        private void LoadDatabasesAndTables()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // 1. Get all databases
                    string queryDatabases = "SHOW DATABASES";
                    MySqlCommand cmd = new MySqlCommand(queryDatabases, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    treeViewDatabases.Nodes.Clear();

                    while (reader.Read())
                    {
                        string databaseName = reader.GetString(0);
                        TreeNode databaseNode = new TreeNode(databaseName);
                        treeViewDatabases.Nodes.Add(databaseNode);
                    }

                    reader.Close();

                    // 2. Get tables in each database
                    foreach (TreeNode dbNode in treeViewDatabases.Nodes)
                    {
                        string queryTables = $"SHOW TABLES FROM `{dbNode.Text}`";
                        MySqlCommand cmdTables = new MySqlCommand(queryTables, conn);
                        MySqlDataReader tableReader = cmdTables.ExecuteReader();

                        while (tableReader.Read())
                        {
                            string tableName = tableReader.GetString(0);
                            TreeNode tableNode = new TreeNode(tableName);
                            dbNode.Nodes.Add(tableNode);
                        }

                        tableReader.Close();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving database information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcelButton_Click(object sender, EventArgs e)
        {
            // Get selected table
            if (treeViewDatabases.SelectedNode == null || treeViewDatabases.SelectedNode.Parent == null)
            {
                MessageBox.Show("Please select a table!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string databaseName = treeViewDatabases.SelectedNode.Parent.Text; // Database name
            string tableName = treeViewDatabases.SelectedNode.Text; // Table name

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Save Excel File",
                FileName = $"{tableName}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportTableToExcel(databaseName, tableName, saveFileDialog.FileName);
            }
        }

        private void ExportTableToExcel(string database, string table, string filePath)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = $"SELECT * FROM `{database}`.`{table}`";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(table);
                        worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                        File.WriteAllBytes(filePath, package.GetAsByteArray());
                    }

                    conn.Close();
                }

                MessageBox.Show("Table successfully exported to Excel!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
