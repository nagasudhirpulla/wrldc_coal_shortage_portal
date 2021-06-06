using System.Collections.Generic;
using System.Text;

namespace CoalShortagePortal.Infrastructure.Services.Email
{
    public class EmailConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string MailAddress { get; set; }
        public string HostName { get; set; }
    }
}
