using GitHub_Helper;

// Główna funkcja programu
using (var dbConnection = new DatabaseConnection("GitHub_Helper"))
{
    try
    {
        var resultsList = dbConnection.ExecuteQueriesAndReturnResults("SELECT * FROM KomendyGit ORDER BY ID ASC", "SELECT * FROM ParametryKomend", "SELECT * FROM PrzykladyKomend");
        ProcessUserInput(resultsList);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Wystąpił błąd: {ex.Message}");
    }
}

// Funkcja do przetwarzania wejścia użytkownika
static void ProcessUserInput(List<Dictionary<string, Tuple<string, string, string, string>>> resultsList)
{
    string userInput;
    do
    {
        DisplayCommands(resultsList[0]);
        userInput = Console.ReadLine() ?? "";

        if (resultsList[0].TryGetValue(userInput, out Tuple<string, string, string, string>? value))
        {
            DisplayCommandDetails(userInput, resultsList);
        }
        else if (userInput != "koniec")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Nie rozpoznano wprowadzonej komendy. Proszę wpisać numer komendy z listy dostępnych komend lub wpisz 'koniec', aby zakończyć program: "); ;
            Console.ResetColor();
        }
    }
    while (userInput != "koniec");
}
// Funkcja do wyświetlania dostępnych komend
static void DisplayCommands(Dictionary<string, Tuple<string, string, string, string>> commands)
{
    Console.WriteLine("Helper do komend git\n");
    foreach (var item in commands)
    {
        Console.Write($"{item.Key,2}.");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write($"{item.Value.Item1,-20}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(item.Value.Item2);
        Console.ResetColor();
    }
    Console.Write("\nNapisz numer komendy, aby zobaczyć więcej szczegółów na jej temat, lub wpisz 'koniec', aby zakończyć program: ");
}
// Funkcja do wyświetlania szczegółów wybranej komendy
static void DisplayCommandDetails(string command, List<Dictionary<string, Tuple<string, string, string, string>>> resultsList)
{
    DisplayClear();
    string? detailInput = "";

    do
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{resultsList[0][command].Item1}");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{resultsList[0][command].Item2}\n");
        Console.ResetColor();
        Console.WriteLine("Menu komendy:\n");

        var availableDetails = new List<string>();
        if (!string.IsNullOrWhiteSpace(resultsList[0][command].Item4))
            availableDetails.Add("Opis");
        if (resultsList[2].Any(item => item.Value.Item1 == command))
            availableDetails.Add("Przykłady");
        if (resultsList[1].Any(item => item.Value.Item1 == command))
            availableDetails.Add("Opcje");
        if (!string.IsNullOrWhiteSpace(resultsList[0][command].Item3))
            availableDetails.Add("Składnia");

        for (int i = 0; i < availableDetails.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{availableDetails[i]}");
            Console.ResetColor();
        }

        Console.Write("\nWybierz szczegół komendy do wyświetlenia: ");
        detailInput = Console.ReadLine();
        if (int.TryParse(detailInput, out int detailNumber) && detailNumber > 0 && detailNumber <= availableDetails.Count)
        {
            DisplayClear();
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
            Console.ReadKey();
            DisplayClear();
        }
        else return;

    } while (detailInput != "");
}
// Funkcja do wyświetlania opisu wybranej komendy
static void DisplayCommandDescription(string command, Dictionary<string, Tuple<string, string, string, string>> commands)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("OPIS\n");
    Console.ResetColor();
    Console.WriteLine($"{commands[command].Item4}\n");
}
// Funkcja do wyświetlania składni wybranej komendy
static void DisplayCommandSyntax(string command, Dictionary<string, Tuple<string, string, string, string>> commands)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("SKŁADNIA\n");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(commands[command].Item3);
    Console.ResetColor();
    Console.WriteLine();
}
// Funkcja do wyświetlania parametrów wybranej komendy
static void DisplayCommandParameters(string command, Dictionary<string, Tuple<string, string, string, string>> parameters)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("OPCJE\n");
    Console.ResetColor();
    foreach (var item in parameters.Where(item => item.Value.Item1 == command))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{item.Value.Item2}");
        Console.ResetColor();
        Console.WriteLine(WordWrap(item.Value.Item4));
    }
}
// Funkcja do wyświetlania przykładów użycia wybranej komendy
static void DisplayCommandExamples(string command, Dictionary<string, Tuple<string, string, string, string>> examples)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("PRZYKŁADY\n");
    Console.ResetColor();

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
// Funkcja do zawijania tekstu na podstawie szerokości konsoli
static string WordWrap(string text, int edgeOffset = -5)
{
    string[] paragraphs = text.Split('\n');
    string result = "  ";
    int lineLength = 0;
    int maxLength = Console.WindowWidth + edgeOffset;
    foreach (string paragraph in paragraphs)
    {
        string[] words = paragraph.Split(' ');

        foreach (string word in words)
        {
            if (lineLength + word.Length > maxLength)
            {
                result += Environment.NewLine + "  ";
                lineLength = 0;
            }

            result += word + " ";
            lineLength += word.Length + 1;
        }
        result += Environment.NewLine + "  ";
        lineLength = 0;
    }

    return result;
}
// Funkcja do czyszczenia konsoli
static void DisplayClear()
{
    Console.Clear();
    Console.WriteLine("\x1b[3J");
    Console.Clear();
}