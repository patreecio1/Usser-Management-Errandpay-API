using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Services
{
    public interface IExcelService
    {
        T[] Read<T>(Stream stream);
        T[] ReadEmployeeUpload<T>(Stream stream, ref string[] columnnames);
    }
}
