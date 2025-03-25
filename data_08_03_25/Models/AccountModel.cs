using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_08_03_25.Abstractions;

namespace data_08_03_25.Models
{
    public class AccountModel : IModel
    {
        public int ID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public List<SettingsModel>? Settings { get; set; }

        public AccountModel()
        {
            Settings = [];
        }

        public AccountModel(int iD, string? username, string? password, string? email)
        {
            ID = iD;
            Username = username;
            Password = password;
            Email = email;
            Settings = [];
        }

        public AccountModel(int iD, string? username, string? password, string? email, List<SettingsModel>? settings)
        {
            ID = iD;
            Username = username;
            Password = password;
            Email = email;
            Settings = settings;
        }
    }
}
