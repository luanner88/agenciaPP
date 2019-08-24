using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgenciappHome.Models
{
    public partial class ClientPhone
    {
        //public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Phone Phone { get; set; }

        
    }
}
