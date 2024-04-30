using System.ComponentModel.DataAnnotations;

namespace ErrandPayApp.API.RequestVms
{
    public class MultipleUserRoleRequestVM : Model
    {
        [Required(ErrorMessage = "User is required")]
        public int userId { get; set; }

        public long[] RoleIds { get; set; }
    }
}
