using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ErrandPayApp.Infrastructure.Jobs
{
 
    public class EmailNotificationJob : BackgoundService
    {
        public EmailNotificationJob(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "*/1 * * * * *";
 //        protected override string Schedule => "0 0 9 * * 1-6";

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            ILoggerService<EmailNotificationJob> _logger = serviceProvider.GetService<ILoggerService<EmailNotificationJob>>();
            try
            {
                _logger.LogInformation($"EmailSenderJob => {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

                IEmailSenderJob settlementManager = serviceProvider.GetService<IEmailSenderJob>();
                //await settlementManager.PushContractExpiringMails();
              //  await settlementManager.PushPendingApprovalContractsMails();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }
    }
}

 
