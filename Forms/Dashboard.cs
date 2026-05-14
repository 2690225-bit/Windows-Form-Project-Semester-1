using System;
using System.Linq;
using System.Windows.Forms;
using POSSystem.Services;

namespace POSSystem.Forms
{
    // Dashboard inherits from Form, giving it all standard window behaviour automatically
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();

            // Show low stock alerts on startup
            RefreshLowStockAlerts();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void lblLogo(object sender, EventArgs e)
        {

        }

        private void navPanel(object sender, PaintEventArgs e)
        {

        }

        private void btnAnalytics(object sender, EventArgs e)
        {

        }

        private void btnViewInventory(object sender, EventArgs e)
        {
            // Open the inventory form and wait for it to close before returning to dashboard
            new InventoryForm().ShowDialog(this);
        }

        private void btnAddProduct(object sender, EventArgs e)
        {
            // Open the add product form directly from the nav
            new AddProductForm().ShowDialog(this);
        }

        private void btnCustomers(object sender, EventArgs e)
        {
            new CustomerForm().ShowDialog(this);
        }

        private void btnBuildOrder(object sender, EventArgs e)
        {

        }

        private void btnTransactionLog(object sender, EventArgs e)
        {

        }

        private void btnReceipts(object sender, EventArgs e)
        {

        }

        private void btnAdmin(object sender, EventArgs e)
        {

        }

        private void btnSettings(object sender, EventArgs e)
        {

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
            // Log the user out and return to the login screen
            DataStore.Instance.CurrentUser = null;
            new LoginForm().Show();

            // Hide rather than close so the app doesn't shut down
            this.Hide();
        }

        private void RefreshLowStockAlerts()
        {
            var store = DataStore.Instance;

            // Get all products where stock is below 10
            var lowStockProducts = store.Products
                .Where(p => p.IsLowStock)
                .ToList();

            // Update the window title to show how many low stock alerts there are
            if (lowStockProducts.Count > 0)
                this.Text = $"Dashboard — ⚠ {lowStockProducts.Count} low stock alert(s)";
            else
                this.Text = "Dashboard";
        }
    }
}