using ASP.NETCoreD10.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreD10.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>  
    {
        /*------------------------------------------------------------------*/
        public AppDbContext() : base() { }
        /*------------------------------------------------------------------*/
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        /*------------------------------------------------------------------*/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Keep Behavior of IdentityDbContext
            base.OnModelCreating(builder);
        }
        /*------------------------------------------------------------------*/
    }
}
