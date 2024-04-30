using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Services
{
    public interface IDocumentService
    {
        byte[] GeneratePdf(string htmlContent);
    }
}
