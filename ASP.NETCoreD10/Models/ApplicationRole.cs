using Microsoft.AspNetCore.Identity;

namespace ASP.NETCoreD10.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string? Desciption { get; set; }
    }
}
