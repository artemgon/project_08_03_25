using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core_08_03_25.Repository.AuthRepository;
using data_08_03_25.DBProvider;
using data_08_03_25.Models;

namespace data_08_03_25.Repository
{
    public class AuthRepositoryImpl : AuthRepository
    {
        private readonly DatabaseProvider? _databaseProvider;

        public AuthRepositoryImpl()
        { 
        }

        public AuthRepositoryImpl(DatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public override async Task<bool> RegisterAsync(string email, string password)
        {
            AccountModel account = new (email, password);
            var resultResponse = _databaseProvider?.CreateAccountAsync(account);
            return await Task.FromResult(resultResponse?.Result == 1);
        }

        public override Task<bool> LoginAsync(string email, string password)
        {
            var account = _databaseProvider?.GetAccountAsync(email);
            return Task.FromResult(account?.Result?.Password == password);
        }
    }
}
