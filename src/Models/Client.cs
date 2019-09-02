using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class Client
    {
        public Client()
        {
            Contact = new HashSet<Contact>();
            Order = new HashSet<Order>();
        }

        public Guid ClientId { get; set; }
        public Guid AgencyId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }

        public Agency Agency { get; set; }
        public ICollection<Contact> Contact { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
