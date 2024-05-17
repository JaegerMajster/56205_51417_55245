using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GitHub_Helper
{
    public class DatabaseConnection
    {
        private SqliteConnection _connection;

        public DatabaseConnection(string databaseName)
        {
            try
            {
                var connectionString = $@"Data Source=..\..\..\{databaseName}.db";
                _connection = new SqliteConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił wyjątek podczas tworzenia połączenia z bazą danych: {ex.Message}");
            }
        }

        public void OpenConnection()
        {
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił wyjątek podczas otwierania połączenia z bazą danych: {ex.Message}");
            }
        }

        public void CloseConnection()
        {
            try
            {
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił wyjątek podczas zamykania połączenia z bazą danych: {ex.Message}");
            }
        }

        public Dictionary<string, Tuple<string, string, string, string>> ExecuteQueryAndReturnResults(string query)
        {
            Dictionary<string, Tuple<string, string, string, string>> gitCommands = new();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = query;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var komenda = reader.GetString(1);
                        var opisKomendy = reader.GetString(2);
                        var skladnia = reader.GetString(3);
                        var opis = reader.GetString(4);

                        gitCommands[id.ToString()] = new Tuple<string, string, string, string>(komenda, opisKomendy, skladnia, opis);
                    }
                }
            }

            return gitCommands;
        }
    }
}
