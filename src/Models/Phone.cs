using System;
using System.Collections.Generic;

namespace AgenciappHome.Models
{
    public partial class Phone
    {
        public Guid PhoneId { get; set; }
        public Guid ReferenceId { get; set; }
        public string Type { get; set; }
        public bool Current { get; set; }
        public string Number { get; set; }
    }
}
