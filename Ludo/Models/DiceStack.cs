using Ludo.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.Models
{
    public class DiceStack
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int DiceValue1 { get; set; }

        public int DiceValue2 { get; set; }

        public int DiceValue3 { get; set; }
    }
}
