using System;
using System.Windows.Forms;
using POSSystem.Models;
using POSSystem.Services;

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

            // Double-click a row to view that staff member
            listView1.DoubleClick += (s, e) =>
            {
                var user = GetSelectedUser();
                if (user != null)
                    new ViewStaffForm(user).ShowDialog(this);
            };

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

        private void btnEditStaff(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null) { MessageBox.Show("Please select a staff member."); return; }
            new EditStaffForm(user).ShowDialog(this);
            RefreshList();
        }

        private void btnViewStaff(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null) { MessageBox.Show("Please select a staff member."); return; }
            new ViewStaffForm(user).ShowDialog(this);
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

        private Users GetSelectedUser()
        {
            if (listView1.SelectedItems.Count == 0) return null;
            return listView1.SelectedItems[0].Tag as Users;
        }
    }

    public class AddStaffForm : Form
    {
        private System.Windows.Forms.TextBox txtName, txtEmail, intPhone, txtDob;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnSave;

        public AddStaffForm()
        {
            this.Text = "Add Staff";
            this.Size = new System.Drawing.Size(400, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;

            BuildForm();
        }

        private void BuildForm()
        {
            AddLabel("Full Name", 20); txtName = AddTextBox(42);
            AddLabel("Email", 82); txtEmail = AddTextBox(104);
            AddLabel("Phone", 144); intPhone = AddTextBox(166);
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
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
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
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
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

            if (!int.TryParse(intPhone.Text.Trim(), out int phone))
            {
                MessageBox.Show("Phone must be a number.");
                return;
            }

            var store = DataStore.Instance;

            // New staff are given a default password to change on first login
            var user = new Users
            {
                Id = store.GenerateUserId(),
                FullName = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = phone,
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
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };
            this.Controls.Add(txt);
            return txt;
        }
    }

    public class EditStaffForm : Form
    {
        private readonly Users _user;
        private System.Windows.Forms.TextBox txtName, txtEmail, intPhone, txtDob;
        private System.Windows.Forms.ComboBox cmbRole;

        public EditStaffForm(Users user)
        {
            _user = user;
            this.Text = "Edit Staff";
            this.Size = new System.Drawing.Size(400, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;

            // Pre-fill all fields with the existing staff member's data
            AddLabel("Full Name", 20); txtName = AddTextBox(42, _user.FullName);
            AddLabel("Email", 82); txtEmail = AddTextBox(104, _user.Email);
            AddLabel("Phone", 144); intPhone = AddTextBox(166, _user.Phone.ToString());
            AddLabel("Date of Birth (DD/MM/YYYY)", 206); txtDob = AddTextBox(228, _user.DateOfBirth);

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
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            };
            cmbRole.Items.AddRange(new[] { "Cashier", "Manager", "Admin" });
            cmbRole.SelectedItem = _user.Role;
            this.Controls.Add(cmbRole);

            var btnSave = new System.Windows.Forms.Button
            {
                Text = "Save Changes",
                Location = new System.Drawing.Point(50, 335),
                Size = new System.Drawing.Size(300, 40),
                BackColor = System.Drawing.Color.FromArgb(30, 80, 162),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
            };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Name and Email required.");
                    return;
                }

                if (!int.TryParse(intPhone.Text.Trim(), out int phone))
                {
                    MessageBox.Show("Phone must be a number.");
                    return;
                }

                // DataStore holds the same reference so updating here updates everywhere
                _user.FullName = txtName.Text.Trim();
                _user.Email = txtEmail.Text.Trim();
                _user.Phone = phone;
                _user.DateOfBirth = txtDob.Text.Trim();
                _user.Role = cmbRole.SelectedItem?.ToString() ?? "Cashier";

                FileManager.SaveAll();
                MessageBox.Show("Staff member updated successfully!");
                this.Close();
            };
            this.Controls.Add(btnSave);
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

        private System.Windows.Forms.TextBox AddTextBox(int y, string value = "")
        {
            var txt = new System.Windows.Forms.TextBox
            {
                Text = value,
                Location = new System.Drawing.Point(50, y),
                Size = new System.Drawing.Size(300, 28),
                Font = new System.Drawing.Font("Segoe UI", 10),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };
            this.Controls.Add(txt);
            return txt;
        }
    }

    public class ViewStaffForm : Form
    {
        public ViewStaffForm(Users user)
        {
            this.Text = "View Staff Member";
            this.Size = new System.Drawing.Size(420, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;

            // Display all staff details as read-only label pairs
            int y = 20;
            AddRow("Full Name:", user.FullName, ref y);
            AddRow("Email:", user.Email, ref y);
            AddRow("Date of Birth:", user.DateOfBirth, ref y);
            AddRow("Role:", user.Role, ref y);
            AddRow("ID:", user.Id, ref y);

            var btnClose = new System.Windows.Forms.Button
            {
                Text = "Close",
                Location = new System.Drawing.Point(140, y + 15),
                Size = new System.Drawing.Size(120, 35),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        // ref y advances the position after each row so labels don't overlap
        private void AddRow(string label, string value, ref int y)
        {
            var lbl = new System.Windows.Forms.Label
            {
                Text = label,
                Location = new System.Drawing.Point(30, y),
                Size = new System.Drawing.Size(120, 22),
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };
            var val = new System.Windows.Forms.Label
            {
                Text = value,
                Location = new System.Drawing.Point(160, y),
                Size = new System.Drawing.Size(230, 22),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            this.Controls.AddRange(new System.Windows.Forms.Control[] { lbl, val });
            y += 28;
        }
    }
}