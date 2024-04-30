using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Dtos
{
    public class AppUserDto : BaseModel<int>
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTimeOffset? EmailConfirmationDate { get; set; }
        public bool IsSuperAdmin { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
      
        public string Password { get; set; }
     
        public int LockoutCounter { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime DateAccountLocked { get; set; }
        public bool MustChangePassword { get; set; }
        public DateTime? LastLogin { get; set; }

        public DateTime ReleasedDate { get; set; }
        public String Message { get; set; }
        public long[] RoleIds { get; set; }

        public string Title { get; set; }

        public string SurName { get; set; }


        public string No { get; set; }

        public string Road { get; set; }


        public string City { get; set; }

        public string State { get; set; }


        public string Country { get; set; }


        public string Telephone { get; set; }


        public string ConfirmEmail { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
