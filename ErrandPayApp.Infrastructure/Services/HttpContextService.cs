using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ErrandPayApp.Infrastructure.Utilities;
using ErrandPayApp.Core.Interfaces.Services;

namespace ErrandPayApp.Infrastructure.Services
{
   public  class HttpContextService : IHttpContextService
    {

        private readonly IMemoryCache memoryCache;
        private readonly IHttpService httpService;
        private readonly IHttpContextAccessor _httpAccessor;

        public HttpContextService(IMemoryCache memoryCache,
            IHttpService httpService, IHttpContextAccessor httpContextAccessor)
        {
            _httpAccessor = httpContextAccessor;
            
            this.memoryCache = memoryCache;
            this.httpService = httpService;
        }

        public int CurrentUserId()
        {
            try
            {
                var claim = _httpAccessor.HttpContext.User.Claims
                    .Where(x => x.Type == ClaimTypes.NameIdentifier)
                    .FirstOrDefault();

                if (claim != null && int.TryParse(claim.Value, out int userId))
                {
                    return userId;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        public string CurrentClientId()
        {
            try
            {
                var claim = _httpAccessor.HttpContext.User.Claims
                       .Where(x => x.Type == "client_id")
                       .FirstOrDefault();
                if (claim == null)
                    return null;

                return claim.Value;
            }
            catch
            {
                return null;
            }
        }
        public string CurrentUsername()
        {
            try
            {
                var claim = _httpAccessor.HttpContext.User.Claims
                       .Where(x => x.Type == ClaimTypes.Email)
                       .FirstOrDefault();
                return claim.Value;
            }
            catch (Exception ex)
            {
                Log<IHttpContextService>.LogError(ex);

                return null;
            }
        }

        public string GetCalerIP()
        {
            var e = _httpAccessor.HttpContext.Request.Headers;
            if (e.ContainsKey("X-Forwarded-For"))
            {
                string ip = e["X-Forwarded-For"];
                ip = ip.Replace("[", string.Empty);
                ip = ip.Replace("\\", string.Empty);
                ip = ip.Replace("]", string.Empty);
                ip = ip.Replace("\"", string.Empty);
                return ip;
            }

            return "127.0.0.1";
        }
    }
}
