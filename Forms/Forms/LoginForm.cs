    using System;
    using System.Windows.Forms;

    namespace POSSystem.Forms
    {
        // LoginForm inherits from Form, giving it all standard window behaviour automatically
        public partial class LoginForm : Form
        {
            public LoginForm()
            {
                InitializeComponent();

                // textBox1 (email) is disabled in the designer so we enable it here
                textBox1.Enabled = true;
            }

            private void LoginForm_Load(object sender, EventArgs e)
            {

            }

            private void lblTitle(object sender, EventArgs e)
            {

            }

            private void lblUsername(object sender, EventArgs e)
            {

            }

            // Fires every time a key is pressed in the email box
            private void txtEmail(object sender, EventArgs e)
            {

            }

            private void lblPassword(object sender, EventArgs e)
            {

            }

            private void txtPassword(object sender, EventArgs e)
            {

            }

            private void lblError(object sender, EventArgs e)
            {

            }

            private void btnLogin(object sender, EventArgs e)
            {
                // Clear any previous error message
                label4.Text = "";

                // Validate that neither field is empty before attempting login
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    label4.Text = "Please enter your email and password.";
                    return; // Stop here — don't attempt login
                }

                // AuthService checks the email and password against stored users
                var user = AuthServiceHelpers.Login(textBox1.Text.Trim(), textBox2.Text);

                if (user != null)
                {
                    // Login successful — open the next form
                    var dashboard = new Dashboard();
                    dashboard.Show();

                    // Hide rather than close so the app doesn't shut down
                    this.Hide();
                }
                else
                {
                    // Login failed — tell the user and clear the password for security
                    label4.Text = "Invalid email or password. Please try again.";
                    textBox2.Clear();
                    textBox2.Focus(); // Move the cursor back to the password field
                }
            }

            private void btnSignUp(object sender, EventArgs e)
            {
                // ShowDialog blocks this form until the sign up window is closed
                var signUpForm = new Signupform();
                signUpForm.ShowDialog(this);
            }

            // Overrides the base Form method so the app exits when the login window is closed
            protected override void OnFormClosed(FormClosedEventArgs e)
            {
                base.OnFormClosed(e); // Call the parent's version first
                Application.Exit();
            }
        }
    }