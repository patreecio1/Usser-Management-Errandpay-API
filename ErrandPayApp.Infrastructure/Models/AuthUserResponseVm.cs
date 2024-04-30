using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Infrastructure.Models
{
    public class AuthUserResponseVm
    {
        public string Email { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public int userId { get; set; }
        public string IsEmailConfirmed { get; set; }
    }
}
