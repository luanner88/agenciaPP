using System;
using System.Collections.Generic;

namespace AgenciappHome.Models
{
    public partial class Carrier
    {
        public Carrier()
        {
            Shipping = new HashSet<Shipping>();
        }

        public Guid CarrierId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public ICollection<Shipping> Shipping { get; set; }
    }
}
