using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class Shipping
    {
        public Shipping()
        {
            ShippingItem = new HashSet<ShippingItem>();
        }

        public Guid PackingId { get; set; }
        public Guid AgencyId { get; set; }
        public Guid OfficeId { get; set; }
        public Guid CarrierId { get; set; }

        public Agency Agency { get; set; }
        public Carrier Carrier { get; set; }
        public Office Office { get; set; }
        public ICollection<ShippingItem> ShippingItem { get; set; }
    }
}
