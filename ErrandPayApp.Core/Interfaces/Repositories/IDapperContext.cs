using System.Data;

namespace ErrandPayApp.Core.Interfaces.Repositories
{
    public interface IDapperContext
    {
        IDbConnection GetDbConnection();
    
    }
}
