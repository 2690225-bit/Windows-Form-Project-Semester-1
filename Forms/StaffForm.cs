using System;
using System.Windows.Forms;
using POSSystem.Models;
using POSSystem.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POSSystem.Forms
{
    public partial class StaffForm : Form
    {
        public StaffForm()
        {
            InitializeComponent();

            // Block access if the current user doesn't have staff management permission
            var currentUser = DataStore.Instance.CurrentUser;
            if (currentUser == null || !currentUser.HasPermission("ManageStaff"))
            {
                MessageBox.Show("You don't have permission to access this area.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Close immediately after the form finishes loading
                this.Load += (s, e) => this.Close();
                return;
            }

            RefreshList();
        }

        private void StaffForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddStaff(object sender, EventArgs e)
        {
            new AddStaffForm().ShowDialog(this);
            RefreshList();
        }

        private void lvStaff(object sender, EventArgs e)
        {

        }

        // Reloads all staff from the DataStore into the ListView
        private void RefreshList()
        {
            listView1.Items.Clear();

            foreach (var user in DataStore.Instance.Users)
            {
                var item = new ListViewItem(user.FullName);
                item.SubItems.Add(user.Email);
                item.SubItems.Add(user.Role);
                item.SubItems.Add(user.Id);

                // Tag stores the Users object so it can be retrieved on row selection
                item.Tag = user;
                listView1.Items.Add(item);
            }
        }
    }

    public class AddStaffForm : Form
        
    {
        private System.Windows.Forms.TextBox txtName, txtEmail, txtPhone, txtDob;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnSave;

        public AddStaffForm()
        {
            this.Text = "Add Staff";
            this.Size = new System.Drawing.Size(400, 380);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;

            BuildForm();
        }

        private void BuildForm()
        {
            AddLabel("Full Name", 20); txtName = AddTextBox(42);
            AddLabel("Email", 82); txtEmail = AddTextBox(104);
            AddLabel("Phone", 144); txtPhone = AddTextBox(166);
            AddLabel("Date of Birth (DD/MM/YYYY)", 206); txtDob = AddTextBox(228);

            var lblRole = new System.Windows.Forms.Label
            {
                Text = "Job Role:",
                Location = new System.Drawing.Point(50, 268),
                Size = new System.Drawing.Size(300, 20)
            };
            this.Controls.Add(lblRole);

            cmbRole = new System.Windows.Forms.ComboBox
            {
                Location = new System.Drawing.Point(50, 290),
                Size = new System.Drawing.Size(300, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRole.Items.AddRange(new[] { "Cashier", "Manager", "Admin" });
            cmbRole.SelectedIndex = 0;
            this.Controls.Add(cmbRole);

            btnSave = new System.Windows.Forms.Button
            {
                Text = "Save",
                Location = new System.Drawing.Point(50, 335),
                Size = new System.Drawing.Size(300, 40),
                BackColor = System.Drawing.Color.FromArgb(30, 80, 162),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Name and Email required.");
                return;
            }

            var store = DataStore.Instance;

            // New staff are given a default password to change on first login
            var user = new Users
            {
                Id = store.GenerateUserId(),
                FullName = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = int.TryParse(txtPhone.Text.Trim(), out int val) ? val : null,
                DateOfBirth = txtDob.Text.Trim(),
                Password = "changeme123",
                Role = cmbRole.SelectedItem?.ToString() ?? "Cashier"
            };

            store.Users.Add(user);
            FileManager.SaveAll();

            MessageBox.Show($"Staff member '{user.FullName}' added. Default password: changeme123");
            this.Close();
        }

        private void AddLabel(string text, int y)
        {
            var lbl = new System.Windows.Forms.Label
            {
                Text = text,
                Location = new System.Drawing.Point(50, y),
                Size = new System.Drawing.Size(300, 20),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            this.Controls.Add(lbl);
        }

        private System.Windows.Forms.TextBox AddTextBox(int y)
        {
            var txt = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(50, y),
                Size = new System.Drawing.Size(300, 28),
                Font = new System.Drawing.Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txt);
            return txt;
        }
    }
}