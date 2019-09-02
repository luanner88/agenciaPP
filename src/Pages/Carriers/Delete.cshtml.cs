using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Pages.Carriers
{
    public class DeleteModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public DeleteModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Carrier Carrier { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Carrier = await _context.Carrier.FirstOrDefaultAsync(m => m.CarrierId == id);

            if (Carrier == null)
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

            Carrier = await _context.Carrier.FindAsync(id);

            if (Carrier != null)
            {
                _context.Carrier.Remove(Carrier);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
