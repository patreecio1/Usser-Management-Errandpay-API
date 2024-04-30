using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Exceptions;
using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Core.Models;
using ErrandPayApp.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Managers
{
    public  class AccountManager : IAccountManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService iJWTService;
        private readonly AppUrl _appUrl;
        private readonly IEmailService _emailService;
        private readonly IHttpContextService httpContextService;
        private readonly IUtilityService utilityService;
        private readonly IUserRoleRepository userRoleRepository;


        public AccountManager(IUserRoleRepository userRoleRepository, IUtilityService utilityService, IHttpContextService httpContextService, IUserRepository  userRepository, AppUrl appUrl,
           IEmailService emailService,  IJWTService jWTService)
        {
            _userRepository = userRepository;
            this.iJWTService = jWTService;
            this._appUrl = appUrl;
           this. _emailService = emailService;
            this.httpContextService = httpContextService;
            this.utilityService = utilityService;
            this.userRoleRepository = userRoleRepository;


        }

        public async Task<LoginResponseModel> Login(string email, string password)
        {

            AppUserDto user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new BadRequestException("Email is invalid");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadRequestException("Username or password is incorrect.");
            }
            var appuserDto  = await _userRepository.Login(email, password);

            if (appuserDto != null)
            {
                if (appuserDto.IsLockedOut)
                {
                    throw new BadRequestException("Account is locked out. Please contact Admin");
                }
            }
           
            var login = new LoginResponseModel();
            login.Profile = user;
          
            var roles = await userRoleRepository.GetRolesforUser(user.Id);

            login.Roles = roles;
            TokenInfo tokenInfo = iJWTService.GenerateToken(appuserDto);
            login.AccessTokenExpiry = tokenInfo.AccessTokenExpiry;
            login.RefreshTokenExpiry = tokenInfo.RefreshTokenExpiry;
            login.AccessToken = tokenInfo.AccessToken;
            login.RefreshToken = tokenInfo.RefreshToken;
            return login;
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


    }
}
