using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using data_08_03_25.DBCommands;
using Dapper;

namespace data_08_03_25.DBProvider
{
    public class DatabaseProvider (string connectionString)
    {
        [Obsolete ("Obsolete")]
        public async Task InitializeDatabaseAsync()
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await connection.ExecuteAsync(DbCommands.CreateDbCommandWithNotExists("School"));
            await connection.ExecuteAsync(DbCommands.CreateTablesCommand());
        }

        [Obsolete("Obsolete")]

        public async Task ResetDatabaseAsync()
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await connection.ExecuteAsync(DbCommands.DropTablesCommand());
            await connection.ExecuteAsync(DbCommands.CreateTablesCommand());
        }
    }
}
