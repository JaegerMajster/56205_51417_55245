using GitHub_Helper;

var dbConnection = new DatabaseConnection("GitHub_Helper");
using (var connection = dbConnection.GetConnection())
{
    connection.Open();
}

Console.ReadLine();