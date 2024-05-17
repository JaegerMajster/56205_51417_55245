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
        Console.WriteLine($"{item.Key}. {item.Value.Item1}. {item.Value.Item2}");
    }
    Console.WriteLine();
    Console.WriteLine("Wpisz 'koniec' aby zakończyć.");

    userInput = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(userInput))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Nie wprowadziłeś żadnej czynności. Spróbuj ponownie.");
        Console.ResetColor();
        userInput = Console.ReadLine();
    }

    while (!results.ContainsKey(userInput))
    {
        if (userInput == "koniec")
        {
            break;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Nie rozumiem Twojej czynności. Spróbuj użyć innych słów lub sprawdź pisownię.");
        Console.ResetColor();
        userInput = Console.ReadLine();
    }

    if (results.ContainsKey(userInput))
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
        Console.Clear();

        string desc = results[userInput].Item4;
        string syntax = results[userInput].Item3;
        Console.WriteLine(desc);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Składnia:\n {syntax}");
        Console.ResetColor();

        Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić...");
        Console.ReadKey();
        Console.Clear();
    }
}