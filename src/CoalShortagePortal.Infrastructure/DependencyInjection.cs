using CoalShortagePortal.Core.Interfaces.Sms;
using CoalShortagePortal.Infrastructure.Services.Email;
using CoalShortagePortal.Infrastructure.Services.Sms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoalShortagePortal.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            // add email settings from app config
            EmailConfiguration emailConfig = new EmailConfiguration();
            configuration.Bind("EmailSettings", emailConfig);
            services.AddSingleton(emailConfig);

            // add sms settings from app config
            SmsConfiguration smsConfig = new SmsConfiguration();
            configuration.Bind("SmsSettings", smsConfig);
            services.AddSingleton(smsConfig);

            // Add Infra services
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmsSender, SmsSender>();

            return services;
        }
    }
}
