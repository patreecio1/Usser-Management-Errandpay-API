
using System;
using System.Collections.Generic;
using System.Data;
 using Microsoft.Extensions.Configuration;
using ErrandPayApp.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace ErrandPayApp.Data
{
    public class DapperContext: IDapperContext
    {
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection()
        {
            string _connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(_connectionString);
        }

    }
}
