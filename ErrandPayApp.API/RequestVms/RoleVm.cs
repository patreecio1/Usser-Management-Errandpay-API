using System.ComponentModel.DataAnnotations;

namespace ErrandPayApp.API.RequestVms
{
    public class RoleVm : Model
    {
        public RoleVm()
        {
        }

        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string RoleDescription { get; set; }
    }
}
