using System.Collections.Generic;
using System.Linq;

namespace POSSystem.Models
{
    public class OrderItem
    {
        // product being ordered
        public Product Product { get; set; } = null!;

        // quantity of products in cart
        public int Quantity { get; set; }

        // the price locked in at time of adding to cart. this is important because if a price changes instore after adding to cart, the cart price stays the same
        public decimal UnitPrice { get; set; }

        // finding the total of an item by multiplying the quantity by the price
        public decimal LineTotal => Quantity * UnitPrice;

        // display as "productname × qty — $linetotal"
        public override string ToString() =>
            $"{Product.Name} × {Quantity} — ${LineTotal:f2}";
    }

    public class Order
    {
        //this is the code for all the items in the cart
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        //sum of all totals before tax
        public decimal Subtotal => Items.Sum(i => i.LineTotal);

        // tax is calculated as a percentage of the subtotal
        public decimal Tax => Subtotal * (Services.DataStore.Instance.Settings.TaxRate / 100m);

        // final total the customer pays
        public decimal Total => Subtotal + Tax;

        // total number of individual items (e.g. 3 × cola + 2 × chips = 5 items)
        public int totalitemcount => Items.Sum(i => i.Quantity);

        //Add a product to the cart
        public void AddItem(Product product, int quantity = 1)
        {
            /*The .FirstOrDefault method returns the first element which satisfies the condition. i represents each element in the Item collection and the
              it is basically saying for each i, check i.product.sku = product.sku. This is then stored in var existing*/
            var existing = Items.FirstOrDefault(i => i.Product.SKU == product.SKU);

            if (existing != null)
            {
                // Product already in cart — just increase the quantity
                existing.Quantity += quantity;
            }
            else
            {
                // Add a new OrderItem to the list
                // We capture UnitPrice NOW so it's locked in even if product price changes later
                Items.Add(new OrderItem
                {
                    Product = product,
                    Quantity = quantity,
                    UnitPrice = product.Price   // Lock the price at time of adding
                });
            }
        }

        public void RemoveItem(string sku)
        {
            //Removes every item which matches the condition
            Items.RemoveAll(i => i.Product.SKU == sku);
        }

        //If the user wants to clear their cart completly
        public void Clear() => Items.Clear();
    }
}
