using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<AppUserDto> CreateUser(AppUserDto userDto);
        Task<AppUserDto> GetUserByEmail(string email);
        Task<AppUserDto> Login(string email, string password);
        Task<AppUserDto[]> GetAllUsers();
        Task<AppUserDto> GetById(int userId);
    }
}
 