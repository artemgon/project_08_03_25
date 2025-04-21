using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_08_03_25.DBCommands;
using Dapper;
using data_08_03_25.Models;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
using System.Windows.Controls;

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
            try
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(DbCommands.UseDbCommand("Games_Store_and_Launcher"));

                return await connection.ExecuteScalarAsync<int>(DbCommands.InsertAccountCommand,
                    new { account.Username, account.Password, account.Email });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error in CreateAccountAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<AccountModel?>> GetAccountByEmailAsync(string email, string password)
        {
            await using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryAsync<AccountModel>(
                    DbCommands.GetAccountByEmailAndPasswordCommand,
                    new { Email = email, Password = password }
                );

                Console.WriteLine($"Query executed for email: {email}");
                Console.WriteLine($"Results found: {result.Count()}");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }

        public async Task<int> GetAccountCountAsync()
        {
            await using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Accounts");
        }

        public async Task InitializeDatabaseAsync()
        {
            try
            {
                //await using var connection = new SqlConnection(_connectionString);
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(DbCommands.CreateDbCommandWithNotExists("Games_Store_and_Launcher"));
                    await connection.ExecuteAsync(DbCommands.UseDbCommand("Games_Store_and_Launcher"));
                    await connection.ExecuteAsync(DbCommands.CreateTablesCommandIfNotExist());
                }
               
            }
            catch (SqlException e)
            {
                throw new Exception($"Database initialization is failed\n\nDetails: {e}");
            }
        }

        public async Task ResetDatabaseAsync()
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(DbCommands.DropTablesCommand());
            await connection.ExecuteAsync(DbCommands.CreateTablesCommandIfNotExist());
        }
    }
}
