using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public EditModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order
                .Include(o => o.Agency)
                .Include(o => o.Client)
                .Include(o => o.Contact)
                .Include(o => o.Office)
                .Include(o => o.TipoPago)
                .Include(o => o.User).FirstOrDefaultAsync(m => m.OrderId == id);

            if (Order == null)
            {
                return NotFound();
            }
           ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName");
           ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email");
           ViewData["ContactId"] = new SelectList(_context.Contact, "ContactId", "LastName");
           ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name");
           ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "TipoPagoId", "Type");
           ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("././Index");
        }

        private bool OrderExists(Guid id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
