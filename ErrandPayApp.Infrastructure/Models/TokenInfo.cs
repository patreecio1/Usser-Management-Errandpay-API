using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Infrastructure.Models
{
    public class TokenInfo
    {
        public string AccessToken { get; set; }
        public DateTime Expiry { get; set; }
    }
}
