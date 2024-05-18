// Importowanie potrzebnych bibliotek
using Microsoft.Data.Sqlite;

namespace GitHub_Helper;

// Definicja klasy DatabaseConnection, która implementuje interfejs IDisposable
public class DatabaseConnection : IDisposable
{
    // Prywatne pole przechowujące połączenie z bazą danych
    private readonly SqliteConnection _connection;

    // Konstruktor klasy, który inicjalizuje połączenie z bazą danych
    public DatabaseConnection(string databaseName)
    {
        // Tworzenie łańcucha połączenia z bazą danych
        var connectionString = $@"Data Source=..\..\..\{databaseName}.db";
        // Inicjalizacja połączenia z bazą danych
        _connection = new SqliteConnection(connectionString);
        // Otwarcie połączenia z bazą danych
        _connection.Open();
    }

    // Metoda wykonująca zapytania do bazy danych i zwracająca wyniki
    public List<Dictionary<string, Tuple<string, string, string, string>>> ExecuteQueriesAndReturnResults(params string[] queries)
    {
        // Dla każdego zapytania tworzymy słownik przechowujący wyniki zapytania
        return queries.Select(query =>
        {
            var gitCommands = new Dictionary<string, Tuple<string, string, string, string>>();
            try
            {
                // Tworzenie nowego polecenia SQL
                using var command = _connection.CreateCommand();
                // Ustawienie tekstu polecenia SQL
                command.CommandText = query;
                // Wykonanie polecenia SQL i otrzymanie wyników
                using var reader = command.ExecuteReader();
                // Czytanie wyników zapytania
                while (reader.Read())
                {
                    // Pobieranie wartości z kolumn wyników zapytania
                    var columnId = reader.GetInt32(0);
                    var column1 = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                    var column2 = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    var column3 = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    var column4 = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);

                    // Dodawanie wyników do słownika
                    gitCommands[columnId.ToString()] = Tuple.Create(column1, column2, column3, column4);
                }
            }
            catch (Exception ex)
            {
                // Obsługa wyjątków podczas wykonywania zapytania
                Console.WriteLine($"Wystąpił błąd podczas wykonywania zapytania: {ex.Message}");
            }
            // Zwracanie wyników zapytania
            return gitCommands;
        }).ToList();
    }

    // Metoda zamykająca połączenie z bazą danych
    public void Dispose()
    {
        _connection?.Close();
    }
}