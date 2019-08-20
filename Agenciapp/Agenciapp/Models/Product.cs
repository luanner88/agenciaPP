﻿using System;
using System.Collections.Generic;

namespace Agenciapp.Models
{
    public partial class Product
    {
        public Product()
        {
            PackageItem = new HashSet<PackageItem>();
            ShippingItem = new HashSet<ShippingItem>();
        }

        public Guid ProductId { get; set; }
        public Guid AgencyId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Agency Agency { get; set; }
        public ICollection<PackageItem> PackageItem { get; set; }
        public ICollection<ShippingItem> ShippingItem { get; set; }
    }
}