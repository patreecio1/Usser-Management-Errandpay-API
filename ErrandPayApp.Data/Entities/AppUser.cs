using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Data.Entities
{
    public class AppUser : Entity<int>
    {

        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTimeOffset? EmailConfirmationDate { get; set; }
        public int LockoutCounter { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime DateAccountLocked { get; set; }
        public DateTime? LastLogin { get; set; }

    }
}
