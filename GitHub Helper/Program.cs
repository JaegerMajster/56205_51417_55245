using GitHub_Helper;

var dbConnection = new DatabaseConnection("GitHub_Helper");
dbConnection.OpenConnection();
var results = dbConnection.ExecuteQueryAndReturnResults("SELECT * FROM KomendyGit");
dbConnection.CloseConnection();

foreach (var item in results)
{
    Console.WriteLine($"ID: {item.Key}, Komenda: {item.Value.Item1}, Opis: {item.Value.Item2}");
}

Console.ReadLine();