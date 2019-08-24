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
    public class AgenciesController : Controller
    {
        private readonly databaseContext _context;

        public AgenciesController(databaseContext context)
        {
            _context = context;
        }

        // GET: Agencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agency.ToListAsync());
        }

        // GET: Agencies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agency = await _context.Agency
                .FirstOrDefaultAsync(m => m.AgencyId == id);
            if (agency == null)
            {
                return NotFound();
            }

            return View(agency);
        }

        // GET: Agencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgencyId,Type,Name,LegalName,CreatedAt")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                agency.AgencyId = Guid.NewGuid();
                agency.CreatedAt = DateTime.Now;
                _context.Add(agency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agency);
        }

        // GET: Agencies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agency = await _context.Agency.FindAsync(id);
            if (agency == null)
            {
                return NotFound();
            }
            return View(agency);
        }

        // POST: Agencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AgencyId,Type,Name,LegalName,CreatedAt")] Agency agency)
        {
            if (id != agency.AgencyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgencyExists(agency.AgencyId))
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
            return View(agency);
        }

        // GET: Agencies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agency = await _context.Agency
                .FirstOrDefaultAsync(m => m.AgencyId == id);
            if (agency == null)
            {
                return NotFound();
            }

            return View(agency);
        }

        // POST: Agencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var agency = await _context.Agency.FindAsync(id);
            _context.Agency.Remove(agency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgencyExists(Guid id)
        {
            return _context.Agency.Any(e => e.AgencyId == id);
        }
    }
}
