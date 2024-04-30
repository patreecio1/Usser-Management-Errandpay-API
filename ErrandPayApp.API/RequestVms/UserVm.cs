using System.ComponentModel.DataAnnotations;

namespace ErrandPayApp.API.RequestVms
{
    public class UserVm : Model
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
      

        public string PhoneNumber { get; set; }
        public long[] RoleIds { get; set; }

    }
}
