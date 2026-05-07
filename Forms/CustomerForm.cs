using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using POSSystem.Models;
using POSSystem.Services;

namespace POSSystem.Forms
{
    public partial class CustomersForm : Form
    {
        private ListView lvCustomers;
        private TextBox txtSearch;
        private Button btnEdit, btnView, btnAdd;
        private Button button1;
        private Button button2;

        public CustomersForm()
        {
            InitializeComponent();
            this.Text = "Customers";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            //The following is for the search bar
            var lblSearch = new Label { Text = "Search:", Location = new Point(20,20), Size = new Size(60, 25) };
            txtSearch = new TextBox
            {
                Location = new Point(80, 18),
                Size = new Size(300, 28),
                Font = new Font("Times New Roman", 10)
            };
            //TextChanged fires every time a key is pressed
            txtSearch.TextChanged += (s, e) =>
            {
                RefreshList();  // Refresh the list after adding
            };

            btnAdd = MakeButton("Add Customer", 600, 15, Color.FromArgb(30, 80, 162));
            btnAdd.Width = 130;

            btnAdd.Click += (s, e) =>
            {
                new AddCustomerForm().ShowDialog(this);
                RefreshList();
            };

            btnEdit = MakeButton("Edit", 400, 15, Color.FromArgb(100, 150, 200));
            btnEdit.Width = 90;
            btnEdit.Click += (s, e) =>
            {
                // Get the selected customer from the ListView
                var customer = GetSelectedCustomer();
                if (customer == null) { MessageBox.Show("Please select a customer."); return; }
                new EditCustomerForm(customer).ShowDialog(this);
                RefreshList();
            };

            btnView = MakeButton("View", 500, 15, Color.FromArgb(80, 130, 80));
            btnView.Width = 90;
            btnView.Click += (s, e) =>
            {
                var customer = GetSelectedCustomer();
                if (customer == null) { MessageBox.Show("Please select a customer."); return; }
                new ViewCustomerForm(customer).ShowDialog(this);
            };

            // ListView in Details mode shows information in the form of a table
            lvCustomers = new ListView()
            {
                Location = new Point(20, 60), //This shows the location of where the table is
                Size = new Size(750, 490), //This shows the size of the table
                View = View.Details,      // Show in table/grid format
                FullRowSelect = true,               // Clicking anywhere on a row selects the whole row
                GridLines = true,               // Show grid lines between rows/columns
                Font = new Font("Times New Roman", 10)
            };

            // Add column headers to the ListView table
            lvCustomers.Columns.Add("Name", 200);
            lvCustomers.Columns.Add("Email", 250);
            lvCustomers.Columns.Add("Phone", 150);
            lvCustomers.Columns.Add("ID", 100);

            //If a customer's row is double clicked, then it will view the customer
            lvCustomers.DoubleClick += (s, e) =>
            {
                var customer = GetSelectedCustomer();
                if (customer != null)
                    new ViewCustomerForm(customer).ShowDialog(this);
            };

            this.Controls.AddRange(new Control[]    
            {
                lblSearch, txtSearch, btnAdd, btnEdit, btnView, lvCustomers
            });

            //This will load the customers on startup
            RefreshList();  
        }

        private void RefreshList()
        {
            //Remove all existing rows
            lvCustomers.Items.Clear();

            string search = txtSearch.Text.ToLower();
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

            //Add each customer as a row in the ListView table
            foreach ( var customer in customers )
            {
                //ListViewItem = one row. The first argument is the first column's text.
                var item = new ListViewItem(customer.Name);

                //These subitems are additional columns in the table
                item.SubItems.Add(customer.Email);
                item.SubItems.Add(customer.Phone);
                item.SubItems.Add(customer.Id);

                // Tag is a general-purpose property that can hold any object
                item.Tag = customer;
                lvCustomers.Items.Add(item);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CustomersForm
            // 
            this.ClientSize = new System.Drawing.Size(1570, 695);
            this.Name = "CustomersForm";
            this.Load += new System.EventHandler(this.CustomersForm_Load);
            this.ResumeLayout(false);

        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {

        }

        private Customer GetSelectedCustomer()
        {
            if (lvCustomers.SelectedItems.Count == 0) return null;
            // Cast the Tag back to Customer (it was stored as object)
            return lvCustomers.SelectedItems[0].Tag as Customer;
        }

        //The following function is used to reduce button creation repitition
        private Button MakeButton(string text, int x, int y, Color backColor)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(130, 35),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Times New Roman", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }
    }

    public class AddCustomerForm : Form
    {
        private TextBox txtName, txtEmail, txtPhone;
        private Button btnSave, btnCancel;

        public AddCustomerForm()
        {
            this.Text = "Add Customer";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            BuildForm();
        }

        private void BuildForm()
        {
            AddLabel("Name", 30); txtName = AddTextBox(55);
            AddLabel("Email", 95); txtEmail = AddTextBox(120);
            AddLabel("Phone", 160); txtPhone = AddTextBox(185);

            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(50, 230),
                Size = new Size(130, 38),
                BackColor = Color.FromArgb(30, 80, 162),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Times New Roman", 10, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(210, 230),
                Size = new Size(130, 38),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Times New Roman", 10)
            };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { txtName, txtEmail, txtPhone, btnSave, btnCancel });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //vallidate required fields
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Name and Email are required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var store = DataStore.Instance;

            //Create and Save a new customer
            var customer = new Customer
            {
                Id = store.GenerateCustomerId(),
                Name = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim()
            };

            store.Customers.Add(customer);

            //immediatly sends the information into permanent storage rather than temporary storage like RAM
            FileManager.SaveAll();

            MessageBox.Show($"Customer '{customer.Name}' added successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Returns to the customers list
            this.Close();
        }
        private Label AddLabel(string text, int y)
        {
            var lbl = new Label { Text = text, Location = new Point(50, y), Size = new Size(200, 20), Font = new Font("Times New Roman", 9) };
            this.Controls.Add(lbl);
            return lbl;
        }

        private TextBox AddTextBox(int y)
        {
            var txt = new TextBox { Location = new Point(50, y), Size = new Size(300, 28), Font = new Font("Times New Roman", 10), BorderStyle = BorderStyle.FixedSingle };
            this.Controls.Add(txt);
            return txt;
        }
    }

