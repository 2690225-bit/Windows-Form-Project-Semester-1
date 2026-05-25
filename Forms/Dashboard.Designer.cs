namespace POSSystem.Forms
{
    partial class Dashboard
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.panelAmountDue = new System.Windows.Forms.Panel();
            this.labelAmountDueTitle = new System.Windows.Forms.Label();
            this.labelAmountDueValue = new System.Windows.Forms.Label();
            this.panelCustomers = new System.Windows.Forms.Panel();
            this.labelCustomersTitle = new System.Windows.Forms.Label();
            this.labelCustomersValue = new System.Windows.Forms.Label();
            this.panelInvoices = new System.Windows.Forms.Panel();
            this.labelInvoicesTitle = new System.Windows.Forms.Label();
            this.labelInvoicesValue = new System.Windows.Forms.Label();
            this.panelTotalSales = new System.Windows.Forms.Panel();
            this.labelTotalSalesTitle = new System.Windows.Forms.Label();
            this.labelTotalSalesValue = new System.Windows.Forms.Label();
            this.labelLowStock = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panelAmountDue.SuspendLayout();
            this.panelCustomers.SuspendLayout();
            this.panelInvoices.SuspendLayout();
            this.panelTotalSales.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(162)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button9);
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 700);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.navPanel);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(101, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 60);
            this.label2.TabIndex = 1;
            this.label2.Text = "The Star Company";
            this.label2.Click += new System.EventHandler(this.lblCompany);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = global::WindowsFormsApp1.Properties.Resources.download;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 60);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.lblLogo);
            // 
            // button9
            // 
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(110)))), ((int)(((byte)(200)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(0, 370);
            this.button9.Name = "button9";
            this.button9.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button9.Size = new System.Drawing.Size(200, 45);
            this.button9.TabIndex = 10;
            this.button9.Text = "Settings";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.btnSettings);
            // 
            // button8
            // 
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(110)))), ((int)(((byte)(200)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(0, 325);
            this.button8.Name = "button8";
            this.button8.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button8.Size = new System.Drawing.Size(200, 45);
            this.button8.TabIndex = 9;
            this.button8.Text = "Admin";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.btnAdmin);
            // 
            // button6
            // 
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(110)))), ((int)(((byte)(200)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(0, 280);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button6.Size = new System.Drawing.Size(200, 45);
            this.button6.TabIndex = 7;
            this.button6.Text = "Transaction Log";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnTransactionLog);
            // 
            // button5
            // 
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(110)))), ((int)(((byte)(200)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(0, 235);
            this.button5.Name = "button5";
            this.button5.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button5.Size = new System.Drawing.Size(200, 45);
            this.button5.TabIndex = 6;
            this.button5.Text = "Build Order";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnBuildOrder);
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(110)))), ((int)(((byte)(200)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(0, 190);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button4.Size = new System.Drawing.Size(200, 45);
            this.button4.TabIndex = 5;
            this.button4.Text = "Customers";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnCustomers);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(110)))), ((int)(((byte)(200)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(0, 145);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button3.Size = new System.Drawing.Size(200, 45);
            this.button3.TabIndex = 4;
            this.button3.Text = "Add Product";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnAddProduct);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(110)))), ((int)(((byte)(200)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(0, 100);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(200, 45);
            this.button2.TabIndex = 3;
            this.button2.Text = "Inventory";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnViewInventory);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(200, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(884, 60);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.headerPanel);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(250, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(300, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Welcome back,";
            this.label3.Click += new System.EventHandler(this.lblWelcome);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(580, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "● Online";
            this.label4.Click += new System.EventHandler(this.lblOnlineStatus);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(690, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Sync Status: Synced";
            this.label5.Click += new System.EventHandler(this.lblSyncStatus);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Location = new System.Drawing.Point(860, 12);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(80, 35);
            this.button10.TabIndex = 5;
            this.button10.Text = "Logout";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.btnLogout);
            // 
            // panelAmountDue
            // 
            this.panelAmountDue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(162)))));
            this.panelAmountDue.Controls.Add(this.labelAmountDueTitle);
            this.panelAmountDue.Controls.Add(this.labelAmountDueValue);
            this.panelAmountDue.Location = new System.Drawing.Point(220, 80);
            this.panelAmountDue.Name = "panelAmountDue";
            this.panelAmountDue.Size = new System.Drawing.Size(200, 110);
            this.panelAmountDue.TabIndex = 6;
            // 
            // labelAmountDueTitle
            // 
            this.labelAmountDueTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmountDueTitle.ForeColor = System.Drawing.Color.White;
            this.labelAmountDueTitle.Location = new System.Drawing.Point(10, 10);
            this.labelAmountDueTitle.Name = "labelAmountDueTitle";
            this.labelAmountDueTitle.Size = new System.Drawing.Size(180, 25);
            this.labelAmountDueTitle.TabIndex = 0;
            this.labelAmountDueTitle.Text = "Amount Due";
            // 
            // labelAmountDueValue
            // 
            this.labelAmountDueValue.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmountDueValue.ForeColor = System.Drawing.Color.White;
            this.labelAmountDueValue.Location = new System.Drawing.Point(10, 45);
            this.labelAmountDueValue.Name = "labelAmountDueValue";
            this.labelAmountDueValue.Size = new System.Drawing.Size(180, 50);
            this.labelAmountDueValue.TabIndex = 1;
            this.labelAmountDueValue.Text = "$0.00";
            // 
            // panelCustomers
            // 
            this.panelCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(100)))));
            this.panelCustomers.Controls.Add(this.labelCustomersTitle);
            this.panelCustomers.Controls.Add(this.labelCustomersValue);
            this.panelCustomers.Location = new System.Drawing.Point(440, 80);
            this.panelCustomers.Name = "panelCustomers";
            this.panelCustomers.Size = new System.Drawing.Size(200, 110);
            this.panelCustomers.TabIndex = 7;
            // 
            // labelCustomersTitle
            // 
            this.labelCustomersTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomersTitle.ForeColor = System.Drawing.Color.White;
            this.labelCustomersTitle.Location = new System.Drawing.Point(10, 10);
            this.labelCustomersTitle.Name = "labelCustomersTitle";
            this.labelCustomersTitle.Size = new System.Drawing.Size(180, 25);
            this.labelCustomersTitle.TabIndex = 0;
            this.labelCustomersTitle.Text = "Customers";
            // 
            // labelCustomersValue
            // 
            this.labelCustomersValue.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomersValue.ForeColor = System.Drawing.Color.White;
            this.labelCustomersValue.Location = new System.Drawing.Point(10, 45);
            this.labelCustomersValue.Name = "labelCustomersValue";
            this.labelCustomersValue.Size = new System.Drawing.Size(180, 50);
            this.labelCustomersValue.TabIndex = 1;
            this.labelCustomersValue.Text = "0";
            // 
            // panelInvoices
            // 
            this.panelInvoices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(100)))), ((int)(((byte)(30)))));
            this.panelInvoices.Controls.Add(this.labelInvoicesTitle);
            this.panelInvoices.Controls.Add(this.labelInvoicesValue);
            this.panelInvoices.Location = new System.Drawing.Point(660, 80);
            this.panelInvoices.Name = "panelInvoices";
            this.panelInvoices.Size = new System.Drawing.Size(200, 110);
            this.panelInvoices.TabIndex = 8;
            // 
            // labelInvoicesTitle
            // 
            this.labelInvoicesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoicesTitle.ForeColor = System.Drawing.Color.White;
            this.labelInvoicesTitle.Location = new System.Drawing.Point(10, 10);
            this.labelInvoicesTitle.Name = "labelInvoicesTitle";
            this.labelInvoicesTitle.Size = new System.Drawing.Size(180, 25);
            this.labelInvoicesTitle.TabIndex = 0;
            this.labelInvoicesTitle.Text = "Invoices";
            // 
            // labelInvoicesValue
            // 
            this.labelInvoicesValue.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoicesValue.ForeColor = System.Drawing.Color.White;
            this.labelInvoicesValue.Location = new System.Drawing.Point(10, 45);
            this.labelInvoicesValue.Name = "labelInvoicesValue";
            this.labelInvoicesValue.Size = new System.Drawing.Size(180, 50);
            this.labelInvoicesValue.TabIndex = 1;
            this.labelInvoicesValue.Text = "0";
            // 
            // panelTotalSales
            // 
            this.panelTotalSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(30)))), ((int)(((byte)(200)))));
            this.panelTotalSales.Controls.Add(this.labelTotalSalesTitle);
            this.panelTotalSales.Controls.Add(this.labelTotalSalesValue);
            this.panelTotalSales.Location = new System.Drawing.Point(880, 80);
            this.panelTotalSales.Name = "panelTotalSales";
            this.panelTotalSales.Size = new System.Drawing.Size(200, 110);
            this.panelTotalSales.TabIndex = 9;
            // 
            // labelTotalSalesTitle
            // 
            this.labelTotalSalesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalSalesTitle.ForeColor = System.Drawing.Color.White;
            this.labelTotalSalesTitle.Location = new System.Drawing.Point(10, 10);
            this.labelTotalSalesTitle.Name = "labelTotalSalesTitle";
            this.labelTotalSalesTitle.Size = new System.Drawing.Size(180, 25);
            this.labelTotalSalesTitle.TabIndex = 0;
            this.labelTotalSalesTitle.Text = "Total Sales";
            // 
            // labelTotalSalesValue
            // 
            this.labelTotalSalesValue.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalSalesValue.ForeColor = System.Drawing.Color.White;
            this.labelTotalSalesValue.Location = new System.Drawing.Point(10, 45);
            this.labelTotalSalesValue.Name = "labelTotalSalesValue";
            this.labelTotalSalesValue.Size = new System.Drawing.Size(180, 50);
            this.labelTotalSalesValue.TabIndex = 1;
            this.labelTotalSalesValue.Text = "$0.00";
            // 
            // labelLowStock
            // 
            this.labelLowStock.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLowStock.ForeColor = System.Drawing.Color.Green;
            this.labelLowStock.Location = new System.Drawing.Point(220, 210);
            this.labelLowStock.Name = "labelLowStock";
            this.labelLowStock.Size = new System.Drawing.Size(400, 28);
            this.labelLowStock.TabIndex = 10;
            this.labelLowStock.Text = "Low Stock Alerts (0 items)";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 661);
            this.Controls.Add(this.labelLowStock);
            this.Controls.Add(this.panelTotalSales);
            this.Controls.Add(this.panelInvoices);
            this.Controls.Add(this.panelCustomers);
            this.Controls.Add(this.panelAmountDue);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panel2.ResumeLayout(false);
            this.panelAmountDue.ResumeLayout(false);
            this.panelCustomers.ResumeLayout(false);
            this.panelInvoices.ResumeLayout(false);
            this.panelTotalSales.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Panel panelAmountDue;
        private System.Windows.Forms.Panel panelCustomers;
        private System.Windows.Forms.Panel panelInvoices;
        private System.Windows.Forms.Panel panelTotalSales;
        private System.Windows.Forms.Label labelAmountDueTitle;
        private System.Windows.Forms.Label labelAmountDueValue;
        private System.Windows.Forms.Label labelCustomersTitle;
        private System.Windows.Forms.Label labelCustomersValue;
        private System.Windows.Forms.Label labelInvoicesTitle;
        private System.Windows.Forms.Label labelInvoicesValue;
        private System.Windows.Forms.Label labelTotalSalesTitle;
        private System.Windows.Forms.Label labelTotalSalesValue;
        private System.Windows.Forms.Label labelLowStock;
    }
}