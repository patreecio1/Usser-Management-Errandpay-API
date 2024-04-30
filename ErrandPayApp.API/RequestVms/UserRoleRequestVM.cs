
using System.ComponentModel.DataAnnotations;
namespace ErrandPayApp.API.RequestVms
{
    public class UserRoleRequestVM : Model
    {
        [Required(ErrorMessage = "User is required")]
        public int userId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public int RoleId { get; set; }
    }
}
