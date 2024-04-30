using System.ComponentModel.DataAnnotations;

namespace ErrandPayApp.API.RequestVms
{
    public class ForgotPasswordRequestVm
    {
        public class ForgetPasswordRequestModel : Model
        {
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
            public string Email { get; set; }
        }
    }
}
