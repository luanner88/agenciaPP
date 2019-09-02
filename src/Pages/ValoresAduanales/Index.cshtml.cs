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
    public class IndexModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public IndexModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

        public IList<ValorAduanal> ValorAduanal { get;set; }

        public async Task OnGetAsync()
        {
            ValorAduanal = await _context.ValorAduanal.ToListAsync();
        }
    }
}
