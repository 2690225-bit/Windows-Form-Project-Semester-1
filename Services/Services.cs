using System.Linq;
using POSSystem.Models;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POSSystem.Services
{
    public static class Authenticate_Service
    {
        //Log In Section
        //Attempts to log in with username and password
        public static Users? Login(string email, string password)
        {
            var store = DataStore.Instance;

            //The FirstOrDefault syntax returns the first match and if none found, returns null
            //Searches for all users for one with matching email and password

            var user = store.Users.FirstOrDefault( 
                u => u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase) &&
                u.Password == password
                );

            if (user == null)
            {
                store.CurrentUser = user;
            }
            return null;
        }

        //Register Account Section
        //Creates a new user account.
        public static bool Register(string fullName, string email, string password)
        {
            var store = DataStore.Instance;

            //check if there is a duplicate email
            bool emailExists = store.Users.Any(
                u => u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase)
            );

            if (emailExists)
            {
                return false;
            }

            //Create a new user
            var newUser = new Users
            {
                Id = store.GenerateCustomerId(),
                FullName = fullName,
                Email = email,
                Password = password,
                //When a new user is created, they are defult to the lowest permission level
                Role = "Cashier" 
            };

            store.Users.Add(newUser);
            //Saves everything so nothing gets lost
            FileManager.SaveAll();
            return true;
        }

        //Logout Section
        public static void Logout ()
        {
            DataStore.Instance.CurrentUser = null;
        }
    }

    public static class InventoryService
    {
        //Add a product to the inventory
        public static void AddProduct(string name, string category, decimal price, int stock)
        {
            var store = DataStore.Instance;

            var product = new Product
            {
                SKU = store.GenerateSKU(),
                Name = name,
                Category = category,
                Price = price,
                Stock = stock,
                LastUpdated = DateTime.Now
            };

            store.Products.Add(product);
            FileManager.SaveAll();
        }

        //Edits an existing product information
        public static bool UpdateProduct(string sku, string name, string catergory, decimal price, int stock)
        {
            var store = DataStore.Instance;

            //Find the product by using its SKU
            var product = store.Products.FirstOrDefault(p => p.SKU == sku);
            if (product == null)
            {
                return false;
            }

            // Update the properties
            product.Name = name;
            product.Category = catergory;
            product.Price = price;
            product.Stock = stock;
            product.LastUpdated = DateTime.Now;

            FileManager.SaveAll();
            return true;
        }

        //Deleting a product from the inventory
        public static bool DeleteProduct(string sku)
        {
            var store = DataStore.Instance;
            int removed = store.Products.RemoveAll(p => p.SKU == sku);
            if (removed > 0) FileManager.SaveAll();
            return removed > 0;
        }

        //Changes the stock level both higher or lower
        public static bool AdjustStock(string sku, int newStock)
        {
            var store = DataStore.Instance;

            var product = store.Products.FirstOrDefault(p => p.SKU == sku);
            if (product == null)
            {  
                return false;
            }

            // Used to prevent the product stock from going into the negatives
            product.Stock = Math.Max(0, newStock);
            product.LastUpdated = DateTime.Now;
            FileManager.SaveAll();
            return true;
        }

        // Searches products by name, SKU, or category and returns matching results.
        // If the search term is empty, all products are returned.
        public static List<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return DataStore.Instance.Products;

            string term = searchTerm.ToLower();

            return DataStore.Instance.Products
                .Where(p =>
                    p.Name.ToLower().Contains(term) ||
                    p.SKU.ToLower().Contains(term) ||
                    p.Category.ToLower().Contains(term)
                )
                .ToList();
        }
    }

    public static class SalesService
    {
        // Called when the cashier clicks "Complete Transaction" at checkout.
        // Completes checkout by creating a transaction, updating stock, linking it to the customer, saving data, and clearing the cart.
        public static Transaction CompleteTransaction(string paymentMethod, string customerId = "")
        {
            var store = DataStore.Instance;
            var order = store.CurrentOrder;

            // Transaction record
            var transaction = new Transaction
            {
                TransactionId = store.GenerateTransactionId(),
                Date = DateTime.Now,
                CustomerId = customerId,
                CustomerName = GetCustomerName(customerId),
                // Copy the items
                Items = new System.Collections.Generic.List<OrderItem>(order.Items),
                Subtotal = order.Subtotal,
                Tax = order.Tax,
                Total = order.Total,
                PaymentMethod = paymentMethod,
                Status = store.IsOnline ? "Completed" : "Pending",
                // Mark as pending if we're offline
                IsPendingSync = !store.IsOnline
            };

            // Deduct stock for each item sold by finding the product in inventory by using SKU
            foreach (var item in order.Items)
            {
                //Reduce inventory stock based on quantity sold
                InventoryService.AdjustStock(item.Product.SKU, -item.Quantity);
            }

            // Link to customer's purchase history
            if (!string.IsNullOrEmpty(customerId))
            {
                //Find a customer whose ID matches
                var customer = store.Customers.FirstOrDefault(c => c.Id == customerId);
                if (customer != null)
                {
                    // Add this transaction's ID to the customer's purchase history list
                    customer.PurchaseHistory.Add(transaction.TransactionId);
                }
            }

            // Save the transaction
            store.Transactions.Add(transaction);
            FileManager.SaveAll();

            // Clear the cart for the next sale
            store.CurrentOrder.Clear();

            return transaction;
        }

        private static string GetCustomerName(string customerId)
        {
            // If no customer ID exists, use ‘Walk-in’ customer
            if (string.IsNullOrEmpty(customerId))
            {
                return "Walk-in";
            }

            // Find the matching customer
            var customer = DataStore.Instance.Customers
                .FirstOrDefault(c => c.Id == customerId);

            if (customer != null)
                return customer.Name;
            else
                return "Walk-in";
        }

        // Returns total sales revenue across all completed transactions
        public static decimal GetSalesTotal()
        {
            return DataStore.Instance.Transactions
                // Only include Completed sales and add all totals together
                .Where(t => t.Status == "Completed")
                .Sum(t => t.Total);
        }

        // Total number of completed transactions
        public static int GetTransactionCount()
        {
            //count how many completed transactions exist
            return DataStore.Instance.Transactions
                // Count transactions where status is Completed
                .Count(t => t.Status == "Completed");
        }
    }
}
