using Microsoft.AspNetCore.Identity;

namespace Authorization.FacebookDemo.Entities
{
    /// <summary>
    /// //Screech: Update Summary (2023-12-16 4:43 ApplicationUser)
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
        
        }

        public ApplicationUser(string username) : base(username) 
        {

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    
}
