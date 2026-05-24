using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using POSSystem.Services;

namespace POSSystem.Forms
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            var store = DataStore.Instance;
            label2.Text = store.Settings.CompanyName;  // pulls from settings
            label3.Text = $"Welcome back, {store.CurrentUser?.FirstName}";
            label4.Text = store.IsOnline ? "● Online" : "○ Offline";
            label4.ForeColor = store.IsOnline ? Color.Green : Color.Red;
            RefreshDashboard();
        }

        private void lblLogo(object sender, EventArgs e)
        {

        }

        private void navPanel(object sender, PaintEventArgs e)
        {

        }

        private void btnAnalytics(object sender, EventArgs e)
        {
            //new AnalyticsForm().ShowDialog(this);
            RefreshDashboard();
        }

        private void btnViewInventory(object sender, EventArgs e)
        {
            new InventoryForm().ShowDialog(this);
            RefreshDashboard();
        }

        private void btnAddProduct(object sender, EventArgs e)
        {
            new AddProductForm().ShowDialog(this);
            RefreshDashboard();
        }

        private void btnCustomers(object sender, EventArgs e)
        {
            new CustomerForm().ShowDialog(this);
            RefreshDashboard();
        }

        private void btnBuildOrder(object sender, EventArgs e)
        {
            new BuildOrderForm().ShowDialog(this);
            RefreshDashboard();
        }

        private void btnTransactionLog(object sender, EventArgs e)
        {
            new TransactionLogForm().ShowDialog(this);
            RefreshDashboard();
        }

        private void btnReceipts(object sender, EventArgs e)
        {
            //new ReceiptsForm().ShowDialog(this);
            RefreshDashboard();
        }

        private void btnAdmin(object sender, EventArgs e)
        {
            new StaffForm().ShowDialog(this);
        }

        private void btnSettings(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog(this);
            RefreshDashboard();
        }

        private void headerPanel(object sender, PaintEventArgs e)
        {

        }

        private void lblCompany(object sender, EventArgs e)
        {

        }

        private void lblWelcome(object sender, EventArgs e)
        {

        }

        private void lblOnlineStatus(object sender, EventArgs e)
        {

        }

        private void lblSyncStatus(object sender, EventArgs e)
        {

        }

        private void btnLogout(object sender, EventArgs e)
        {
            DataStore.Instance.CurrentUser = null;
            new LoginForm().Show();
            this.Close();
        }

        // Refreshes all stats on the dashboard — called on load and when returning from any form
        private void RefreshDashboard()
        {
            var store = DataStore.Instance;

            // Update low stock alert count in the window title
            var lowStock = store.Products.Where(p => p.IsLowStock).ToList();
            this.Text = lowStock.Count > 0
                ? $"Dashboard — {lowStock.Count} low stock alert(s)"
                : "Dashboard";

            // Update stat panels
            UpdateStatCard(panelAmountDue, "Amount Due", "$0.00");
            UpdateStatCard(panelCustomers, "Customers", store.Customers.Count.ToString());
            UpdateStatCard(panelInvoices, "Invoices", SalesService.GetTransactionCount().ToString());
            UpdateStatCard(panelTotalSales, "Total Sales", $"${SalesService.GetSalesTotal():F2}");

            // Update low stock list
            RefreshLowStockAlerts(lowStock);
        }

        // Updates the title and value labels inside a stat card panel
        private void UpdateStatCard(Panel card, string title, string value)
        {
            foreach (Control c in card.Controls)
            {
                if (c is Label lbl)
                {
                    // The smaller label is the title, the larger one is the value
                    if (lbl.Font.Size < 14)
                        lbl.Text = title;
                    else
                        lbl.Text = value;
                }
            }
        }

        // Rebuilds the low stock alert list below the stat cards
        private void RefreshLowStockAlerts(System.Collections.Generic.List<POSSystem.Models.Product> lowStock)
        {
            // Remove any previously added low stock labels (tag them so we can find them again)
            var toRemove = this.Controls
                //Only check if it is a label
                .OfType<Label>()
                //Check if the tag of the label says lowstock
                .Where(l => l.Tag?.ToString() == "lowstock")
                .ToList();
            foreach (var l in toRemove)
                this.Controls.Remove(l);

            // Update the alert heading label
            labelLowStock.Text = $"Low Stock Alerts ({lowStock.Count} items)";
            labelLowStock.ForeColor = lowStock.Count > 0 ? Color.OrangeRed : Color.Green;

            // Add one label per low-stock product below the heading
            int y = labelLowStock.Bottom + 5;
            foreach (var product in lowStock)
            {
                var lbl = new Label
                {
                    Text = $"  • {product.Name} — {product.Stock} remaining",
                    Location = new Point(labelLowStock.Left, y),
                    Size = new Size(400, 22),
                    ForeColor = Color.OrangeRed,
                    Font = new Font("Segoe UI", 9),
                    Tag = "lowstock"   // so we can remove them on next refresh
                };
                this.Controls.Add(lbl);
                lbl.BringToFront();
                y += 22;
            }
        }
    }
}