using Microsoft.Extensions.Configuration;

namespace CoalShortagePortal.Application.Security
{
    public class UserInitVariables
    {
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string AdminUserName { get; set; } = "admin";

        public void InitializeFromConfig(IConfiguration Configuration)
        {
            AdminEmail = Configuration["IdentityInit:AdminEmail"];
            AdminPassword = Configuration["IdentityInit:AdminPassword"];
            AdminUserName = Configuration["IdentityInit:AdminUserName"];
        }
    }
}
