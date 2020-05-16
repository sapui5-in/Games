using Ludo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.API.Model
{
    public class Game
    {
        public int Id { get; set; }

        public string GameType { get; set; }

        public int GameStatusId { get; set; }

        public virtual GameStatus GameStatus { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime StartDateTime { get; set; }

        public virtual ICollection<GameProgress> GameProgresses { get; set; }

        public virtual ICollection<GamePlayer> GamePlayers { get; set; }

        public virtual ICollection<GamePlayerPiecePosition> GamePlayerPiecePositions { get; set; }

        public virtual ICollection<GameGameConfig> GameGameConfigs { get; set; }

        public virtual ICollection<DiceStack> DiceStacks { get; set; }
    }
}
