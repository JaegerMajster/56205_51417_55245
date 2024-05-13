using GitHub_Helper;

var dbConnection = new DatabaseConnection("GitHub_Helper");
dbConnection.OpenConnection();
var results = dbConnection.ExecuteQueryAndReturnResults("SELECT * FROM KomendyGit");
dbConnection.CloseConnection();

string userInput = "";
while (userInput != "koniec")
{
    Console.WriteLine("Helper do komend git");
    Console.WriteLine();
    foreach (var item in results)
    {
        Console.WriteLine($"{item.Key}. {item.Value.Item2}. {item.Value.Item1}");
    }
    Console.WriteLine();
    Console.WriteLine("Wpisz 'koniec' aby zakończyć.");
    userInput = Console.ReadLine();
}