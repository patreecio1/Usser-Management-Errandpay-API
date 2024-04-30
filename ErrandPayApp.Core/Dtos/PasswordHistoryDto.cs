using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Dtos
{
    public class PasswordHistoryDto
    {
        public int userId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsCurrentPassword { get; set; }
    }
}
