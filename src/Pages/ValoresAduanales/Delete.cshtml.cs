using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Pages.ValoresAduanales
{
    public class DeleteModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public DeleteModel(Agenciapp.Models.databaseContext context)
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ValorAduanal = await _context.ValorAduanal.FindAsync(id);

            if (ValorAduanal != null)
            {
                _context.ValorAduanal.Remove(ValorAduanal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
