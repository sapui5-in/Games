using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.API.Model
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        // Unique
        public string Email { get; set; }

        // Unique
        public string Mobile { get; set; }

        public DateTime DOB { get; set; }

        public string ProfilePic { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public virtual ICollection<GameProgress> GameProgresses { get; set; }

        public virtual ICollection<GamePlayer> GamePlayers { get; set; }

        public virtual ICollection<GamePlayerPiecePosition> GamePlayerPiecePositions { get; set; }
    }
}