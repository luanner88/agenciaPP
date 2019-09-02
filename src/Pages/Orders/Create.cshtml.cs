using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Agenciapp.Models;

namespace Agenciapp.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public CreateModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

  
        public static Guid contactId;
        public IActionResult OnGet(Guid id)
        {
        ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName");
        ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email");
        ViewData["ContactId"] = new SelectList(_context.Contact, "ContactId", "LastName");
        ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name");
        ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "TipoPagoId", "Type");
        ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email");
        ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description");
        contactId = id;
        PackageItem = new List<PackageItem>();
            
        return Page();
        }
        [BindProperty]
        public Order Order { get; set; }
        [BindProperty]
        public static Package pac { get; set; }
        [BindProperty]
        public string producto { get; set; }
        [BindProperty]
        public List<PackageItem> PackageItem { get; set; }
        [BindProperty]
        private List<string> listProduct { get; set; }
        [BindProperty]
        public List<string> listVA { get; set; }
        static Guid orderId;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Order.OrderId = Guid.NewGuid();
            Order.Agency = _context.Agency.First();
            Order.AgencyId = Order.Agency.AgencyId;
            Order.Office = _context.Office.First();
            Order.OfficeId = Order.Office.OfficeId;
            Order.User = _context.User.First();
            Order.UserId = _context.User.First().UserId;
            Order.Date = DateTime.Now.Date;

            Order.TipoPago = _context.TipoPago.First();
            Order.TipoPagoId = Order.TipoPago.TipoPagoId;
            if (Order.Type == "Mixto")
                Order.Number = "MX";
            else if (Order.Type == "Paquete")
                Order.Number = "PA";
            else if (Order.Type == "Alimentos")
                Order.Number = "AL";
            else if (Order.Type == "Medicina")
                Order.Number = "ME";
            else
                Order.Number = "RE";
            Order.Number += DateTime.Now.ToString("MMddyyyyHHmm");

            if (listVA.Count() > 0)
            {
                for (int i = 0; i < listVA.Count(); i++)
                {
                    ValorAduanalItem value = new ValorAduanalItem();
                    value.ValorAduanalItemId = Guid.NewGuid();
                    value.OrderId = Order.OrderId;
                    value.Order = Order;
                    value.ValorAduanal = _context.ValorAduanal.Where(x => x.ValorAduanalId == Guid.Parse(listVA[i])).First();
                    value.ValorAduanalId = Guid.Parse(listVA[i]);
                    Order.ValorAduanal += value.ValorAduanal.Value;
                    Order.ValorAduanalItem.Add(value);
                    _context.Add(value);
                }
            }

            Order.Amount = Order.ValorAduanal + (Order.CantLb * Order.PriceLb) + Order.OtrosCostos;
            Order.Balance = Order.Amount - Order.ValorPagado;
            if (Order.Balance != 0)//si el balance no es 0 el status es pendiente si el valanece es 0 iniciado, 
                Order.Status = "Pendiente";
            else
                Order.Status = "Iniciada";
            Order.ContactId = contactId;
            Order.ClientId = _context.Contact.Find(Order.ContactId).ClientId;
            _context.Add(Order);
          

            //Crear un Pacquete
            Package package = new Package();
            package.PackageId = Guid.NewGuid();
            package.PackageNavigation = Order;
            _context.Add(package);

            if (PackageItem.Count>0)
            {
                for (int i = 0; i < listProduct.Count; i++)
                {
                    PackageItem packageItem = new PackageItem();
                    packageItem.PackageItemId = Guid.NewGuid();
                    packageItem.PackageId = pac.PackageId;
                    packageItem.Package = pac;
                    packageItem.Product = _context.Product.Where(x => x.ProductId == Guid.Parse(listProduct[i])).First();
                    packageItem.ProductId = packageItem.Product.ProductId;
                    _context.PackageItem.Add(packageItem);
                }
            }
            
            Order.Package = package;
            _context.SaveChanges();

            return RedirectToPage("/Index");
        }


        public void CrearOrden()
        {
           
        }
        [HttpPost]
        public void InsertPackageItem([FromBody]List<PackageItem> productos)
        {
            int i = 0;
            //listProduct.Add(productos);
        }

    }
}