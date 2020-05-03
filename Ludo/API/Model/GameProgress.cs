using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.API.Model
{
    public class GameProgress
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int CurrentPlayerId { get; set; }

        public virtual User CurrentPlayer { get; set; }

        public bool CurrentPlayerDiceRolled { get; set; }   // Whether Current Player has rolled the Dice

        public int LastDiceValue { get; set; }

        public DateTime LastActionDateTime { get; set; }

    }
}
