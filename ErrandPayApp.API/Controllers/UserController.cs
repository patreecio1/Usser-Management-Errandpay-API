using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ErrandPayApp.API.RequestVms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Models;
using ErrandPayApp.Core.Utilities;

namespace ErrandPayApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        public UserController(IUserManager userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserVm model)
        {
            model.Validate();

            AppUserDto user = _mapper.Map<AppUserDto>(model);
            user = await _userManager.CreateUser(user);

            return Ok(ResponseModel<AppUserDto>.IsSuccessful(user, "Accont Registered"));
        }


        [HttpGet("getAllUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _userManager.GetAllUsers();
            return Ok(ResponseModel<AppUserDto[]>.IsSuccessful(users, "Successful"));
     
        }
    
    }
}
