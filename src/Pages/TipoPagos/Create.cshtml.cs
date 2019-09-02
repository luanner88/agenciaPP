using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Agenciapp.Models;

namespace Agenciapp.Pages.TipoPagos
{
    public class CreateModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public CreateModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TipoPago TipoPago { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TipoPago.Add(TipoPago);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}