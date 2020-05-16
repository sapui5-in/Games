using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.Services
{
    public class ServiceResponse
    {
        public Object Data { get; set; }
        public List<Message> Messages { get; set; }
    }
}
