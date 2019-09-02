using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agenciapp.Models
{
    public partial class PackageItem
    {
        public Guid PackageItemId { get; set; }
        public Guid PackageId { get; set; }
        public Guid ProductId { get; set; }
        [Display(Name= "Cantidad")]
        public decimal Qty { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public Package Package { get; set; }
        [Display(Name = "Producto")]
        public Product Product { get; set; }
    }
}
