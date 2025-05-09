﻿using System;
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

        public static string CreateTablesCommandIfNotExist() => @"
        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))
        BEGIN
            CREATE TABLE Accounts (
                ID INT PRIMARY KEY IDENTITY(1,1),
                Username NVARCHAR(50) NOT NULL,
                Password NVARCHAR(50) NOT NULL,
                Email NVARCHAR(50) NOT NULL,
                CONSTRAINT UC_Account UNIQUE(Username, Email)
            );
        END
            
        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Games]') AND type in (N'U'))
        BEGIN
            CREATE TABLE Games
                    (
                        ID INT PRIMARY KEY IDENTITY(1,1),
                        Games NVARCHAR(50) NOT NULL,
                        MostPlayedGame NVARCHAR(50) NOT NULL
                    );
        END

        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GamesDetail]') AND type in (N'U'))
        BEGIN
             CREATE TABLE GamesDetail
                (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    GameID INT FOREIGN KEY REFERENCES Games(ID),
                    Game NVARCHAR(50) NOT NULL
                );
        END

        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
        BEGIN
             CREATE TABLE Settings
                (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(50) NOT NULL,
                    GameID INT FOREIGN KEY REFERENCES Games(ID)
                );
        END

        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountsSettings]') AND type in (N'U'))
        BEGIN
             CREATE TABLE AccountsSettings
                (
                    AccountID INT FOREIGN KEY REFERENCES Accounts(ID),
                    SettingID INT FOREIGN KEY REFERENCES Settings(ID) ,
                    PRIMARY KEY(AccountID, SettingID)
                );
        END";

        public static string DropTablesCommand()
            => @" DROP TABLE IF EXISTS AccountsSettings;
                  DROP TABLE IF EXISTS Settings; 
                  DROP TABLE IF EXISTS GamesDetail;
                  DROP TABLE IF EXISTS Games;
                  DROP TABLE IF EXISTS Accounts;";

        public static string InsertAccountCommand =>
            "INSERT INTO Accounts (Username, Password, Email) VALUES (@Username, @Password, @Email);";

        public static string GetAccountByEmailAndPasswordCommand =>
            "SELECT ID, Username, Password, Email FROM Accounts WHERE Email = @Email AND Password = @Password;";
    
        public static string GetAccountCommand =>
            "SELECT ID, Username, Password, Email FROM Accounts WHERE Email = @Email;";
    }
}
