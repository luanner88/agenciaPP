using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class Contact
    {
        public Contact()
        {
            Order = new HashSet<Order>();
        }

        public Guid ContactId { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }

        public Client Client { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
