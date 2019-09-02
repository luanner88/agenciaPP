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
    public class CreateContactModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public CreateContactModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Contact Contact { get; set; }


        public IActionResult OnGet()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Name");
            ViewData["ContactId"] = new SelectList(_context.Contact, "ContactId", "Name");
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "TipoPagoId", "Type");
            ViewData["ProductId"] = new SelectList(_context.Product, "Code", "Description");
            ViewData["ClientName"] = new SelectList(_context.Client, "ClientId", "Name");
            ViewData["ClientLastName"] = new SelectList(_context.Client, "ClientId", "LastName");
            ViewData["ClientEmail"] = new SelectList(_context.Client, "ClientId", "Email");
            ViewData["Movil"] = new SelectList(_context.Phone.Where(x => x.Type == "Móvil"), "PhoneId", "Number");
            ViewData["PhoneCasa"] = new SelectList(_context.Phone.Where(x => x.Type == "Casa"), "PhoneId", "Number");
            ViewData["PhoneOficina"] = new SelectList(_context.Phone.Where(x => x.Type == "Oficina"), "PhoneId", "Number");
            ViewData["ContactLastName"] = new SelectList(_context.Contact, "ContactId", "LastName");
            ViewData["ContactEmail"] = new SelectList(_context.Contact, "ContactId", "Email");
            return Page();
        }

    
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
          //  return RedirectToPage("./IndexPackageItem", new { id = Contact.ContactId });
            return RedirectToPage("./Create", new { id = Contact.ContactId });

        }
    }
}