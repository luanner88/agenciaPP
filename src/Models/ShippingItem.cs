using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class ShippingItem
    {
        public Guid ShippingItemId { get; set; }
        public Guid PackingId { get; set; }
        public Guid PackageId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Qty { get; set; }

        public Package Package { get; set; }
        public Shipping Packing { get; set; }
        public Product Product { get; set; }
    }
}
