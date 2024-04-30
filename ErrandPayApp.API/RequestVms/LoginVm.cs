using System;
using System.ComponentModel.DataAnnotations;

namespace ErrandPayApp.API.RequestVms
{
    public class LoginVm : Model
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
