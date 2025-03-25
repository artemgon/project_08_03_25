using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_08_03_25.Abstractions;

namespace data_08_03_25.Models
{
    public class SettingsModel : IModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        public SettingsModel()
        {
        }

        public SettingsModel(string name)
        {
            Name = name;
        }

        public SettingsModel(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
