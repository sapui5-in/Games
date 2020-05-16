using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.API.Model
{
    public class Address
    {
        public int Id { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string CountryCode { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
