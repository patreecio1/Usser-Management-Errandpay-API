using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Exceptions;
using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Core.Models;
using ErrandPayApp.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Managers
{
    public class UserManager : IUserManager
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfirmationTokenRepository _confirmationTokenRepository;
        private readonly AppUrl _appUrl;
        private readonly IHttpContextService _httpContextService;
        private readonly IEmailService _emailService;
        private readonly IUserRoleRepository userRoleRepository;
        public UserManager( IUserRoleRepository userRoleRepository, IUserRepository userRepository, IConfirmationTokenRepository confirmationTokenRepository,
            IHttpContextService httpContextService,  AppUrl appUrl, IEmailService emailService)
        {

            _userRepository = userRepository;
            this._confirmationTokenRepository = confirmationTokenRepository;
            _appUrl = appUrl;
            _httpContextService = httpContextService;
            _emailService = emailService;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<AppUserDto> CreateUser(AppUserDto model)
        {

            if (string.IsNullOrWhiteSpace(model.Password))
                throw new BadRequestException("Password is required");


            if (string.IsNullOrWhiteSpace(model.Email))
                throw new BadRequestException("Email is required");

            if (!string.IsNullOrWhiteSpace(model.Password) && (model.Password != model.ConfirmPassword))
                throw new BadRequestException("Password and Confirm password do not match");

            var appUser = await _userRepository.CreateUser(model);
            if (appUser == null)
                throw new BadRequestException("User registration failed. Kindly retry or contact administrator if error persist");


            // Insert User Roles
            var rest = await userRoleRepository.Create(appUser.Id, model.RoleIds);

            return appUser;
        }

        public async Task<ConfirmationTokenDto> CreateToken(int userId)
        {
            var model = new ConfirmationTokenDto
            {
                TokenId = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower(),
                Token = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower(),
                UserId = userId,
                ExpiredDate = DateTime.Now.AddMinutes(15),
               // CreatedBy = _httpContextService.CurrentUserId()

            };

            return await _confirmationTokenRepository.Create(model);
        }

        public async Task<AppUserDto[]> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

    }
}
