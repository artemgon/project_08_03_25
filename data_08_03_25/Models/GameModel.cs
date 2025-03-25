using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_08_03_25.Abstractions;

namespace data_08_03_25.Models
{
    public class GameModel : IModel
    {
        public int ID { get; set; }
        public List<string>? Games { get; set; }
        public string? MostPlayedGame { get; set; }

        public GameModel()
        {
            Games = [];
        }

        public GameModel(int id, List<string> games)
        {
            ID = id;
            Games = games;
        }

        public GameModel(int id, List<string> games, string mostPlayedGame)
        {
            ID = id;
            Games = games;
            MostPlayedGame = mostPlayedGame;
        }
    }
}
