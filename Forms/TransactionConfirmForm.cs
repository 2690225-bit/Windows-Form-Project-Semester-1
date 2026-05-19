using System;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using POSSystem.Models;
using POSSystem.Services;

namespace POSSystem.Forms
{
    public partial class TransactionConfirmForm : Form
    {
        private readonly Transaction _transaction;

        public TransactionConfirmForm(Transaction transaction)
        {
            _transaction = transaction;
            InitializeComponent();

            // Fill in the transaction details
            label2.Text = $"Transaction ID: {transaction.TransactionId}";
            label3.Text = $"Total: ${transaction.Total:F2} ({transaction.PaymentMethod})";
        }

        private void TransactionConfirmForm_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint(object sender, EventArgs e)
        {
            // In production, this would send to an actual printer
            MessageBox.Show("Sending to printer...");
        }

        private void btnDownloadPdf(object sender, EventArgs e)
        {
            // Saves a text receipt — a full PDF would require a library like iTextSharp
            using var dlg = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt",
                FileName = $"receipt_{_transaction.TransactionId}.txt"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(dlg.FileName, GenerateReceiptText(_transaction));
                MessageBox.Show("Receipt saved!");
            }
        }

        private void btnEmailReceipt(object sender, EventArgs e)
        {
            // Requires SMTP configuration to implement fully
            MessageBox.Show("Email functionality requires SMTP configuration.");
        }

        private void btnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        // Builds a plain-text receipt string from a completed transaction
        private string GenerateReceiptText(Transaction t)
        {
            var sb = new StringBuilder();
            sb.AppendLine(DataStore.Instance.Settings.CompanyName);
            sb.AppendLine(new string('-', 40));
            sb.AppendLine($"Receipt: {t.TransactionId}");
            sb.AppendLine($"Date:    {t.Date:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Customer:{t.CustomerName}");
            sb.AppendLine(new string('-', 40));

            foreach (var item in t.Items)
                sb.AppendLine($"{item.Product.Name,-25} {item.Quantity,3} x ${item.UnitPrice:F2} = ${item.LineTotal:F2}");

            sb.AppendLine(new string('-', 40));
            sb.AppendLine($"{"Subtotal:",-30} ${t.Subtotal:F2}");
            sb.AppendLine($"{"Tax:",-30} ${t.Tax:F2}");
            sb.AppendLine($"{"TOTAL:",-30} ${t.Total:F2}");
            sb.AppendLine($"{"Payment:",-30} {t.PaymentMethod}");
            sb.AppendLine(new string('=', 40));
            sb.AppendLine("Thank you for your purchase!");

            return sb.ToString();
        }
    }
}