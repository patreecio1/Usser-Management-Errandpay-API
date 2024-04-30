using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Managers
{
    public class EmailSenderJob: IEmailSenderJob
    {
        private readonly IEmailService emailService;
        private readonly IUserRepository userRepository;
        public EmailSenderJob( IEmailService emailService, IUserRepository userRepository)
        {
            this.emailService = emailService;
            this.userRepository = userRepository;
        }

      
    }
}
