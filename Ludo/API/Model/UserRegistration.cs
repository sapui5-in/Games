using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.API.Model
{
    public class UserRegistration
    {
        public int Id { get; set; }

        public int CreationDate { get; set; }

        public string Password { get; set; }
    }
}