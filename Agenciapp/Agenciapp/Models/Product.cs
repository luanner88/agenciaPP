using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Código")]
        public string Code { get; set; }
        [Display(Name = "Producto")]
        public string Description { get; set; }
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
        [Display(Name = "Talla/Marca")]
        public string TallaMarca { get; set; }

        public Agency Agency { get; set; }
        public ICollection<PackageItem> PackageItem { get; set; }
        public ICollection<ShippingItem> ShippingItem { get; set; }
    }
}
