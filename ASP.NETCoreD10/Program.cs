using ASP.NETCoreD10.Context;
using ASP.NETCoreD10.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreD10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SDMVCDay10"));
            });

            // Built In - But not Registered 
            // Register => All Dependencies
            builder.Services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    //options.Password.req
                })
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                //options.Lockout.AllowedForNewUsers = true;  

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                //options.User.AllowedUserNameCharacters = "@";

                options.User.RequireUniqueEmail = true;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            var app = builder.Build();

            // First Roles And Admin User
            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //    string[] roles = { "Admin", "User" };

            //    foreach (var role in roles)
            //    {
            //        if (!await roleManager.RoleExistsAsync(role))
            //            await roleManager.CreateAsync(new IdentityRole(role));
            //    }

            //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //    string adminEmail = "admin@gmail.com";
            //    string password = "Admin@123";

            //    if (await userManager.FindByEmailAsync(adminEmail) == null)
            //    {
            //        var admin = new ApplicationUser
            //        {
            //            UserName = adminEmail,
            //            Email = adminEmail
            //        };

            //        await userManager.CreateAsync(admin, password);
            //        await userManager.AddToRoleAsync(admin, "Admin");
            //    }
            //}

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
