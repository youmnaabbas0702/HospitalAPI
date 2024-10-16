
using HospitalSystemAPI.Data;
using HospitalSystemAPI.Models;
using HospitalSystemAPI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystemAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<HospitalDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            // Add Identity services and configure to use ApplicationUser
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(/*options =>
            {
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5; 
                options.Lockout.AllowedForNewUsers = true;
            }*/)
                .AddEntityFrameworkStores<HospitalDbContext>()
                .AddDefaultTokenProviders();

            //// Configure cookie authentication for "Remember Me" functionality
            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true; // Make cookies HttpOnly
            //    options.ExpireTimeSpan = TimeSpan.FromDays(30); // Set cookie expiration (e.g., 30 days for "Remember Me")
            //    options.SlidingExpiration = true; // Extend session if the user is active
            //});

            builder.Services.AddScoped<IIdGenerator, IdGenerator>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 // Set the LoginPath to null to avoid redirection
                 options.LoginPath = null;

                 // Set the AccessDeniedPath to null if you want to avoid redirection for access denied as well
                 options.AccessDeniedPath = null;
             });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            //seeding admins
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await DataSeeder.SeedAdminsAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    // Log any errors encountered during seeding 
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during seeding the database.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
