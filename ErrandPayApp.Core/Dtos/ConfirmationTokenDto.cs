using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Dtos
{
    public class ConfirmationTokenDto : BaseModel<long>
    {
        public string TokenId { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
