using Microsoft.AspNetCore.Identity;

namespace AuthorizationDatabase.Entities
{
    /// <summary>
    /// //Screech: Update Summary (2023-12-16 4:43 ApplicationUser)
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
