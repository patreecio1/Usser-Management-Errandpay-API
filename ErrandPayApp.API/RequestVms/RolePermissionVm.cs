using System.ComponentModel.DataAnnotations;

namespace ErrandPayApp.API.RequestVms
{
    public class RolePermissionVm : Model
    {
        public RolePermissionVm()
        {
        }

        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public long RoleId { get; set; }

        [Required(ErrorMessage = "Permission is required")]
        public long PermissionId { get; set; }


        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanRead { get; set; }

        public bool CanTerminate { get; set; }
        public bool CanApprove { get; set; }
    }
}
