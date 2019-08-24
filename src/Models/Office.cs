using System;
using System.Collections.Generic;

namespace AgenciappHome.Models
{
    public partial class Office
    {
        public Office()
        {
            Order = new HashSet<Order>();
            Shipping = new HashSet<Shipping>();
        }

        public Guid OfficeId { get; set; }
        public Guid AgencyId { get; set; }
        public string Name { get; set; }

        public Agency Agency { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Shipping> Shipping { get; set; }
    }
}
