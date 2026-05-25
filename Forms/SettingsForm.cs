using System;
using System.Windows.Forms;
using POSSystem.Models;
using POSSystem.Services;


namespace POSSystem.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            // Pre-fill all fields with the current settings
            var settings = DataStore.Instance.Settings;
            textBox1.Text = settings.CompanyName;
            textBox2.Text = settings.StoreAddress;
            textBox3.Text = settings.ContactEmail;
            textBox4.Text = settings.Currency;
            textBox5.Text = settings.TaxRate.ToString();

            // Set the correct theme radio button
            radioButton1.Checked = settings.Theme == "Light";
            radioButton2.Checked = settings.Theme == "Dark";

            checkBox1.Checked = settings.LowStockAlertsEnabled;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void rbLight(object sender, EventArgs e)
        {

        }

        private void rbDark(object sender, EventArgs e)
        {

        }

        private void chkLowStock(object sender, EventArgs e)
        {
        }

        private void btnSave(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textBox5.Text, out decimal taxRate))
            {
                MessageBox.Show("Invalid tax rate.");
                return;
            }

            // Apply all fields back to the Settings object in DataStore
            var s = DataStore.Instance.Settings;
            s.CompanyName = textBox1.Text.Trim();
            s.StoreAddress = textBox2.Text.Trim();
            s.ContactEmail = textBox3.Text.Trim();
            s.Currency = textBox4.Text.Trim();
            s.TaxRate = taxRate;
            s.Theme = radioButton2.Checked ? "Dark" : "Light";
            s.LowStockAlertsEnabled = checkBox1.Checked;

            FileManager.SaveAll();

            MessageBox.Show("Settings saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReset(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset all settings to default?", "Confirm",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataStore.Instance.Settings = new AppSettings();
                FileManager.SaveAll();
                MessageBox.Show("Settings reset.");
                this.Close();
            }
        }
    }
}