using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;

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

        public List<Dictionary<string, Tuple<string, string, string, string>>> ExecuteQueriesAndReturnResults(string[] queries)
        {
            var results = new List<Dictionary<string, Tuple<string, string, string, string>>>();

            foreach (var query in queries)
            {
                var gitCommands = new Dictionary<string, Tuple<string, string, string, string>>();

                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var columnId = reader.GetInt32(0);
                            var column1 = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            var column2 = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            var column3 = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                            var column4 = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);

                            gitCommands[columnId.ToString()] = new Tuple<string, string, string, string>(column1, column2, column3, column4);
                        }
                    }
                }

                results.Add(gitCommands);
            }

            return results;
        }
    }
}
