using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenciapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenciapp.Pages.Orders
{
    public class CreateOrderModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public CreateOrderModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<PackageItem> PackageItem { get; set; }
        private List<string> listProduct { get; set; }
        [BindProperty]
        public string producto { get; set; }
        [BindProperty]
        public int cantidad { get; set; }
        [BindProperty]
        public string desc { get; set; }
        [BindProperty]
        public string type { get; set; }
        public static Order order;
        public static Package pac;
        public Guid contactId;
        public static bool nueva = true;
        public string tipoPago;
        public int cantlb;
        public decimal preciolb;
        public decimal otrosgastos;
        public decimal valor;


        [BindProperty]
        public List<string> listVA { get; set; }
        public void OnGet(Guid id)
        {
            contactId = id;
        
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description");
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "TipoPagoId", "Type");
            ViewData["ValorAduanalId"] = new SelectList(_context.ValorAduanal, "ValorAduanalId", "Article");
            PackageItem = new List<PackageItem>();
    }
        public void CrearOrden()
        {
            order = new Order();
            order.OrderId = Guid.NewGuid();
            order.Agency = _context.Agency.First();
            order.AgencyId = order.Agency.AgencyId;
            order.Office = _context.Office.First();
            order.OfficeId = order.Office.OfficeId;
            order.User = _context.User.First();
            order.UserId = _context.User.First().UserId;
            order.Date = DateTime.Now.Date;

            //order.TipoPago = _context.TipoPago.First();
            //order.TipoPagoId = order.TipoPago.TipoPagoId;
            order.Type = type;
            if (type == "Mixto")
                order.Number = "MX";
            else if (type == "Paquete")
                order.Number = "PA";
            else if (type == "Alimentos")
                order.Number = "AL";
            else if (type == "Medicina")
                order.Number = "ME";
            else
                order.Number = "RE";
            order.Number += DateTime.Now.ToString("MMddyyyyHHmm");

            if (listVA.Count() > 0)
            {
                for (int i = 0; i < listVA.Count(); i++)
                {
                    ValorAduanalItem value = new ValorAduanalItem();
                    value.ValorAduanalItemId = Guid.NewGuid();
                    value.OrderId = order.OrderId;
                    value.Order = order;
                    value.ValorAduanal = _context.ValorAduanal.Where(x => x.ValorAduanalId == Guid.Parse(listVA[i])).First();
                    value.ValorAduanalId = Guid.Parse(listVA[i]);
                    order.ValorAduanal += value.ValorAduanal.Value;
                    order.ValorAduanalItem.Add(value);
                    _context.Add(value);
                }
            }
            


            order.CantLb = cantlb;
            order.PriceLb = preciolb;
            order.OtrosCostos = otrosgastos;
            order.ValorAduanal = valor;
            order.Amount = order.ValorAduanal + (order.CantLb * order.PriceLb) + order.OtrosCostos;
            order.Balance = order.Amount - order.ValorPagado;
            if (order.Balance != 0)//si el balance no es 0 el status es pendiente si el valanece es 0 iniciado, 
                order.Status = "Pendiente";
            else
                order.Status = "Iniciada";
            order.ContactId = contactId;
            order.ClientId = _context.Contact.Find(order.ContactId).ClientId;
            _context.Add(order);
            _context.SaveChanges();

            //Crear un Pacquete
            Package package = new Package();
            package.PackageId = Guid.NewGuid();
            package.PackageNavigation = order;
            _context.Add(package);

            for (int i = 0; i < listProduct.Count; i++)
            {
                PackageItem packageItem = new PackageItem();
                packageItem.PackageItemId = Guid.NewGuid();
                packageItem.PackageId = pac.PackageId;
                packageItem.Package = pac;
                packageItem.Product = _context.Product.Where(x => x.ProductId == Guid.Parse(listProduct[i])).First();
                packageItem.ProductId = packageItem.Product.ProductId;
                _context.Add(packageItem);
            }
            order.Package = package;
            _context.SaveChanges();
    }
        [HttpPost]
        public void InsertPackageItem([FromBody]List<PackageItem> productos)
        {
            int i = 0;
            //listProduct.Add(productos);
        }
    }
}