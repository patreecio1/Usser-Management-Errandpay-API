using ErrandPayApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Services
{
    public interface IUtilityService
    {
        public string ToSha256(string value);
        string EncryptAes(string text);
        string DecryptAes(string cipherText);
        //EnvMode AppEnv();
    }
}
