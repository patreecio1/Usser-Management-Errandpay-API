using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ErrandPayApp.API.RequestVms;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Models;
using ErrandPayApp.Core.Utilities;
using ErrandPayApp.Core.Exceptions;
using static ErrandPayApp.API.RequestVms.ForgotPasswordRequestVm;
namespace ErrandPayApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        public AccountsController(IAccountManager accountManager)
        {
            this._accountManager = accountManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginVm model)
        {
            //validate my endpoint here yess
            model.Validate();
            LoginResponseModel result = await _accountManager.Login(model.Email, model.Password);
            return Ok(ResponseModel<LoginResponseModel>.IsSuccessful(result, "Successful"));
        }

      

    }
}
