using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class Contact
    {
        public Guid ContactId { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AddressId { get; set; }
        public Guid PhoneId { get; set; }

        public Address Address { get; set; }
        public Client Client { get; set; }
        public Phone Phone { get; set; }
    }
}
