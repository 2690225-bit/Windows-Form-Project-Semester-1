using System;
using System.Windows.Forms;
using POSSystem.Services;

namespace POSSystem.Forms
{
    // Signupform inherits from Form, giving it all standard window behaviour automatically
    public partial class Signupform : Form
    {
        public Signupform()
        {
            InitializeComponent();
        }

        private void Signupform_Load(object sender, EventArgs e)
        {

        }

        private void DashboardAndSignup_Load(object sender, EventArgs e)
        {

        }

        private void lblTitle(object sender, EventArgs e)
        {

        }

        private void lblFullName(object sender, EventArgs e)
        {

        }

        // Fires every time a key is pressed in the full name box
        private void txtFullName(object sender, EventArgs e)
        {

        }

        private void lblEmail(object sender, EventArgs e)
        {

        }

        // Fires every time a key is pressed in the email box
        private void txtEmail(object sender, EventArgs e)
        {

        }

        private void lblPassword(object sender, EventArgs e)
        {

        }

        // Fires every time a key is pressed in the password box
        private void txtPassword(object sender, EventArgs e)
        {

        }

        private void lblError(object sender, EventArgs e)
        {

        }

        private void btnCreate(object sender, EventArgs e)
        {
            // Clear any previous error message
            label5.Text = "";

            // Validate that all fields are filled before attempting to register
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                label5.Text = "All fields are required.";
                return; // Stop here — don't attempt to register
            }

            // AuthService checks if the email is already in use and creates the account
            bool success = Authenticate_Service.Register(
                textBox1.Text.Trim(),
                textBox2.Text.Trim(),
                textBox3.Text
            );

            if (success)
            {
                MessageBox.Show("Account created! You can now log in.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close the sign up window and return to login
                this.Close();
            }
            else
            {
                // Registration failed — email is already linked to an existing account
                label5.Text = "Email already in use. Please try a different one.";
            }
        }

        private void btnBack(object sender, EventArgs e)
        {
            // Close the sign up window and return to login
            this.Close();
        }
    }
}