using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Pages.ValoresAduanales
{
    public class EditModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public EditModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ValorAduanal ValorAduanal { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ValorAduanal = await _context.ValorAduanal.FirstOrDefaultAsync(m => m.ValorAduanalId == id);

            if (ValorAduanal == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ValorAduanal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValorAduanalExists(ValorAduanal.ValorAduanalId))
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

        private bool ValorAduanalExists(Guid id)
        {
            return _context.ValorAduanal.Any(e => e.ValorAduanalId == id);
        }
    }
}
