using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using data_08_03_25.DBCommands;
using Dapper;
using data_08_03_25.Models;
using System.Reflection.Metadata;

namespace data_08_03_25.DBProvider
{
    public class DatabaseProvider
    {
        private readonly string? _connectionString;

        public DatabaseProvider()
        {
        }

        public DatabaseProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAccountAsync(AccountModel account)
        {
            await using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(DbCommands.InsertAccountCommand,
                new { account.Username, account.Password, account.Email });
        }

        public async Task<AccountModel?> GetAccountAsync(string Username)
        {
            await using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<AccountModel>(DbCommands.GetAccountCommand, new { Username });
        }

        public async Task InitializeDatabaseAsync()
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(DbCommands.CreateDbCommandWithNotExists("Games Store and Launcher"));
            await connection.ExecuteAsync(DbCommands.UseDbCommand("Games Store and Launcher"));
            await connection.ExecuteAsync(DbCommands.CreateTablesCommand());
        }

        public async Task ResetDatabaseAsync()
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(DbCommands.DropTablesCommand());
            await connection.ExecuteAsync(DbCommands.CreateTablesCommand());
        }
    }
}
