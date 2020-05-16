using System.Collections.Generic;

namespace Ludo.API.Model
{
    public class GameStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
