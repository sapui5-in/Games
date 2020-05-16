using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.Services
{
    public enum MessageType
    {
        E,
        W,
        I,
        S
    }

    public class Message
    {
        public int Code { get; set; }
        public MessageType Type { get; set; }
        public string Msg { get; set; }
    }
}
