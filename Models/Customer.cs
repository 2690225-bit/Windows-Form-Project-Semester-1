using System.Collections.Generic;

namespace POSSystem.Models
{
    public class Customer
    {
        //Used to create a unique ID number (for example "CUST001")
        public string Id { get; set; } = string.Empty;

        //Used for Customer's full name
        public string Name { get; set; } = string.Empty;

        //Used for Customer's email address
        public string Email {  get; set; } = string.Empty;
        
        //Used for Customer's phone number
        public string Phone {  get; set; } = string.Empty;

        //To Do - Purchase History
        public List<string> PurchaseHistory { get; set; } = new List<string>();

        //This is used to overide ToString so that customers display nicely in ListBoxes and ComboBoxes
        public override string ToString() => $"{Name} ({Email})";
    }
}
