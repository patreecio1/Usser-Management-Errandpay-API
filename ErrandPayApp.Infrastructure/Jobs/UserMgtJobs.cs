using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ErrandPayApp.Infrastructure.Jobs
{
    public class UserMgtJobs : BackgoundService
    {

        public UserMgtJobs(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        //    protected override string Schedule => "0 */30 * * * 1-6";
        protected override string Schedule => "0 */30 * * * *";

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            ILoggerService<UserMgtJobs> _logger = serviceProvider.GetService<ILoggerService<UserMgtJobs>>();
            try
            {
                _logger.LogInformation($"User ManagementJob => {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

                IAccountManager settleAccount = serviceProvider.GetService<IAccountManager>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }
    }
}
