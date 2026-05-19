using System;
using System.Reflection.Emit;
using System.Windows.Forms;
using POSSystem.Models;
using POSSystem.Services;

namespace POSSystem.Forms
{
    public partial class CheckoutForm : Form
    {
        public CheckoutForm()
        {
            InitializeComponent();

            var store = DataStore.Instance;
            var order = store.CurrentOrder;

            // Populate the order summary list
            foreach (var item in order.Items)
            {
                var lbl = new System.Windows.Forms.Label
                {
                    Text = $"{item.Product.Name} × {item.Quantity}  —  ${item.LineTotal:F2}",
                    Location = new System.Drawing.Point(30, _summaryY),
                    Size = new System.Drawing.Size(440, 22),
                    Font = new System.Drawing.Font("Segoe UI", 9),
                    ForeColor = System.Drawing.Color.FromArgb(60, 60, 60)
                };
                this.Controls.Add(lbl);
                _summaryY += 22;
            }

            // Fill in totals
            label3.Text = $"Subtotal: ${order.Subtotal:F2}";
            label4.Text = $"Tax ({store.Settings.TaxRate}%): ${order.Tax:F2}";
            label5.Text = $"TOTAL:  ${order.Total:F2}";

            // Populate customer dropdown
            comboBox1.Items.Add("Walk-in (no customer)");
            foreach (var c in store.Customers)
                comboBox1.Items.Add(c);
            comboBox1.SelectedIndex = 0;

            // Show offline warning panel only when offline
            panel1.Visible = !store.IsOnline;
        }

        private int _summaryY = 55;  // Tracks where to place each order summary row

        private void CheckoutForm_Load(object sender, EventArgs e)
        {

        }

        private void cmbCustomer(object sender, EventArgs e)
        {

        }

        private void rbCash(object sender, EventArgs e)
        {

        }

        private void rbCard(object sender, EventArgs e)
        {

        }

        private void rbGiftCard(object sender, EventArgs e)
        {

        }

        // Finalises the sale — creates the transaction, deducts stock, saves everything
        private void btnComplete(object sender, EventArgs e)
        {
            // Determine which payment method radio button is selected
            string paymentMethod = radioButton1.Checked ? "Cash" : radioButton2.Checked ? "Card" : "Gift Card";

            // Get the customer ID if one was selected (index 0 = Walk-in)
            string customerId = "";
            if (comboBox1.SelectedIndex > 0 && comboBox1.SelectedItem is Customer customer)
                customerId = customer.Id;

            // SalesService handles creating the Transaction, deducting stock, and saving
            var transaction = SalesService.CompleteTransaction(paymentMethod, customerId);

            // Show the confirmation screen
            new TransactionConfirmForm(transaction).ShowDialog(this);

            this.Close();  // Return to Build Order — cart is now empty
        }

        private void btnCancel(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}