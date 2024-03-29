using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoalShortagePortal.Data;
using CoalShortagePortal.Application.Security;
using CoalShortagePortal.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CoalShortagePortal.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CoalShortagePortal.WebApp")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 2;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // configure login path for return urls
                // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
            });

            services.Configure<CookieTempDataProviderOptions>(options =>
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest
            );

            services.AddInfrastructure(Configuration, Environment);

            services
                .AddControllersWithViews(options =>
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                .AddRazorRuntimeCompilation();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // https://dotnetcoretutorials.com/2017/01/08/set-x-frame-options-asp-net-core/
            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            //    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            //    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //    context.Response.Headers.Add("Content-Security-Policy",
            //                 "default-src 'self'");
            //    context.Response.Headers.Add("Cache-Control", "private");
            //    await next();
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // seed Admin User and Admin, Guest Roles
            AppIdentityInitializer identityInitializer = new AppIdentityInitializer()
            {
                UserManager = userManager,
                RoleManager = roleManager,
                Configuration = Configuration
            };
            identityInitializer.SeedData();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
