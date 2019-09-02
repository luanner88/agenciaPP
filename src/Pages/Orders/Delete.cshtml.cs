using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public DeleteModel(Agenciapp.Models.databaseContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order.FindAsync(id);

            if (Order != null)
            {
                foreach (var package in _context.Package.Where(x => x.PackageNavigation.OrderId == Order.OrderId))
                {
                    foreach (var item in _context.PackageItem.Where(x => x.PackageId == package.PackageId))
                    {
                        _context.PackageItem.Remove(item);
                    }
                    _context.Package.Remove(package);
                }

                foreach (var va in _context.ValorAduanalItem.Where(x => x.OrderId == Order.OrderId))
                {
                    _context.ValorAduanalItem.Remove(va);
                }
                _context.Order.Remove(Order);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("././Index");
        }
    }
}
