namespace MySQLTablesToExcel
{
    partial class DatabaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewDatabases = new System.Windows.Forms.TreeView();
            this.ExportToExcelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewDatabases
            // 
            this.treeViewDatabases.Location = new System.Drawing.Point(12, 12);
            this.treeViewDatabases.Name = "treeViewDatabases";
            this.treeViewDatabases.Size = new System.Drawing.Size(540, 285);
            this.treeViewDatabases.TabIndex = 0;
            // 
            // ExportToExcelButton
            // 
            this.ExportToExcelButton.Location = new System.Drawing.Point(436, 304);
            this.ExportToExcelButton.Name = "ExportToExcelButton";
            this.ExportToExcelButton.Size = new System.Drawing.Size(116, 30);
            this.ExportToExcelButton.TabIndex = 1;
            this.ExportToExcelButton.Text = "Export";
            this.ExportToExcelButton.UseVisualStyleBackColor = true;
            this.ExportToExcelButton.Click += new System.EventHandler(this.ExportToExcelButton_Click);
            // 
            // DatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 346);
            this.Controls.Add(this.ExportToExcelButton);
            this.Controls.Add(this.treeViewDatabases);
            this.Name = "DatabaseForm";
            this.Text = "DatabaseForm";
            this.Load += new System.EventHandler(this.DatabaseForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewDatabases;
        private System.Windows.Forms.Button ExportToExcelButton;
    }
}