using System;

namespace POSSystem.Models
{
    public class Product
    {
        //Generate a unique code for each product
        public string SKU { get; set; } = string.Empty;

        //Human-readable product name
        public string Name { get; set; } = string.Empty;

        //Catergory for filtering
        public string Category {  get; set; } = string.Empty;

        //The selling price
        public decimal Price {  get; set; }

        //Quantity of units currently in stock
        public int Stock {  get; set; }

        //Last time stock was updated. This is stored in the Datatime object
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        //Check if the stock in under 10
        public bool IsLowStock => Stock < 10;

        // Display in ListBoxes as "SKU | Name | $Price | Stock units"
        public override string ToString() => $"{SKU} | {Name} | ${Price:F2} | {Stock} units";
    }
}
