﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Pages.ValoresAduanales
{
    public class DetailsModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public DetailsModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

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
    }
}
