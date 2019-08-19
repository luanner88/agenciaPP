using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class Agency
    {
        public Agency()
        {
            Client = new HashSet<Client>();
            Office = new HashSet<Office>();
            Order = new HashSet<Order>();
            Product = new HashSet<Product>();
            Shipping = new HashSet<Shipping>();
        }

        public Guid AgencyId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Client> Client { get; set; }
        public ICollection<Office> Office { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<Shipping> Shipping { get; set; }
    }
}
