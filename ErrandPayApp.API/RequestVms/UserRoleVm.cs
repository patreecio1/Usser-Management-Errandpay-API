using System.ComponentModel.DataAnnotations;

namespace ErrandPayApp.API.RequestVms
{
    public class UserRoleVm : Model
    {
        [Required(ErrorMessage = "Role is required")]
        public long RoleId { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int userId { get; set; }
    }
}
