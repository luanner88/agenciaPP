using System;
using System.Collections.Generic;

namespace AgenciappHome.Models
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string SecureCode { get; set; }
        public DateTime? ExpiresSecureCode { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public Guid AccountId { get; set; }
        public DateTime Timestamp { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
