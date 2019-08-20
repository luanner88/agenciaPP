using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class ValorAduanal
    {
        public ValorAduanal()
        {
            Order = new HashSet<Order>();
        }

        public Guid ValorAduanalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Um { get; set; }
        public decimal Value { get; set; }
        public string Article { get; set; }
        public string Observaciones { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
