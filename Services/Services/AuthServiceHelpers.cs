using POSSystem.Models;
using POSSystem.Services;
using System.Linq;

// Handles user login and sign-up logic.
internal static class AuthServiceHelpers
{
    // Attempts to log in with an email and password.
    public static Users? Login(string email, string password)
    {
        var store = DataStore.Instance;

        // Search through all users for one with matching email AND password
        // StringComparison.OrdinalIgnoreCase makes the email comparison case-insensitive
        //=> is a lambda operator which creates short-inline functions without giving it a name
        var user = store.Users.FirstOrDefault(u =>
            u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase) &&
            u.Password == password
        );

        if (user != null)
        {
            // Store the logged-in user in the DataStore so all forms can access it
            store.CurrentUser = user;
        }
        return user;
    }
}