using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgenciappHome.Models;

namespace AgenciappHome.Controllers
{
    public class ValorAduanalsController : Controller
    {
        private readonly databaseContext _context;

        public ValorAduanalsController(databaseContext context)
        {
            _context = context;
        }

        // GET: ValorAduanals
        public async Task<IActionResult> Index()
        {
            return View(await _context.ValorAduanal.ToListAsync());
        }

        // GET: ValorAduanals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valorAduanal = await _context.ValorAduanal
                .FirstOrDefaultAsync(m => m.ValorAduanalId == id);
            if (valorAduanal == null)
            {
                return NotFound();
            }

            return View(valorAduanal);
        }

        // GET: ValorAduanals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ValorAduanals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ValorAduanalId,Name,Description,Um,Value,Article,Observaciones")] ValorAduanal valorAduanal)
        {
            if (ModelState.IsValid)
            {
                valorAduanal.ValorAduanalId = Guid.NewGuid();
                _context.Add(valorAduanal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(valorAduanal);
        }

        // GET: ValorAduanals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valorAduanal = await _context.ValorAduanal.FindAsync(id);
            if (valorAduanal == null)
            {
                return NotFound();
            }
            return View(valorAduanal);
        }

        // POST: ValorAduanals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ValorAduanalId,Name,Description,Um,Value,Article,Observaciones")] ValorAduanal valorAduanal)
        {
            if (id != valorAduanal.ValorAduanalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valorAduanal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValorAduanalExists(valorAduanal.ValorAduanalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(valorAduanal);
        }

        // GET: ValorAduanals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valorAduanal = await _context.ValorAduanal
                .FirstOrDefaultAsync(m => m.ValorAduanalId == id);
            if (valorAduanal == null)
            {
                return NotFound();
            }

            return View(valorAduanal);
        }

        // POST: ValorAduanals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var valorAduanal = await _context.ValorAduanal.FindAsync(id);
            _context.ValorAduanal.Remove(valorAduanal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValorAduanalExists(Guid id)
        {
            return _context.ValorAduanal.Any(e => e.ValorAduanalId == id);
        }
    }
}
