namespace POSSystem.Models
{
    public class AppSettings
    {
        // This shows the business name in the header
        public string CompanyName { get; set; } = "My Company";

        // The physical address of the store
        public string StoreAddress { get; set; } = string.Empty;

        // Business contact email
        public string ContactEmail { get; set; } = string.Empty;

        // The currency code and default currency
        public string Currency { get; set; } = "AUD";

        // Tax rate as a percentage
        //The m makes it a decimal, from 10 to 10.0
        public decimal TaxRate { get; set; } = 10m;

        // Default payment method and pre-selected option at checkout
        public string DefaultPaymentMethod { get; set; } = "Card";

        // Whether to show low-stock alerts on the dashboard
        public bool LowStockAlertsEnabled { get; set; } = true;

        // UI theme and default theme
        public string Theme { get; set; } = "Light";
    }
}