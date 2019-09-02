using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Pages.Offices
{
    public class DetailsModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public DetailsModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
