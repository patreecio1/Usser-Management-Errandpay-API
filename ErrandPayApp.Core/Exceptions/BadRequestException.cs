using ErrandPayApp.Core.Utilities;
using System.Net;

namespace ErrandPayApp.Core.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message)
        {
            httpStatusCode = HttpStatusCode.BadRequest;
            Code = ResponseCodes.BadReqeust;
        }
    }
}
