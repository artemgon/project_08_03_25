using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_08_03_25.DBCommands
{
    public class DbCommands
    {
        public static string CreateDbCommandWithNotExists(string dbName)
            => $"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{dbName}') CREATE DATABASE {dbName};";

        public static string UseDbCommand(string dbName)
            => $"USE {dbName};";

        public static string CreateTablesCommand()
            => @"CREATE TABLE Accounts
                (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    Username NVARCHAR(50) NOT NULL,
                    Password NVARCHAR(50) NOT NULL,
                    Email NVARCHAR(50) NOT NULL,
                    CONSTRAINT UC_Account UNIQUE(Username, Email)
                );
                
                CREATE TABLE Games
                (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    Games NVARCHAR(50) NOT NULL,
                    MostPlayedGame NVARCHAR(50) NOT NULL
                );

                CREATE TABLE GamesDetail
                (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    GameID INT FOREIGN KEY REFERENCES Games(ID),
                    Game NVARCHAR(50) NOT NULL
                );

                CREATE TABLE Settings
                (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(50) NOT NULL,
                    GameID INT FOREIGN KEY REFERENCES Games(ID)
                );

                CREATE TABLE AccountsSettings
                (
                    AccountID INT FOREIGN KEY REFERENCES Accounts(ID),
                    SettingID INT FOREIGN KEY REFERENCES Settings(ID) ,
                    PRIMARY KEY(AccountID, SettingID)
                );
            ";
          
        public static string DropTablesCommand()
            => @" DROP TABLE IF EXISTS AccountsSettings;
                  DROP TABLE IF EXISTS Settings; 
                  DROP TABLE IF EXISTS GamesDetail;
                  DROP TABLE IF EXISTS Games;
                  DROP TABLE IF EXISTS Accounts;";

        public static string InsertAccountCommand =>
            "INSERT INTO Accounts (Username, Password, Email) VALUES (@Username, @Password, @Email);";

        public static string GetAccountByLoginCommand =>
            "SELECT ID, Username, Password, Email FROM Accounts WHERE Username = @Username AND Password = @Password;";
    
        public static string GetAccountCommand =>
            "SELECT ID, Username, Password, Email FROM Accounts WHERE Username = @Username;";
    }
}
