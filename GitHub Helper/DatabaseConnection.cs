using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub_Helper
{
    public class DatabaseConnection
    {
        private string _connectionString;

        public DatabaseConnection(string databaseName)
        {
            _connectionString = $"Data Source={databaseName}.db";
        }

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }
    }
}
