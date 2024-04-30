using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using IdentityModel;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Core.Dtos;
using ErrandPayApp.Core.Models;


namespace ErrandPayApp.Infrastructure.Services
{
    public  class JWTService: IJWTService
    {

		private readonly IConfiguration iconfiguration;
		public JWTService(IConfiguration iconfiguration)
        {
			this.iconfiguration = iconfiguration;

		}

        public TokenInfo GenerateToken(AppUserDto user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["AuthConfig:Key"]);

                // Access token
                var accessTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, $"{user.SurName} {user.FirstName}"),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Surname, user.SurName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15), // Short expiry for access token
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
                var accessTokenString = tokenHandler.WriteToken(accessToken);

                // Refresh token
                var refreshTokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddDays(2), // Example: Refresh token expiry in 30 days
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);
                var refreshTokenString = tokenHandler.WriteToken(refreshToken);

                return new TokenInfo
                {
                    AccessToken = accessTokenString,
                    RefreshToken = refreshTokenString,
                    AccessTokenExpiry = accessTokenDescriptor.Expires ?? DateTime.UtcNow.AddMinutes(15),
                    RefreshTokenExpiry = refreshTokenDescriptor.Expires ?? DateTime.UtcNow.AddDays(30)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
