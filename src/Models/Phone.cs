using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class Phone
    {
        public Phone()
        {
            Client = new HashSet<Client>();
            Contact = new HashSet<Contact>();
        }

        public Guid PhoneId { get; set; }
        public Guid ReferenceId { get; set; }
        public string Type { get; set; }
        public bool Current { get; set; }
        public string Number { get; set; }

        public ICollection<Client> Client { get; set; }
        public ICollection<Contact> Contact { get; set; }
    }
}
