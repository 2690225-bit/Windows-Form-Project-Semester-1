using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using POSSystem.Models;
using POSSystem.Services;

namespace POSSystem.Forms
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();

            // Add column headers to the ListView table
            listView1.Columns.Add("Name", 200);
            listView1.Columns.Add("Email", 250);
            listView1.Columns.Add("Phone", 150);
            listView1.Columns.Add("ID", 100);

            //If a customer's row is double clicked, then it will view the customer
            listView1.DoubleClick += (s, e) =>
            {
                var customer = GetSelectedCustomer();
                if (customer != null)
                    new ViewCustomerForm(customer).ShowDialog(this);
            };

            //This will load the customers on startup
            RefreshList();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {

        }

        // TextChanged fires every time a key is pressed
        private void txtSearch(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void lblSearch(object sender, EventArgs e)
        {

        }

        private void btnEdit(object sender, EventArgs e)
        {
            // Get the selected customer from the ListView
            var customer = GetSelectedCustomer();
            if (customer == null) { MessageBox.Show("Please select a customer."); return; }
            new EditCustomerForm(customer).ShowDialog(this);
            RefreshList();
        }

        private void btnView(object sender, EventArgs e)
        {
            var customer = GetSelectedCustomer();
            if (customer == null) { MessageBox.Show("Please select a customer."); return; }
            new ViewCustomerForm(customer).ShowDialog(this);
        }

        private void btnAddCustomer(object sender, EventArgs e)
        {
            new AddCustomerForm().ShowDialog(this);
            RefreshList(); // Refresh the list after adding so the Datastore can store the same information as well
        }

        // SelectedIndexChanged fires when the user clicks a row
        private void lvCustomers(object sender, EventArgs e)
        {

        }

        private void RefreshList()
        {
            // Remove all existing rows
            listView1.Items.Clear();

            string search = textBox1.Text.ToLower();
            var store = DataStore.Instance;

            // Filter customers by search term (checks name, email, and phone)
            var customers = store.Customers
               .Where(c =>
                   string.IsNullOrEmpty(search) ||
                   c.Name.ToLower().Contains(search) ||
                   c.Email.ToLower().Contains(search) ||
                   c.Phone.ToLower().Contains(search)
               )
               .ToList();

            // Add each customer as a row in the ListView table
            foreach (var customer in customers)
            {
                // ListViewItem = one row. The first argument is the first column's text.
                var item = new ListViewItem(customer.Name);

                // These subitems are additional columns in the table
                item.SubItems.Add(customer.Email);
                item.SubItems.Add(customer.Phone);
                item.SubItems.Add(customer.Id);

                // Tag is a general-purpose property that can hold any object
                item.Tag = customer;
                listView1.Items.Add(item);
            }
        }

        private Customer GetSelectedCustomer()
        {
            if (listView1.SelectedItems.Count == 0) return null;
            // Cast the Tag back to Customer (it was stored as object)
            return listView1.SelectedItems[0].Tag as Customer;
        }
    }

    public class AddCustomerForm : Form
    {
        private TextBox txtName, txtEmail, intPhone;
        private Button btnSave, btnCancel;

        public AddCustomerForm()
        {
            this.Text = "Add Customer";
            this.Size = new Size(400, 450);

            BuildForm();
        }

        private void BuildForm()
        {
            AddLabel("Name", 30); txtName = AddTextBox(55);
            AddLabel("Email", 95); txtEmail = AddTextBox(120);
            AddLabel("Phone", 160); intPhone = AddTextBox(185);

            btnSave = new Button { Text = "Save", Top = 230, Left = 50 };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button { Text = "Cancel", Top = 230, Left = 150 };
            // s = the button clicked, e = extra click info (neither needed here, we just want to close)
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { txtName, txtEmail, intPhone, btnSave, btnCancel });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Name and Email are required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var store = DataStore.Instance;

            // Create and save a new customer
            var customer = new Customer
            {
                Id = store.GenerateCustomerId(),
                Name = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = intPhone.Text.Trim()
            };

            store.Customers.Add(customer);

            // Immediately sends the information into permanent storage rather than temporary storage like RAM
            FileManager.SaveAll();

            MessageBox.Show($"Customer '{customer.Name}' added successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Returns to the customers list
            this.Close();
        }

        //Styling for all Labels
        private Label AddLabel(string text, int y)
        {
            var lbl = new Label { Text = text, Top = y, Left = 50 };
            this.Controls.Add(lbl);
            return lbl;
        }

        //Styling for all TextBoxes
        private TextBox AddTextBox(int y)
        {
            var txt = new TextBox { Top = y, Left = 50, Width = 300 };
            this.Controls.Add(txt);
            return txt;
        }
    }

    public class EditCustomerForm : Form
    {
        // Store a reference to the customer being edited
        private readonly Customer _customer;
        private TextBox txtName, txtEmail, intPhone;

        // Constructor takes the Customer to edit — we pre-fill the fields with existing data
        public EditCustomerForm(Customer customer)
        {
            _customer = customer;
            this.Text = "Edit Customer";
            this.Size = new Size(400, 450);

            AddLabel("Name", 30); txtName = AddTextBox(55, _customer.Name);
            AddLabel("Email", 95); txtEmail = AddTextBox(120, _customer.Email);
            AddLabel("Phone", 160); intPhone = AddTextBox(185, _customer.Phone);

            var btnSave = new Button { Text = "Save Customer", Top = 225, Left = 50, Width = 300 };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Name is required");
                    return;
                }

                // Because DataStore holds a reference to this same object, if a change is made here, it is made everywhere
                _customer.Name = txtName.Text.Trim();
                _customer.Email = txtEmail.Text.Trim();
                _customer.Phone = intPhone.Text.Trim();

                FileManager.SaveAll();
                MessageBox.Show("Customer updated successfully!");
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
                Text = defaultValue  // Pre-fill with the customer's current value
            };
            this.Controls.Add(txt);
            return txt;
        }
    }

    public class ViewCustomerForm : Form
    {
        public ViewCustomerForm(Customer customer)
        {
            this.Text = "View Customer";

            // Displays the customer information as read-only labels
            int y = 20;
            AddRow("Name:", customer.Name, ref y);
            AddRow("Email:", customer.Email, ref y);
            AddRow("Phone:", customer.Phone, ref y);

            // The purchase history section
            var lblHistory = new Label
            {
                Text = "Purchase History:",
                Top = y + 20,
                Left = 30,
                Width = 380
            };
            this.Controls.Add(lblHistory);
            y += 50;

            var store = DataStore.Instance;

            // The following code shows each transaction in the customer's history
            foreach (var txnId in customer.PurchaseHistory)
            {
                // Look up the full transaction from their ID
                var txn = store.Transactions.FirstOrDefault(t => t.TransactionId == txnId);

                // Checks if a transaction exists. If yes, forms a nicely formatted display string
                // with the ID, total and date. If no, it just shows the ID.
                string display = txn != null
                    ? $"{txn.TransactionId} — ${txn.Total:F2} on {txn.Date:dd/MM/yyyy}"
                    : txnId;

                var lbl = new Label { Text = $"  • {display}", Top = y, Left = 30, Width = 380 };
                this.Controls.Add(lbl);
                y += 22;
            }

            if (customer.PurchaseHistory.Count == 0)
            {
                var lbl = new Label { Text = "  No purchases yet.", Top = y, Left = 30, Width = 380 };
                this.Controls.Add(lbl);
            }
        }

        // 'ref y' passes y by reference — the method can modify the caller's variable directly
        private void AddRow(string label, string value, ref int y)
        {
            var lbl = new Label { Text = label, Top = y, Left = 30, Width = 100 };
            var val = new Label { Text = value, Top = y, Left = 140, Width = 280 };
            this.Controls.AddRange(new Control[] { lbl, val });
            y += 30;
        }
    }
}