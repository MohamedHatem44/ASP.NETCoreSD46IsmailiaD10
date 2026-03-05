using Microsoft.AspNetCore.Identity;

namespace ASP.NETCoreD10.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
