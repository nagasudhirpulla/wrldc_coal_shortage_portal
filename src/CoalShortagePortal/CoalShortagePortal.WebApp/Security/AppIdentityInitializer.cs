using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoalShortagePortal.WebApp.Security
{
    public static class AppIdentityInitializer
    {
        private static readonly string GuestUserRoleString = "GuestUser";
        private static readonly string AdministratorRoleString = "Administrator";

        /**
         * This method seeds admin, guest role and admin user
         * **/
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration Configuration)
        {
            // get admin params from configuration
            UserInitVariables initVariables = new UserInitVariables();
            initVariables.InitializeFromConfig(Configuration);
            // seed roles
            SeedUserRoles(roleManager);
            // seed admin user
            SeedAdminUser(userManager, initVariables);
        }

        /**
         * This method seeds admin user
         * **/
        public static void SeedAdminUser(UserManager<IdentityUser> userManager, UserInitVariables initVariables)
        {
            string AdminUserName = initVariables.AdminUserName;
            string AdminEmail = initVariables.AdminEmail;
            string AdminPassword = initVariables.AdminPassword;

            // check if admin user doesn't exist
            if (userManager.FindByNameAsync(AdminUserName).Result == null)
            {
                // create desired admin user object
                IdentityUser user = new IdentityUser
                {
                    UserName = AdminUserName,
                    Email = AdminEmail
                };

                // push desired admin user object to DB
                IdentityResult result = userManager.CreateAsync(user, AdminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, AdministratorRoleString).Wait();
                }
            }
        }

        /**
         * This method seeds roles
         * **/
        public static void SeedUserRoles(RoleManager<IdentityRole> roleManager)
        {
            // check if role doesn't exist
            if (!roleManager.RoleExistsAsync(GuestUserRoleString).Result)
            {
                // create desired role object
                IdentityRole role = new IdentityRole
                {
                    Name = GuestUserRoleString,
                };
                // push desired role object to DB
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync(AdministratorRoleString).Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = AdministratorRoleString,
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
