using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Services
{
    public interface IHttpContextService
    {
        string CurrentUsername();
        int CurrentUserId();
      //  Task<string> ClientToken();
       // Dictionary<string, string> GetHeader();
        string GetCalerIP();
    }
}
