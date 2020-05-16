using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.API.Model
{
    public class GameGameConfig
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int GameConfigId { get; set; }

        public virtual GameConfig GameConfig { get; set; }

        public string GameConfigValue { get; set; }
    }
}
