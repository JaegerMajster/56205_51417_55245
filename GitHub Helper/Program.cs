using GitHub_Helper;

var dbConnection = new DatabaseConnection("GitHub_Helper");
dbConnection.OpenConnection();
dbConnection.ExecuteQueryAndPrintResults("SELECT * FROM KomendyGit");
dbConnection.CloseConnection();

Console.ReadLine();