using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenciapp.Pages.Orders
{
    public class IndexPackageItemModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public IndexPackageItemModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<PackageItem> PackageItem { get;set; }

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
        public static Guid contact;
        public static bool nueva = true;
        public string tipoPago;
        public decimal cantlb;
        public decimal preciolb;
        public decimal otrosgastos;
        public decimal valor;


        [BindProperty]
        public List<string> listVA { get; set; }

        public async Task OnGetAsync(Guid id)
        {
    
 
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description");
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "TipoPagoId", "Type");

            ViewData["ValorAduanalId"] = new SelectList(_context.ValorAduanal, "ValorAduanalId", "Article") ;

            if (nueva)
            {
                order = new Order();
                CrearOrden(id);
                contact = id;
                nueva = false;
            }
            else
            {
                order = _context.Order.OrderBy(x => x.Date).First();
                contact = id;
               type= order.Type;
               cantlb= order.CantLb;
                preciolb= order.PriceLb;
                otrosgastos= order.OtrosCostos;
                valor= order.ValorPagado;
            }
           
            pac = _context.Package.Where(x => x.PackageNavigation == order).First();
            int cant = _context.PackageItem.Where(x => x.PackageId == pac.PackageId).ToList().Count();
            PackageItem = await _context.PackageItem.Where(x=>x.PackageId== pac.PackageId)
                .Include(p => p.Package)
                .Include(p => p.Product).ToListAsync();
        }
        public IActionResult OnPost()
        {
        
    
            PackageItem p= new PackageItem();
            p.PackageItemId= Guid.NewGuid();
            p.ProductId = Guid.Parse(producto);
            p.PackageId = pac.PackageId;
            p.Qty = cantidad;
            p.Description = desc;
            _context.PackageItem.Add(p);
            _context.SaveChanges();

            return RedirectToPage("./IndexPackageItem",contact);

      
        }
   
        public void CrearOrden(Guid contactId)
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
            order.TipoPago = _context.TipoPago.First();
            order.TipoPagoId = order.TipoPago.TipoPagoId;
            order.Type = "Misto";
            order.Number = "MX";
            order.Number += DateTime.Now.ToString("MMddyyyyHHmm");
            order.ContactId = contactId;
            order.ClientId = _context.Contact.Find(order.ContactId).ClientId;
            order.Amount = 0;
            order.ValorAduanal = 0;
            order.CantLb = 0;
            order.PriceLb = 0;
            order.OtrosCostos = 0;
            order.Balance = 0;
            order.ValorPagado = 0;
            order.Status = "Iniciada";
            _context.Add(order);
            _context.SaveChanges();

            //Crear un Pacquete
            Package package = new Package();
            package.PackageId = Guid.NewGuid();
            package.PackageNavigation = order;
            _context.Add(package);
            order.Package = package;
            _context.SaveChanges();
        }
        [HttpPost]
        public void EditarOrden()
        {
  

        order.Date = DateTime.Now.Date;
            order.TipoPago = _context.TipoPago.First();
            order.TipoPagoId = order.TipoPago.TipoPagoId;

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

            if (listVA.Count()>0)
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

            _context.SaveChangesAsync();
        }

    }
}
