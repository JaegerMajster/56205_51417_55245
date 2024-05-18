using GitHub_Helper;

// Użycie klasy DatabaseConnection do połączenia z bazą danych i wykonania zapytań
using (var dbConnection = new DatabaseConnection("GitHub_Helper"))
{
    // Wykonanie zapytań do bazy danych i otrzymanie wyników
    var resultsList = dbConnection.ExecuteQueriesAndReturnResults("SELECT * FROM KomendyGit", "SELECT * FROM ParametryKomend", "SELECT * FROM PrzykladyKomend");

    // Pętla umożliwiająca użytkownikowi wprowadzanie komend
    string ?userInput = "";
    while (userInput != "koniec")
    {
        // Wyświetlanie dostępnych komend
        DisplayCommands(resultsList[0]);

        // Pobieranie komendy od użytkownika
        userInput = Console.ReadLine();

        while (!resultsList[0].ContainsKey(userInput) && userInput != "koniec")
        {
            // Wyświetlanie komunikatu o błędzie, jeśli komenda nie jest dostępna
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nie rozumiem Twojej czynności. Spróbuj użyć innych słów lub sprawdź pisownię.");
            Console.ResetColor();
            userInput = Console.ReadLine();
        }

        // Sprawdzanie, czy wprowadzona komenda jest dostępna
        if (resultsList[0].TryGetValue(userInput, out Tuple<string, string, string, string>? value))
        {
            var commandDetails = value;
            // Wyświetlanie szczegółów komendy
            DisplayCommandDetails(userInput, resultsList);
        }

    }
}

// Metoda wyświetlająca dostępne komendy dla użytkownika
static void DisplayCommands(Dictionary<string, Tuple<string, string, string, string>> commands)
{
    Console.WriteLine("Helper do komend git");
    Console.WriteLine();
    foreach (var item in commands)
    {
        Console.WriteLine($"{item.Key,2}.{item.Value.Item1,-20} {item.Value.Item2}");
    }
    Console.WriteLine();
    Console.WriteLine("Wpisz 'koniec' aby zakończyć.");
}

// Metoda wyświetlająca szczegóły konkretnej komendy
static void DisplayCommandDetails(string command, List<Dictionary<string, Tuple<string, string, string, string>>> resultsList)
{
    // Czyszczenie konsoli
    DisplayClear();

    // Wyświetlanie parametrów komendy
    DisplayCommandParameters(command, resultsList[1]);
    // Wyświetlanie przykładów użycia komendy
    DisplayCommandExamples(command, resultsList[2]);

    // Pobieranie i wyświetlanie opisu oraz składni komendy
    Console.WriteLine($"{resultsList[0][command].Item4}");
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Składnia:\n {resultsList[0][command].Item3}");
    Console.ResetColor();

    // Wyświetlanie instrukcji dla użytkownika
    Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić...");
    Console.ReadKey();

    // Czyszczenie konsoli
    DisplayClear();
}

// Metoda wyświetlająca parametry komendy
static void DisplayCommandParameters(string command, Dictionary<string, Tuple<string, string, string, string>> parameters)
{
    foreach (var item in parameters.Where(item => item.Value.Item1 == command))
    {
        if (!string.IsNullOrWhiteSpace(item.Value.Item2))
            Console.WriteLine($"Parametr 1:\n{item.Value.Item2}");
        if (!string.IsNullOrWhiteSpace(item.Value.Item3))
            Console.WriteLine($"Parametr 2:\n{item.Value.Item3}");
        if (!string.IsNullOrWhiteSpace(item.Value.Item4))
            Console.WriteLine($"Opis Parametru:\n{item.Value.Item4}");
    }
}

// Metoda wyświetlająca przykłady użycia komendy
static void DisplayCommandExamples(string command, Dictionary<string, Tuple<string, string, string, string>> examples)
{
    foreach (var item in examples.Where(item => item.Value.Item1 == command))
    {
        if (!string.IsNullOrWhiteSpace(item.Value.Item2))
            Console.WriteLine($"Opis przed:\n{item.Value.Item2}");
        if (!string.IsNullOrWhiteSpace(item.Value.Item3))
            Console.WriteLine($"Kod:\n{item.Value.Item3}");
        if (!string.IsNullOrWhiteSpace(item.Value.Item4))
            Console.WriteLine($"Opis po:\n{item.Value.Item4}");
    }
}

// Metoda czyszcząca konsolę
static void DisplayClear()
{
    Console.Clear();
    Console.WriteLine("\x1b[3J");
    Console.Clear();
}