using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class ValorAduanalItem
    {
        public Guid ValorAduanalItemId { get; set; }
        public Guid ValorAduanalId { get; set; }
        public Guid OrderId { get; set; }

        public Order Order { get; set; }
        public ValorAduanal ValorAduanal { get; set; }
    }
}
