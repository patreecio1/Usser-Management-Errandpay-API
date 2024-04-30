using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ErrandPayApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Utilities;

namespace ErrandPayApp.Data.Repositories
{
    public class ConfirmationTokenRepository : IConfirmationTokenRepository
    {
        private readonly APPContext _dbContext;
        private readonly IMapper _mapper;
        public ConfirmationTokenRepository(APPContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
              _mapper = mapper;
        }

        public async Task<ConfirmationTokenDto> Create(ConfirmationTokenDto model)
        {
            var entity = new ConfirmationToken
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                TokenId = model.TokenId,
                Token = CryptographyExtensions .Encrypt(model.Token),
                userId = model.UserId,
                ExpiredDate = model.ExpiredDate




            };// model.Map();


            _dbContext.Set<ConfirmationToken>().Add(entity);
            await _dbContext.SaveChangesAsync();
            var dto = _mapper.Map<ConfirmationTokenDto>(entity);
            return dto;
        }

        public async Task<bool> Deactivate(string TokenId)
        {
            var entity = await _dbContext.Set<ConfirmationToken>().FirstOrDefaultAsync(x => x.TokenId == TokenId);
            if (entity == null)
                return false;

            entity.IsDeleted = true;

            return true;
        }

        public async Task<ConfirmationTokenDto> Get(string TokenId)
        {
            var entity = await _dbContext.Set<ConfirmationToken>()
                .Select(x => new ConfirmationToken
                {
                    Token = CryptographyExtensions.Decrypt(x.Token),
                    Id = x.Id,
                    TokenId = x.TokenId,
                    userId = x.userId,
                    ExpiredDate = x.ExpiredDate,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate,


                })
                .FirstOrDefaultAsync(x => x.TokenId == TokenId);
            if (entity == null)
                return null;
            var dto = _mapper.Map<ConfirmationTokenDto>(entity);
            return dto;
        }

        public async Task<ConfirmationTokenDto> GetByToken(string token)
        {

            var entity = await _dbContext.Set<ConfirmationToken>().Select(x => new ConfirmationToken
            {
                Token = CryptographyExtensions.Decrypt(x.Token),
                Id = x.Id,
                TokenId = x.TokenId,
                userId = x.userId,
                ExpiredDate = x.ExpiredDate,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,


            }).FirstOrDefaultAsync(x => CryptographyExtensions.Decrypt(x.Token) == token);
            if (entity == null)
                return null;

            var dto =_mapper.Map<ConfirmationTokenDto>(entity);
            return dto;

        }
    }


}
