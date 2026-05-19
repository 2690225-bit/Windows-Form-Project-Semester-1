  namespace POSSystem.Models
{
    public class Users
    {
        //A unique number Id for each user
        public string Id { get; set; } = string.Empty;

        //User Full Name
        public string FullName { get; set; } = string.Empty;

        //User Email
        public string Email {  get; set; } = string.Empty;

                                                                        
        //User Password
        public string Password {  get; set; } = string.Empty;

        // The user's job role — either "Admin", "Manager", or "Cashier"
        // This controls what they can see and do in the app (permissions)
        public string Role { get; set; } = "Cashier";

        //User Date Of Birth
        public string DateOfBirth { get; set; } = string.Empty;

        //User Phone Number
        public int? Phone { get; set; } = null;

        public string FirstName
        {
            get
            { 
                //Splitting the full name into first name and last name. For example Bob Smith will be turned into 'Bob' + 'Smith'
                var parts = FullName .Split('.');
                if (parts.Length > 0)
                {
                    return parts[0];
                }
                else
                {
                    return FullName;
                }
            }
        }

        //The below function will check the perimissons of each role
        public bool HasPermission(string permission)
        {
            return permission switch
            {
                // All roles can access ProcessSales
                "ProcessSales" => Role == "Admin" || Role == "Manager" || Role == "Cashier",

                // Only Admin and Manager can access ManageInventory
                "ManageInventory" => Role == "Admin" || Role == "Manager",

                // Only Admin and Manager can access ViewReports
                "ViewReports" => Role == "Admin" || Role == "Manager",

                // Only admin can access ManageStaff
                "ManageStaff" => Role == "Admin",

                // Only Admin can change EditSettings
                "EditSettings" => Role == "Admin",

                // If an unknown permission is checked, deny it without any hesitation
                _ => false
            };
        }
        //Overriding one of the methods in the objects class to show the full name and their role
        public override string ToString() => $"{FullName} ({Role})";
    }
}
