using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class Address
    {
        public Address()
        {
            Client = new HashSet<Client>();
            Contact = new HashSet<Contact>();
        }

        public Guid AddressId { get; set; }
        public Guid ReferenceId { get; set; }
        public bool Current { get; set; }
        public string Type { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        public ICollection<Client> Client { get; set; }
        public ICollection<Contact> Contact { get; set; }
    }
}
