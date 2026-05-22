using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using POSSystem.Models;
using POSSystem.Services;

namespace POSSystem.Forms
{
    // InventoryForm inherits from Form, giving it all standard window behaviour automatically
    public partial class InventoryForm : Form
    {
        public InventoryForm()
        {
            InitializeComponent();

            // Add column headers to the ListView table
            listView1.Columns.Add("SKU", 100);
            listView1.Columns.Add("Product Name", 220);
            listView1.Columns.Add("Category", 130);
            listView1.Columns.Add("Price", 90);
            listView1.Columns.Add("Stock", 80);
            listView1.Columns.Add("Last Updated", 150);

            // If a row is double clicked, it will open the edit product form
            listView1.DoubleClick += (s, e) =>
            {
                var product = GetSelectedProduct();
                if (product != null)
                    new EditProductForm(product).ShowDialog(this);
                RefreshList();
            };

            // Load the inventory on startup
            RefreshList();
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {

        }

        // Fires every time a key is pressed in the search box
        private void txtSearch(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void lblSearch(object sender, EventArgs e)
        {

        }

        // SelectedIndexChanged fires when the user clicks a row
        private void lvInventory(object sender, EventArgs e)
        {

        }

        private void btnAddProduct(object sender, EventArgs e)
        {
            new AddProductForm().ShowDialog(this);
            RefreshList(); // Refresh list after adding so the DataStore updates
        }

        private void btnEdit(object sender, EventArgs e)
        {
            var product = GetSelectedProduct();
            if (product == null) { MessageBox.Show("Please select a product."); return; }
            new EditProductForm(product).ShowDialog(this);
            RefreshList();
        }

        private void btnDelete(object sender, EventArgs e)
        {
            var product = GetSelectedProduct();
            if (product == null) { MessageBox.Show("Please select a product."); return; }

            // Confirm before deleting
            var result = MessageBox.Show($"Are you sure you want to delete '{product.Name}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DataStore.Instance.Products.Remove(product);
                FileManager.SaveAll(); // Persist the deletion to disk immediately
                RefreshList();
            }
        }

        private void RefreshList()
        {
            // Remove all existing rows
            listView1.Items.Clear();

            string search = textBox1.Text.ToLower();
            var store = DataStore.Instance;

            // Filter products by search term (checks name, SKU and category)
            var products = store.Products
                .Where(p =>
                    string.IsNullOrEmpty(search) ||
                    p.Name.ToLower().Contains(search) ||
                    p.SKU.ToLower().Contains(search) ||
                    p.Category.ToLower().Contains(search)
                )
                .OrderBy(p => p.Stock) // Sort by stock so low stock items appear at the top
                .ToList();

            // Add each product as a row in the ListView table
            foreach (var product in products)
            {
                // ListViewItem = one row. The first argument is the first column's text.
                var item = new ListViewItem(product.SKU);

                // These subitems are the additional columns in the table
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.Category);
                item.SubItems.Add($"${product.Price:F2}");
                item.SubItems.Add(product.Stock.ToString());
                item.SubItems.Add(product.LastUpdated.ToString("dd/MM/yyyy"));

                // Highlight low stock items in orange so they stand out
                if (product.IsLowStock)
                    item.BackColor = System.Drawing.Color.FromArgb(255, 220, 180);

                // Tag stores the Product object so we can retrieve it when a row is clicked
                item.Tag = product;
                listView1.Items.Add(item);
            }
        }

        private Product GetSelectedProduct()
        {
            if (listView1.SelectedItems.Count == 0) return null;
            // Cast the Tag back to Product (it was stored as object)
            return listView1.SelectedItems[0].Tag as Product;
        }
    }

    public class AddProductForm : Form
    {
        private TextBox txtName, txtCategory, txtPrice, txtStock;
        private Button btnSave, btnCancel;

        public AddProductForm()
        {
            this.Text = "Add Product";
            this.Size = new Size(400, 450);
            BuildForm();
        }

