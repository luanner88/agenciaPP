using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agenciapp.Models
{
    public partial class Order
    {
        public Order()
        {
            ValorAduanalItem = new HashSet<ValorAduanalItem>();
        }

        public Guid OrderId { get; set; }
        public Guid AgencyId { get; set; }
        public Guid OfficeId { get; set; }
        public Guid ClientId { get; set; }
        public string Type { get; set; }
        [Display(Name = "No. Envío")]
        public string Number { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Precio")]
        public decimal Amount { get; set; }
        [Display(Name = "Estado")]
        public string Status { get; set; }
        public decimal PriceLb { get; set; }
        public decimal CantLb { get; set; }
        public Guid TipoPagoId { get; set; }
        public Guid ContactId { get; set; }
        public Guid UserId { get; set; }
        public decimal ValorPagado { get; set; }
        public decimal Balance { get; set; }
        public decimal OtrosCostos { get; set; }
        [Display(Name = "Valor Aduanal")]
        public decimal ValorAduanal { get; set; }

        public Agency Agency { get; set; }
        [Display(Name = "Cliente")]
        public Client Client { get; set; }
        public Contact Contact { get; set; }
        public Office Office { get; set; }
        public TipoPago TipoPago { get; set; }
        public User User { get; set; }
        public Package Package { get; set; }
        [Display(Name = "Valores Aduanales")]
        public ICollection<ValorAduanalItem> ValorAduanalItem { get; set; }
    }
}
