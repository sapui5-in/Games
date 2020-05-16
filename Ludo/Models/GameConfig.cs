using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.API.Model
{
    public class GameConfig
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<GameGameConfig> GameGameConfigs { get; set; }
    }
}
