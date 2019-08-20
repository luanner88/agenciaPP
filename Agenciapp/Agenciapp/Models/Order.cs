using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class Order
    {
        public Guid OrderId { get; set; }
        public Guid AgencyId { get; set; }
        public Guid OfficeId { get; set; }
        public Guid ClientId { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public decimal PriceLb { get; set; }
        public decimal CantLb { get; set; }
        public Guid? ValorAduanalId { get; set; }

        public Agency Agency { get; set; }
        public Client Client { get; set; }
        public Office Office { get; set; }
        public ValorAduanal ValorAduanal { get; set; }
        public Package Package { get; set; }
    }
}
