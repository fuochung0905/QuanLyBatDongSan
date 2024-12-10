using Application.Common.Interfaces;
using Domain.Entities.Features.Users;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCQRS.Service;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using System.Reflection;
namespace SimpleCQRS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped<IUser, CurrentUser>();
          
            services.AddHttpContextAccessor();

            services.AddRazorPages();

            services.AddCors();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddEndpointsApiExplorer();


            services.AddRouting(options => options.LowercaseUrls = true);
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            services.AddHttpClient();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password = new PasswordOptions
                {
                    RequiredLength = 1,
                    RequiredUniqueChars = 0,
                    RequireNonAlphanumeric = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireDigit = false,
                };

                options.Lockout = new LockoutOptions
                {
                    AllowedForNewUsers = true,
                    MaxFailedAccessAttempts = 999,
                };
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddApiEndpoints();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;

                //if (webHostEnvironment.IsProduction())
                //{
                //    options.LicenseKey = LoadLicenseKey(webHostEnvironment);
                //}

            })
              .AddAspNetIdentity<ApplicationUser>();

            var connectStr = configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;

            builder.AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseNpgsql(connectStr,
                    sql => sql.MigrationsAssembly(migrationsAssembly));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600; // interval in seconds (default is 3600)
            });

            return services;
        }

    }
}
