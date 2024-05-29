using Xunit;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using GitHub_Helper;

namespace GitHubHelper.Tests
{
    public class DatabaseConnectionTests : IDisposable
    {
        private readonly SqliteConnection _testConnection;

        public DatabaseConnectionTests()
        {
            // Inicjalizacja bazy danych w pamięci do celów testowych
            _testConnection = new SqliteConnection("Data Source=:memory:");
            _testConnection.Open();

            // Tworzenie tabel do testów
            using var createTableCommand = _testConnection.CreateCommand();
            createTableCommand.CommandText = @"
                CREATE TABLE KomendyGit (
                    Id INTEGER PRIMARY KEY,
                    Komenda TEXT NOT NULL,
                    OpisKomendy TEXT NOT NULL,
                    Skladnia TEXT,
                    Opis TEXT
                );
            ";
            createTableCommand.ExecuteNonQuery();
        }

        [Fact]
        public void ExecuteQueriesAndReturnResults_ReturnsCorrectData()
        {
            // Arrange
            using var insertCommand = _testConnection.CreateCommand();
            insertCommand.CommandText = @"
                INSERT INTO KomendyGit (Komenda, OpisKomendy, Skladnia, Opis)
                VALUES ('git init', 'Initialize a new git repository', 'git init', 'Initializes a new git repository.');
            ";
            insertCommand.ExecuteNonQuery();

            var dbConnection = new DatabaseConnection(_testConnection);

            // Act
            var results = dbConnection.ExecuteQueriesAndReturnResults("SELECT * FROM KomendyGit");

            // Assert
            Assert.Single(results);
            var result = results.First();
            Assert.NotNull(result);
            Assert.True(result.ContainsKey("1"));
            var command = result["1"];
            Assert.Equal("git init", command.Item1);
            Assert.Equal("Initialize a new git repository", command.Item2);
            Assert.Equal("git init", command.Item3);
            Assert.Equal("Initializes a new git repository.", command.Item4);
        }

        [Fact]
        public void ExecuteQueriesAndReturnResults_ReturnsEmptyForEmptyTable()
        {
            // Arrange
            var dbConnection = new DatabaseConnection(_testConnection);

            // Act
            var results = dbConnection.ExecuteQueriesAndReturnResults("SELECT * FROM KomendyGit");

            // Assert
            Assert.Single(results);
            var result = results.First();
            Assert.Empty(result);
        }

        [Fact]
        public void ExecuteQueriesAndReturnResults_HandlesSqlErrorGracefully()
        {
            // Arrange
            var dbConnection = new DatabaseConnection(_testConnection);

            // Act
            var results = dbConnection.ExecuteQueriesAndReturnResults("SELECT * FROM NonExistentTable");

            // Assert
            Assert.Single(results);
            var result = results.First();
            Assert.Empty(result);
        }

        [Fact]
        public void ExecuteQueriesAndReturnResults_ExecutesMultipleQueries()
        {
            // Arrange
            using var insertCommand = _testConnection.CreateCommand();
            insertCommand.CommandText = @"
                INSERT INTO KomendyGit (Komenda, OpisKomendy, Skladnia, Opis)
                VALUES ('git init', 'Initialize a new git repository', 'git init', 'Initializes a new git repository.'),
                       ('git add', 'Add file contents to the index', 'git add <file>', 'Adds the specified files to the staging area.');
            ";
            insertCommand.ExecuteNonQuery();

            var dbConnection = new DatabaseConnection(_testConnection);

            // Act
            var results = dbConnection.ExecuteQueriesAndReturnResults(
                "SELECT * FROM KomendyGit WHERE Komenda = 'git init'",
                "SELECT * FROM KomendyGit WHERE Komenda = 'git add'"
            );

            // Assert
            Assert.Equal(2, results.Count);

            var initCommand = results[0];
            Assert.NotNull(initCommand);
            Assert.True(initCommand.ContainsKey("1"));
            var init = initCommand["1"];
            Assert.Equal("git init", init.Item1);
            Assert.Equal("Initialize a new git repository", init.Item2);
            Assert.Equal("git init", init.Item3);
            Assert.Equal("Initializes a new git repository.", init.Item4);

            var addCommand = results[1];
            Assert.NotNull(addCommand);
            Assert.True(addCommand.ContainsKey("2"));
            var add = addCommand["2"];
            Assert.Equal("git add", add.Item1);
            Assert.Equal("Add file contents to the index", add.Item2);
            Assert.Equal("git add <file>", add.Item3);
            Assert.Equal("Adds the specified files to the staging area.", add.Item4);
        }

        public void Dispose()
        {
            _testConnection?.Dispose();
        }
    }
}
