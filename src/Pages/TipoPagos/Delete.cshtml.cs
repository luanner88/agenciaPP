﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Pages.TipoPagos
{
    public class DeleteModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public DeleteModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TipoPago TipoPago { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TipoPago = await _context.TipoPago.FirstOrDefaultAsync(m => m.TipoPagoId == id);

            if (TipoPago == null)
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

            TipoPago = await _context.TipoPago.FindAsync(id);

            if (TipoPago != null)
            {
                _context.TipoPago.Remove(TipoPago);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
