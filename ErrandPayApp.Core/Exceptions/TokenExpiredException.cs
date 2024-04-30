using ErrandPayApp.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Exceptions
{
    public class TokenExpiredException : BaseException
    {
        public TokenExpiredException(string message) : base(message)
        {
            httpStatusCode = HttpStatusCode.BadRequest;
            Code = ResponseCodes.TokenExpired;
        }
    }
}
