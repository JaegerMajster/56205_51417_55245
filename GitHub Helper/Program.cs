using GitHub_Helper;

// Użycie klasy DatabaseConnection do połączenia z bazą danych i wykonania zapytań
using (var dbConnection = new DatabaseConnection("GitHub_Helper"))
{
    // Wykonanie zapytań do bazy danych i otrzymanie wyników
    var resultsList = dbConnection.ExecuteQueriesAndReturnResults("SELECT * FROM KomendyGit", "SELECT * FROM ParametryKomend", "SELECT * FROM PrzykladyKomend");

    // Pętla umożliwiająca użytkownikowi wprowadzanie komend
    string? userInput = "";
    while (userInput != "koniec")
    {
        // Wyświetlanie dostępnych komend
        DisplayCommands(resultsList[0]);

        // Pobieranie komendy od użytkownika
        userInput = Console.ReadLine();

        while (userInput != null && !resultsList[0].ContainsKey(userInput) && userInput != "koniec")
        {
            // Wyświetlanie komunikatu o błędzie, jeśli komenda nie jest dostępna
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nie rozumiem Twojej czynności. Spróbuj użyć numer z menu.");
            Console.ResetColor();
            userInput = Console.ReadLine();
        }

        // Sprawdzanie, czy wprowadzona komenda jest dostępna
        if (userInput != null && resultsList[0].TryGetValue(userInput, out Tuple<string, string, string, string>? value))
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
        Console.Write($"{item.Key,2}.");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write($"{item.Value.Item1,-20}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(item.Value.Item2);
        Console.ResetColor();
    }
    Console.WriteLine();
    Console.Write("Wpisz numer komendy aby wyświetlić jej szczegóły lub 'koniec' aby zakończyć: ");
}

// Metoda wyświetlająca szczegóły konkretnej komendy
static void DisplayCommandDetails(string command, List<Dictionary<string, Tuple<string, string, string, string>>> resultsList)
{
    // Czyszczenie konsoli
    DisplayClear();

    // Wyświetlanie menu szczegółów komendy
    string? detailInput = "";
    do
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{resultsList[0][command].Item1}");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{resultsList[0][command].Item2}");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"Menu komendy:");
        Console.WriteLine();

        // Tworzenie listy dostępnych szczegółów komendy
        var availableDetails = new List<string>();
        if (!string.IsNullOrWhiteSpace(resultsList[0][command].Item4))
            availableDetails.Add("Opis");
        if (resultsList[2].Any(item => item.Value.Item1 == command))
            availableDetails.Add("Przykłady");
        if (resultsList[1].Any(item => item.Value.Item1 == command))
            availableDetails.Add("Opcje");
        if (!string.IsNullOrWhiteSpace(resultsList[0][command].Item3))
            availableDetails.Add("Składnia");

        // Wyświetlanie dostępnych szczegółów komendy z automatyczną numeracją
        for (int i = 0; i < availableDetails.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{availableDetails[i]}");
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.Write("Wybierz szczegół komendy do wyświetlenia: ");

        detailInput = Console.ReadLine();
        DisplayClear();

        // Sprawdzanie, czy wprowadzony numer szczegółu jest prawidłowy
        if (int.TryParse(detailInput, out int detailNumber) && detailNumber > 0 && detailNumber <= availableDetails.Count)
        {
            switch (availableDetails[detailNumber - 1])
            {
                case "Opis":
                    DisplayCommandDescription(command, resultsList[0]);
                    break;
                case "Przykłady":
                    DisplayCommandExamples(command, resultsList[2]);
                    break;
                case "Opcje":
                    DisplayCommandParameters(command, resultsList[1]);
                    break;
                case "Składnia":
                    DisplayCommandSyntax(command, resultsList[0]);
                    break;
            }
            Console.Write("Wciśnij cokolwiek, aby wrócić...");
            Console.ReadLine();
            DisplayClear();
        }
        else return;

    } while (detailInput != "");

    // Czyszczenie konsoli
    DisplayClear();
}

// Metoda wyświetlająca opis komendy
static void DisplayCommandDescription(string command, Dictionary<string, Tuple<string, string, string, string>> commands)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("OPIS");
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine($"{commands[command].Item4}");
    Console.WriteLine();
}

// Metoda wyświetlająca składnię komendy
static void DisplayCommandSyntax(string command, Dictionary<string, Tuple<string, string, string, string>> commands)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("SKŁADNIA");
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(commands[command].Item3);
    Console.ResetColor();
    Console.WriteLine();
}

// Metoda wyświetlająca parametry komendy
static void DisplayCommandParameters(string command, Dictionary<string, Tuple<string, string, string, string>> parameters)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"OPCJE");
    Console.ResetColor();
    Console.WriteLine();
    foreach (var item in parameters.Where(item => item.Value.Item1 == command))
    {
        if (!string.IsNullOrWhiteSpace(item.Value.Item2))
            Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{item.Value.Item2}");
        Console.ResetColor();
        if (!string.IsNullOrWhiteSpace(item.Value.Item3))
            Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{item.Value.Item3}");
        Console.ResetColor();
        if (!string.IsNullOrWhiteSpace(item.Value.Item4))
            Console.WriteLine($"{item.Value.Item4}");
        Console.WriteLine();
    }
}

// Metoda wyświetlająca przykłady użycia komendy
static void DisplayCommandExamples(string command, Dictionary<string, Tuple<string, string, string, string>> examples)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("PRZYKŁADY");
    Console.ResetColor();
    Console.WriteLine();

    foreach (var item in examples.Where(item => item.Value.Item1 == command))
    {
        if (!string.IsNullOrWhiteSpace(item.Value.Item2))
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{item.Value.Item2}\n");
        Console.ResetColor();
        if (!string.IsNullOrWhiteSpace(item.Value.Item3))
            Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{item.Value.Item3}\n");
        Console.ResetColor();
        if (!string.IsNullOrWhiteSpace(item.Value.Item4))
            Console.WriteLine($"{item.Value.Item4}\n");
        Console.WriteLine();
    }
}

// Metoda czyszcząca konsolę
static void DisplayClear()
{
    Console.Clear();
    Console.WriteLine("\x1b[3J");
    Console.Clear();
}
