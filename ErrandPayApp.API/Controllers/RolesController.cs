using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ErrandPayApp.API.RequestVms;
using System.Threading.Tasks;
using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Models;
using ErrandPayApp.Core.Utilities;
using ErrandPayApp.Core.Dtos;


namespace ErrandPayApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRoleManager roleManager;

        public RolesController(IRoleManager roleManager, IMapper mapper)
        {
            this.mapper = mapper;
            this.roleManager = roleManager;
        }



        [HttpGet()]
        public async Task<IActionResult> GetRoles()
        {
            var result = await roleManager.GetRoles();
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result
            });
        }



        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleVm model)
        {
            model.Validate();

            RoleDto role = mapper.Map<RoleDto>(model);

            role = await roleManager.Create(role);
            return Ok(ResponseModel<RoleDto>.IsSuccessful(role, "Successful"));
        }
    }
}