        private void BuildForm()
        {
            AddLabel("Product Name", 30); txtName = AddTextBox(55);
            AddLabel("Category", 95); txtCategory = AddTextBox(120);
            AddLabel("Price ($)", 160); txtPrice = AddTextBox(185);
            AddLabel("Stock Quantity", 225); txtStock = AddTextBox(250);

            btnSave = new Button { Text = "Save", Top = 295, Left = 50 };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button { Text = "Cancel", Top = 295, Left = 150 };
            // s = the button clicked, e = extra click info (neither needed here, we just want to close)
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { txtName, txtCategory, txtPrice, txtStock, btnSave, btnCancel });
            
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate that name is filled in
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Product name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check that product name contains only letters, numbers, spaces, hyphens, or periods.
            // If it contains other characters, show a warning and stop processing.
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtName.Text, @"^[a-zA-Z0-9\s\-\.]+$"))
            {
                MessageBox.Show("Product name contains invalid characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate that price is a valid number
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate that stock is a valid number
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Please enter a valid stock quantity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var store = DataStore.Instance;

            // Create and save the new product
            var product = new Product
            {
                SKU = store.GenerateSKU(),
                Name = txtName.Text.Trim(),
                Category = txtCategory.Text.Trim(),
                Price = price,
                Stock = stock,
                LastUpdated = DateTime.Now
            };

            store.Products.Add(product);

            // Immediately sends the information into permanent storage rather than temporary storage like RAM
            FileManager.SaveAll();

            MessageBox.Show($"Product '{product.Name}' added successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private Label AddLabel(string text, int y)
        {
            var lbl = new Label { Text = text, Top = y, Left = 50 };
            this.Controls.Add(lbl);
            return lbl;
        }

        private TextBox AddTextBox(int y)
        {
            var txt = new TextBox { Top = y, Left = 50, Width = 300 };
            this.Controls.Add(txt);
            return txt;
        }
    }

    public class EditProductForm : Form
    {
        // Store a reference to the product being edited
        private readonly Product _product;
        private TextBox txtName, txtCategory, txtPrice, txtStock;

        // Constructor takes the Product to edit — we pre-fill the fields with existing data
        public EditProductForm(Product product)
        {
            _product = product;
            this.Text = "Edit Product";
            this.Size = new Size(400, 450);

            AddLabel("Product Name", 30); txtName = AddTextBox(55, _product.Name);
            AddLabel("Category", 95); txtCategory = AddTextBox(120, _product.Category);
            AddLabel("Price ($)", 160); txtPrice = AddTextBox(185, _product.Price.ToString());
            AddLabel("Stock Quantity", 225); txtStock = AddTextBox(250, _product.Stock.ToString());

            var btnSave = new Button { Text = "Save Product", Top = 295, Left = 50, Width = 300 };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {   
                    MessageBox.Show("Name is required."); 
                    return; 
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price)) 
                { 
                    MessageBox.Show("Enter a valid price.");
                    return; 
                }

                if (!int.TryParse(txtStock.Text, out int stock)) 
                { 
                    MessageBox.Show("Enter a valid stock quantity."); 
                    return; 
                }

                // Check that product name contains only letters, numbers, spaces, hyphens, or periods.
                // If it contains other characters, show a warning and stop processing.
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtName.Text, @"^[a-zA-Z0-9\s\-\.]+$"))
                {
                    MessageBox.Show("Product name contains invalid characters.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Because DataStore holds a reference to this same object, if a change is made here, it is made everywhere
                _product.Name = txtName.Text.Trim();
                _product.Category = txtCategory.Text.Trim();
                _product.Price = price;
                _product.Stock = stock;
                _product.LastUpdated = DateTime.Now;

                FileManager.SaveAll();
                MessageBox.Show("Product updated successfully!");
                this.Close();
            };

            this.Controls.Add(btnSave);
        }

        private Label AddLabel(string text, int y)
        {
            var lbl = new Label { Text = text, Top = y, Left = 50 };
            this.Controls.Add(lbl);
            return lbl;
        }

        private TextBox AddTextBox(int y, string defaultValue = "")
        {
            var txt = new TextBox
            {
                Top = y,
                Left = 50,
                Width = 300,
                Text = defaultValue  // Pre-fill with the product's current value
            };
            this.Controls.Add(txt);
            return txt;
        }
    }
}