    public class EditCustomerForm : Form
    {
        private readonly Customer _customer;
        private TextBox txtName, txtEmail, txtPhone;

        public EditCustomerForm(Customer customer)
        {
            _customer          = customer;  
            this.Text          = "Edit Customer";
            this.Size          = new Size(400, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor     = Color.White;

            AddLabel("Name",  30); txtName  = AddTextBox(55, _customer.Name);
            AddLabel("Email", 95); txtEmail = AddTextBox(120, _customer.Email);
            AddLabel("Phone", 160); txtPhone = AddTextBox(185, _customer.Phone);

            var btnSave = new Button
            {
                Text = "Save Customer",
                Location = new Point(50, 225),
                Size = new Size(300, 38),
                BackColor = Color.FromArgb(30, 80, 162),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Times New Roman", 10, FontStyle.Bold)
            };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Name is required");
                    return;
                }

                //Because DataStore holds a refrence to this same object, if a change is made here, it is made everywhere
                _customer.Name = txtName.Text.Trim();
                _customer.Email = txtEmail.Text.Trim();
                _customer.Phone = txtPhone.Text.Trim();

                FileManager.SaveAll();
                MessageBox.Show("Customer updated successfully!");
                this.Close();
            };
            this.Controls.Add(btnSave);
        }

        private Label AddLabel(string text, int y)
        {
            var lbl = new Label { Text = text, Location = new Point(50, y), Size = new Size(200, 20) };
            this.Controls.Add(lbl); return lbl;
        }

        private TextBox AddTextBox(int y, string defaultValue = "")
        {
            var txt = new TextBox
            {
                Location = new Point(50, y),
                Size = new Size(300, 28),
                Font = new Font("Times New Roman", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Text = defaultValue   // Pre-fill with the customer's current value
            };
            this.Controls.Add(txt); return txt;
        }
    }

    public class ViewCustomerForm : Form
    {
        public ViewCustomerForm(Customer customer)
        {
            this.Text = "View Customer";
            this.Size = new Size(450, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            //Displays the customer information as read only labels
            int y = 20;
            AddRow("Name:", customer.Name, ref y);
            AddRow("Email:", customer.Email, ref y);
            AddRow("Phone:", customer.Phone, ref y);

            //The purchase history section 
            var lblHistory = new Label
            {
                Text = "Purchase History:",
                Location = new Point(30, y + 20),
                Size = new Size(380, 25),
                Font = new Font("Times New Roman", 10, FontStyle.Bold)
            };
            this.Controls.Add(lblHistory);
            y += 50;

            var store = DataStore.Instance;

            //The following code shows each tranasaction in the customer's history
            foreach (var txnId in customer.PurchaseHistory)
            {
                //Look up the full transaction history from their ID
                var txn = store.Transactions.FirstOrDefault(t => t.TransactionId == txnId);
                //Checks if a transaction exists. If yes, it will form a nicely formatted display string with the ID, transaction and date. If no, it just shows the ID. 
                string display = txn != null
                    ? $"{txn.TransactionId} — ${txn.Total:F2} on {txn.Date:dd/MM/yyyy}"
                    : txnId;

                var lbl = new Label
                {
                    Text = $"  • {display}",
                    Location = new Point(30, y),
                    Size = new Size(380, 22),
                    Font = new Font("Times New Roman", 9),
                    ForeColor = Color.FromArgb(60, 60, 60)
                };
                this.Controls.Add(lbl);
                y += 22;
            }

            if (customer.PurchaseHistory.Count == 0)
            {
                var lbl = new Label { Text = "  No purchases yet.", Location = new Point(30, y), Size = new Size(380, 22), ForeColor = Color.Gray };
                this.Controls.Add(lbl);
            }
        }

        private void AddRow(string label, string value, ref int y)
        {
            var lbl = new Label { Text = label, Location = new Point(30, y), Size = new Size(100, 25), Font = new Font("Times New Roman", 9, FontStyle.Bold) };
            var val = new Label { Text = value, Location = new Point(140, y), Size = new Size(280, 25), Font = new Font("Times New Roman", 9) };
            this.Controls.AddRange(new Control[] { lbl, val });
            y += 30;
        }
    }
}
