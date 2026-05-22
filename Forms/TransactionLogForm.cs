using System;
using System.Linq;
using System.Windows.Forms;
using POSSystem.Models;
using POSSystem.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POSSystem.Forms
{
    public partial class TransactionLogForm : Form
    {
        public TransactionLogForm()
        {
            InitializeComponent();

            // Populate the sort dropdown
            comboBox1.Items.AddRange(new[] { "Date", "Customer", "Transaction ID", "Amount", "Payment Method", "Status" });
            comboBox1.SelectedIndex = 0;

            RefreshList();
        }

        private void TransactionLogForm_Load(object sender, EventArgs e)
        {

        }

        // Fires every time a key is pressed in the search box
        private void txtSearch(object sender, EventArgs e)
        {
            RefreshList();
        }

        // Fires when the sort dropdown selection changes
        private void cmbSortBy(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void lvTransactions(object sender, EventArgs e)
        {

        }

        private void btnExportCsv(object sender, EventArgs e)
        {
            // Open a save dialog so the user picks where to save the file
            using var dlg = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"transactions_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileManager.ExportTransactionsCsv(dlg.FileName);
                MessageBox.Show("Transactions exported successfully!", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportPdf(object sender, EventArgs e)
        {
            // PDF export would require a library like iTextSharp in production
            MessageBox.Show("PDF export requires iTextSharp configuration.", "Not Implemented",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportExcel(object sender, EventArgs e)
        {
            // Excel export would require a library like EPPlus in production
            MessageBox.Show("Excel export requires EPPlus configuration.", "Not Implemented",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshList()
        {
            listView1.Items.Clear();

            string search = textBox1.Text.ToLower();
            var store = DataStore.Instance;

            // Filter by transaction ID or customer name
            var transactions = store.Transactions
                .Where(t =>
                    string.IsNullOrEmpty(search) ||
                    t.TransactionId.ToLower().Contains(search) ||
                    t.CustomerName.ToLower().Contains(search)
                );

            // Sort based on the dropdown selection
            transactions = comboBox1.SelectedItem?.ToString() switch
            {
                "Date" => transactions.OrderByDescending(t => t.Date),
                "Customer" => transactions.OrderBy(t => t.CustomerName),
                "Transaction ID" => transactions.OrderBy(t => t.TransactionId),
                "Amount" => transactions.OrderByDescending(t => t.Total),
                "Payment Method" => transactions.OrderBy(t => t.PaymentMethod),
                "Status" => transactions.OrderBy(t => t.Status),
                _ => transactions.OrderByDescending(t => t.Date)
            };

            foreach (var t in transactions)
            {
                var item = new ListViewItem(t.Date.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(t.CustomerName);
                item.SubItems.Add(t.TransactionId);
                item.SubItems.Add($"${t.Total:F2}");
                item.SubItems.Add(t.PaymentMethod);
                item.SubItems.Add(t.Status);

                // Highlight pending (offline) transactions in yellow
                if (t.IsPendingSync)
                    item.BackColor = System.Drawing.Color.FromArgb(255, 240, 180);

                // Tag stores the Transaction so we can open it on double-click
                item.Tag = t;
                listView1.Items.Add(item);
            }

            // Show total count in the form title
            this.Text = $"Transaction Log ({listView1.Items.Count} records)";
        }
    }
}