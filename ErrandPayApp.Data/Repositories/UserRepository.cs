using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ErrandPayApp.Data.Entities;
using ErrandPayApp.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Models;
using ErrandPayApp.Core.Utilities;

namespace ErrandPayApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextService httpContextService;
        private readonly IMapper _mapper;
        private readonly APPContext _dbContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDapperContext dapperContext;
        private readonly IUtilityService utilityService;
        private readonly ILoggerService<UserRepository> loggerService;
        private readonly IRoleRepository roleRepository;
        public UserRepository(IMapper mapper,
           
            IDapperContext dapperContext, IUtilityService utilityService, 
             IHttpContextService httpContextService, IRoleRepository roleRepository, IMemoryCache memoryCache, APPContext    aPPContext, ILoggerService<UserRepository> loggerService)
        {
            this.memoryCache = memoryCache;
            this.httpContextService = httpContextService;
             _mapper = mapper;
            this._dbContext = aPPContext;
            this.dapperContext = dapperContext;
            this.utilityService = utilityService;
            this.loggerService = loggerService;
            this.roleRepository = roleRepository;
        }
     

        public async Task<AppUserDto> CreateUser(AppUserDto userDto)
        {
            try
            {
                // AppUser entity = _mapper.Map<AppUser>(userDto);
                var entity = new AppUser
                {
                    FirstName = userDto.FirstName,
                    SurName = userDto.SurName,
                    Email = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber,
                    State = userDto.State,
                    Country = userDto.Country,
                };

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                entity.PasswordHash = passwordHash;
                entity.PasswordSalt = passwordSalt;

                entity.CreatedBy = "Admin";
                entity.ModifiedBy = "Admin";
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
                entity.CreatedBy = "Admin";
                _dbContext.AppUsers.Add(entity);

                await _dbContext.SaveChangesAsync();

                return _mapper.Map<AppUserDto>(entity);
            }
            catch (Exception ex)
            {

                throw ex;
            } 
          
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<AppUserDto> GetUserByEmail(string email)
        {

            var result = await (from appUser in _dbContext.Set<AppUser>()

                                where appUser.Email == email 

                                select new AppUserDto
                                {

                                    Email = appUser.Email,
                                    PhoneNumber = appUser.PhoneNumber,
                                    SurName = appUser.SurName,
                                    FirstName = appUser.FirstName,
                                    Id = appUser.Id,
                                    LockoutCounter = appUser.LockoutCounter,
                                    IsLockedOut = appUser.IsLockedOut,
                                    LastLogin = appUser.LastLogin,
                                    DateAccountLocked = appUser.DateAccountLocked,
                                    State = appUser.State,
                                    Country = appUser.Country,
                                    PasswordHash = appUser.PasswordHash,
                                    PasswordSalt = appUser.PasswordSalt
                                }).FirstOrDefaultAsync();



            return result;
             
        }
        public async Task<AppUserDto> Login(string email , string password)
        {

            var result = await (from appUser in _dbContext.Set<AppUser>()

                                where appUser.Email == email

                                select new AppUserDto
                                {
                                    Email = appUser.Email,
                                    PhoneNumber = appUser.PhoneNumber,
                                    SurName = appUser.SurName,
                                    FirstName = appUser.FirstName,
                                    Id = appUser.Id,
                                    LockoutCounter = appUser.LockoutCounter,
                                    IsLockedOut = appUser.IsLockedOut,
                                    LastLogin = appUser.LastLogin,
                                    DateAccountLocked = appUser.DateAccountLocked,
                                    State = appUser.State,
                                    Country = appUser.Country
                                }).FirstOrDefaultAsync();


            if (result == null)
            {
                return null; 
            }
               
            else
            {
                return result; 
            }

         
        }

        public async Task<AppUserDto[]> GetAllUsers()
        {
            var result = await (from appUser in _dbContext.Set<AppUser>()

                                select new AppUserDto
                                {
                                    Email = appUser.Email,
                                    PhoneNumber = appUser.PhoneNumber,
                                    SurName = appUser.SurName,
                                    FirstName = appUser.FirstName,
                                    Id = appUser.Id,
                                    LockoutCounter = appUser.LockoutCounter,
                                    IsLockedOut = appUser.IsLockedOut,
                                    LastLogin = appUser.LastLogin,
                                    DateAccountLocked = appUser.DateAccountLocked,
                                    State = appUser.State,
                                    Country = appUser.Country
                                }).ToArrayAsync();



            return result;
        }


        public async Task<AppUserDto> GetById(int userId)
        {
            AppUser entity = await _dbContext.AppUsers.FindAsync(userId);
            if (entity == null)
                return null;

            return _mapper.Map<AppUserDto>(entity);
        }

    }
}
