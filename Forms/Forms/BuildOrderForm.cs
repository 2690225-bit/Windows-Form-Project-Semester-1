using System;
using System.Linq;
using System.Windows.Forms;
using POSSystem.Models;
using POSSystem.Services;

namespace POSSystem.Forms
{
    public partial class BuildOrderForm : Form
    {
        public BuildOrderForm()
        {
            InitializeComponent();

            // Set up product list columns and load products on startup
            RefreshProductList();
            RefreshCart();
        }

        private void BuildOrderForm_Load(object sender, EventArgs e)
        {

        }

        private void txtSearch(object sender, EventArgs e)
        {
            RefreshProductList();
        }

        private void lstProducts_DoubleClick(object sender, EventArgs e)
        {
            AddToCart();
        }

        private void btnAddToCart(object sender, EventArgs e)
        {
            AddToCart();
        }

        private void btnRemoveItem(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            // Retrieve the OrderItem stored in the row's Tag
            var item = listView1.SelectedItems[0].Tag as OrderItem;
            if (item != null)
            {
                DataStore.Instance.CurrentOrder.RemoveItem(item.Product.SKU);
                RefreshCart();
            }
        }

        private void btnClearCart(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all items from cart?", "Confirm",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataStore.Instance.CurrentOrder.Clear();
                RefreshCart();
            }
        }

        private void btnCheckout(object sender, EventArgs e)
        {
            if (DataStore.Instance.CurrentOrder.Items.Count == 0)
            {
                MessageBox.Show("Cart is empty.");
                return;
            }

            var checkout = new CheckoutForm();
            checkout.ShowDialog(this);

            // If checkout completed, the cart will be empty — close this form
            if (DataStore.Instance.CurrentOrder.Items.Count == 0)
                this.Close();
            else
                RefreshCart();
        }

        private void lvCart(object sender, EventArgs e)
        {

        }

        // Reloads the product ListBox, filtered by the search box
        private void RefreshProductList()
        {
            listBox1.Items.Clear();

            var products = InventoryService.SearchProducts(textBox1.Text);

            // Only show products that are in stock
            foreach (var p in products.Where(p => p.Stock > 0))
                listBox1.Items.Add(p);   // Product.ToString() shows "SKU | Name | $Price | Stock units"
        }

        private void AddToCart()
        {
            if (listBox1.SelectedItem is not Product product) return;

            var order = DataStore.Instance.CurrentOrder;

            // Check how many of this product are already in the cart
            var existing = order.Items.FirstOrDefault(i => i.Product.SKU == product.SKU);
            int currentQty = existing?.Quantity ?? 0;

            if (currentQty >= product.Stock)
            {
                MessageBox.Show($"Cannot add more '{product.Name}' — only {product.Stock} in stock.");
                return;
            }

            // Order.AddItem handles the case where the product is already in the cart
            order.AddItem(product, 1);
            RefreshCart();
        }

        // Updates the cart ListView and all totals
        private void RefreshCart()
        {
            listView1.Items.Clear();
            var order = DataStore.Instance.CurrentOrder;

            foreach (var item in order.Items)
            {
                var row = new ListViewItem(item.Product.Name);
                row.SubItems.Add(item.Quantity.ToString());
                row.SubItems.Add($"${item.UnitPrice:F2}");
                row.SubItems.Add($"${item.LineTotal:F2}");

                // Tag stores the OrderItem so we can remove it by row
                row.Tag = item;
                listView1.Items.Add(row);
            }

            // All totals are computed properties on the Order object
            label2.Text = $"Items in cart: {order.totalitemcount}";
            label3.Text = $"Subtotal: ${order.Subtotal:F2}";
            label4.Text = $"Tax ({DataStore.Instance.Settings.TaxRate}%): ${order.Tax:F2}";
            label5.Text = $"TOTAL: ${order.Total:F2}";
        }
    }
}