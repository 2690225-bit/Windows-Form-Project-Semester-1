using System;
using System.IO;                        
using System.Collections.Generic;
using POSSystem.Models;
//using Newtonsoft.Json;
using System.Text.Json;

namespace POSSystem.Services
{
    public static class FileManager
    {
        //This variable will store the main folder where the app saves its data
        public static readonly string AppFolder = Path.Combine(
            //The following line creates the correct place to store app data
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            //This adds a name to the apps folder
            "POSSystem"
            );

        //Every type of data will be separatly stored in its own file
        //readonly means you can only assign the field during the declaration or in a constructor in the same class.
        private static readonly string UsersFile = Path.Combine(AppFolder, "users.json");
        private static readonly string CustomersFile = Path.Combine(AppFolder, "customers.json");
        private static readonly string ProductsFile = Path.Combine(AppFolder, "products.json");
        private static readonly string TransactionsFile = Path.Combine(AppFolder, "transactions.json");
        private static readonly string SettingsFile = Path.Combine(AppFolder, "settings.json");

        //Makes the files readable. The Write.Indented makes each of the information onto different lines. 
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        //Saves all data so that it dosen't get lost during get lost during a crash
        public static void SaveAll()
        {
            //Ensures that a folder exists. If there is a folder, it does nothing but if there is no folder, it creates a new folder
            Directory.CreateDirectory(AppFolder);
            var store = DataStore.Instance;

            //Example for the first one is that it takes all users in memory → convert them into JSON → save them into a file on disk
            File.WriteAllText(UsersFile, System.Text.Json.JsonSerializer.Serialize(store.Users, Options));
            File.WriteAllText(CustomersFile, System.Text.Json.JsonSerializer.Serialize(store.Customers, Options));
            File.WriteAllText(ProductsFile, System.Text.Json.JsonSerializer.Serialize(store.Products, Options));
            File.WriteAllText(TransactionsFile, System.Text.Json.JsonSerializer.Serialize(store.Transactions, Options));
            File.WriteAllText(SettingsFile, System.Text.Json.JsonSerializer.Serialize(store.Settings, Options));
        }

