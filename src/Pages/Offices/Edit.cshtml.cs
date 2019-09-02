using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Pages.Offices
{
    public class EditModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public EditModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Office Office { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Office = await _context.Office
                .Include(o => o.Agency).FirstOrDefaultAsync(m => m.OfficeId == id);

            if (Office == null)
            {
                return NotFound();
            }
           ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Office).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeExists(Office.OfficeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OfficeExists(Guid id)
        {
            return _context.Office.Any(e => e.OfficeId == id);
        }
    }
}
