using System.Collections.Generic;
using POSSystem.Models;

namespace POSSystem.Services
{
    public class DataStore
    {
        //The static means that it will only create one database and keep refreshing rather than creating a new database everytime it is called
        private static DataStore _instance;

        public static DataStore Instance
        { 
            get
            {
                //Without the following code, the database will not work resulting in the user not being able to log in or do any other actions related to the database
                if (_instance == null)
                    _instance = new DataStore();
                return _instance;
            }
        }

        // Encapsulation, which is an OOP principle, is controlling how the object is created
        private DataStore() { }

        // All registered users and staff
        public List<Users> Users { get; set; } = new List<Users>();

        // The currently logged-in user
        public Users? CurrentUser { get => currentUser; set => currentUser = value; }

        // All customers in the system
        public List<Customer> Customers { get; set; } = new List<Customer>();

        // All products in the inventory
        public List<Product> Products { get; set; } = new List<Product>();

        // All completed transactions
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        // The current shopping cart
        public Order CurrentOrder { get; set; } = new Order();

        // Application settings
        public AppSettings Settings { get; set; } = new AppSettings();

        // Whether the app is currently connected to a network
        public bool IsOnline { get; set; } = true;

        // Counter used to generate unique IDs for transactions
        private int _transactionCounter = 1;
        private Users currentUser;

        //Creates a unique Id for each transaction
        public string GenerateTransactionId()
        {
            // Format: TXN-YYYYMMDD-counter (zero-padded to 3 digits)
            string id = $"TXN-{System.DateTime.Now:yyyyMMdd}-{_transactionCounter:D3}";
            _transactionCounter++;   // Increment so the next ID is different
            return id;
        }

        // Creates a unique ID for new customers based on how many people were before them in the list
        public string GenerateCustomerId() => $"CUST{(Customers.Count + 1):D3}";

        // Creates a unique ID for new users
        public string GenerateUserId() => $"USR{(Users.Count + 1):D3}";

        // Creates a unique SKU for new products
        public string GenerateSKU() => $"SKU-{(Products.Count + 1):D3}";
    }
}
