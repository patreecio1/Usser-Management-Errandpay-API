using ErrandPayApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Repositories
{
    public interface IConfirmationTokenRepository
    {
        Task<ConfirmationTokenDto> Create(ConfirmationTokenDto model);
        Task<bool> Deactivate(string tokenId);
        Task<ConfirmationTokenDto> Get(string tokenId);
        Task<ConfirmationTokenDto> GetByToken(string token);
    }
}
