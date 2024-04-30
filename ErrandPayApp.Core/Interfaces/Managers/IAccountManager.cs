using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Managers
{
    public interface IAccountManager
    {
        Task<LoginResponseModel> Login(string email, string password);


    }
}
