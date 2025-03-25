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
            => $"CREATE DATABASE IF NOT EXISTS {dbName}";

        public static string CreateTablesCommand()
            => @"CREATE TABLE Games
            (
                ID INT PRIMARY KEY IDENTITY(1,1),
                MostPlayedGame NVARCHAR(20) NULL
            );
            
            CREATE TABLE GamesDetail
            (
                ID INT PRIMARY KEY IDENTITY(1,1),
                GameName NVARCHAR(20) NOT NULL,
                GameID INT FOREIGN KEY REFERENCES Games(ID)
            );
            
            CREATE TABLE Settings
            (
                ID INT PRIMARY KEY IDENTITY(1,1),
                Name NVARCHAR(30) NOT NULL,
                GameID INT FOREIGN KEY REFERENCES Games(ID)
            );

            CREATE TABLE Accounts
            (
                ID INT PRIMARY KEY IDENTITY(1,1),
                Username NVARCHAR(20) NOT NULL,
                Password NVARCHAR(20) NOT NULL,
                Email NVARCHAR(30) NOT NULL
            );
            
            CREATE TABLE AccountsSettings
            (
                AccountID INT FOREIGN KEY REFERENCES Accounts(ID),
                SettingID INT FOREIGN KEY REFERENCES Settings(ID)
                PRIMARY KEY(AccountID, SettingID)
            );";
          
        public static string DropTablesCommand()
            => @" DROP TABLE IF EXISTS AccountsSettings;
                  DROP TABLE IF EXISTS Accounts;
                  DROP TABLE IF EXISTS Settings;  
                  DROP TABLE IF EXISTS GamesDetail;
                  DROP TABLE IF EXISTS Games;";
    }
}