        //The LoadAll() function is responsible for loading all saved data from files back into the application when it starts.
        public static void LoadAll()
        {
            //This is the centre where all the data is
            var store = DataStore.Instance;

            //Take the first one for example, it reads the user.json file and convert json (List<user>) and stores it in memory
            store.Users = LoadList<Users>(UsersFile);
            store.Customers = LoadList<Customer>(CustomersFile);
            store.Products = LoadList<Product>(ProductsFile);
            store.Transactions = LoadList<Transaction>(TransactionsFile);
            store.Settings = LoadObject<AppSettings>(SettingsFile) ?? new AppSettings(); 

            // If no users exist (first time running), create a default Admin account

            if (store.Users.Count == 0)
            {
                store.Users.Add(new Users
                {
                    Id = "USR001",
                    FullName = "Anjun Feng",
                    Email = "AnjunFeng@gmail.com",
                    Password = "Password123",
                    Role = "Admin",
                    Phone = 0485385192,
                    DateOfBirth = "03/07/2001"
                });
                SaveAll();
            }

            // If no products exist, add some sample products
            if (store.Products.Count == 0)
            {
                store.Products.AddRange(new[]
                {
                    new Product { SKU="SKU-001", Name="Coca Cola 375ml",  Category="Beverages", Price=2.50m, Stock=50, LastUpdated=DateTime.Now },
                    new Product { SKU="SKU-002", Name="Chips BBQ 150g",   Category="Snacks",    Price=3.20m, Stock=30, LastUpdated=DateTime.Now },
                    new Product { SKU="SKU-003", Name="Water 600ml",      Category="Beverages", Price=1.50m, Stock=8,  LastUpdated=DateTime.Now },  // Low stock!
                    new Product { SKU="SKU-004", Name="Chocolate Bar",    Category="Confectionery", Price=2.00m, Stock=5, LastUpdated=DateTime.Now },
                });
                SaveAll();
            }
        }

 
        private static List<T> LoadList<T>(string filePath)
        {
            //The try catch basically allows the use of risky code. The risky code is written in the try part and if it causes an error, it will go the the catch part
            try
            {
                // Checks if the file exists
                if (!File.Exists(filePath))
                    // Return empty list if there is no file
                    return new List<T>();   

                // Read the entire file contents as a string
                string json = File.ReadAllText(filePath);

                // The '??' means if deserialization (convert JSON string → List<T>) returns null, use an empty list instead
                return System.Text.Json.JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch
            {
                // If the file is corrupted or unreadable, return an empty list rather than crash
                return new List<T>();
            }
        }

        //The line defines a static method named `LoadObject` that attempts to load an object of type `T` (where `T` is a class) from a file specified by `filePath`, and returns the object as a nullable reference type (`T?`).
        private static T? LoadObject<T>(string filePath) where T : class
        {
            try
            {
                //checks if a file exists and returns null if it dosent.
                if (!File.Exists(filePath)) return null;
                //Reads the JSON fule as a string
                string json = File.ReadAllText(filePath);
                //Converts the JSON String into an object with type c. 
                return System.Text.Json.JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                //If there are any errors, it will return null
                return null;
            }
        }

        public static void ExportTransactionsCsv(string outputPath)
        {
            var store = DataStore.Instance;

            // Build the CSV content using a writer
            using var writer = new StreamWriter(outputPath);

            // Write the header row
            writer.WriteLine("TransactionId,Date,Customer,Subtotal,Tax,Total,PaymentMethod,Status");

            // Write one row per transaction
            foreach (var t in store.Transactions)
            {
                // Escape commas in fields by wrapping in quotes
                writer.WriteLine(
                    $"\"{t.TransactionId}\",\"{t.Date:dd/MM/yyyy HH:mm}\"," +
                    $"\"{t.CustomerName}\",{t.Subtotal},{t.Tax},{t.Total}," +
                    $"\"{t.PaymentMethod}\",\"{t.Status}\""
                );
            }
        }

        // Exports the product list to CSV (for stock-taking or importing into another system)
        public static void ExportInventoryCsv(string outputPath)
        {
            //Ensures there is only one instance of Datastore
            var store = DataStore.Instance;
            //The StreamWriter is used to write text into the fule outputPath. The using element is used to ensure that the file is always closed even if an error occurs. 
            using var writer = new StreamWriter(outputPath);
            
            //used to write the header row for the CSV file
            writer.WriteLine("SKU,Name,Category,Price,Stock,LastUpdated");
            //P represents each individual product in the collection. The foreach loop iterates over each Product in the store.Products collection.
            foreach (var p in store.Products)
            {
                //Inside the loop, the method writes a new line to the CSV file for each product, each having unique formatting
                writer.WriteLine(
                    $"\"{p.SKU}\",\"{p.Name}\",\"{p.Category}\"," +
                    $"{p.Price},{p.Stock},\"{p.LastUpdated:dd/MM/yyyy}\""
                );
            }
        }

        // Reads a CSV file and adds products to inventory.
        public static (int imported, int skipped) ImportProductsCsv(string inputPath)
        {
            int imported = 0, skipped = 0;
            //Makes sure that there is only one instance of DataStore.
            var store = DataStore.Instance;

            // ReadAllLines reads every line of the file into a string array
            string[] lines = File.ReadAllLines(inputPath);

            // Skip line 0 (the header row) — start from line 1
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    // Split each line by commas to get individual fields
                    string[] fields = lines[i].Split(',');

                    // We need exactly 4 fields
                    if (fields.Length < 4) { skipped++; continue; }

                    // Parse each field into the correct type. Trim() removes leading/trailing whitespace or quotes
                    var product = new Product
                    {
                        SKU = store.GenerateSKU(),
                        Name = fields[0].Trim().Trim('"'),
                        Category = fields[1].Trim().Trim('"'),
                        // Convert string to decimal
                        Price = decimal.Parse(fields[2].Trim()),
                        // Convert string to int
                        Stock = int.Parse(fields[3].Trim()),       
                        LastUpdated = DateTime.Now
                    };

                    store.Products.Add(product);
                    imported++;
                }
                catch
                {
                    // If parsing fails on a row, skip it and count it as skipped
                    skipped++;
                }
            }

            // Save after all imports
            if (imported > 0) SaveAll();

            // Return a tuple with both counts
            return (imported, skipped);   
        }
    }
}
