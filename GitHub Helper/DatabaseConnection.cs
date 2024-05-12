using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public void ExecuteQueryAndPrintResults(string query)
        {
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

                        Console.WriteLine($"ID: {id}, Komenda: {komenda}, Opis Komendy: {opisKomendy}");
                    }
                }
            }
        }
    }
}
