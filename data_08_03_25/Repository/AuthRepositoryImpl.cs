using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core_08_03_25.Entity;
using core_08_03_25.Repository.AuthRepository;
using data_08_03_25.DBProvider;
using data_08_03_25.Models;
using Windows.System;

namespace data_08_03_25.Repository
{
    public class AuthRepositoryImpl : AuthRepository<AccountModel?>
    {
        private DatabaseProvider? _dbProvider;

        public AuthRepositoryImpl()
        { 
        }

        public AuthRepositoryImpl(DatabaseProvider databaseProvider)
        {
            _dbProvider = databaseProvider;
        }

        public override async Task<bool> RegisterAsync(string email, string password)
        {
            if (_dbProvider == null)
            {
                throw new InvalidOperationException("_dbProvider is not initialized.");
            }

            try
            {
                string username = email.Split('@')[0];

                AccountModel account = new()
                {
                    Username = username,
                    Email = email,
                    Password = password
                };

                var resultResponse = await _dbProvider.CreateAccountAsync(account);
                Console.WriteLine($"Registration result: {resultResponse}"); 
                return resultResponse > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration failed: {ex.Message}");
                throw;
            }
        }


        public override async Task<AccountModel?> LoginAsync(string email, string password)
        {
            if (_dbProvider == null)
            {
                throw new InvalidOperationException("_dbProvider is not initialized.");
            }

            try
            {
                var accounts = await _dbProvider.GetAccountByEmailAsync(email, password);

                Console.WriteLine($"Login attempt for email: {email}");

                return accounts.FirstOrDefault(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                throw;
            }
        }

    }
}
