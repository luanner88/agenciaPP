using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;

namespace Agenciapp.Controllers
{
    public class OfficesController : Controller
    {
        private readonly databaseContext _context;

        public OfficesController(databaseContext context)
        {
            _context = context;
        }

        // GET: Offices
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Office.Include(o => o.Agency);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Offices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _context.Office
                .Include(o => o.Agency)
                .FirstOrDefaultAsync(m => m.OfficeId == id);
            if (office == null)
            {
                return NotFound();
            }

            return View(office);
        }

        // GET: Offices/Create
        public IActionResult Create()
        {
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName");
            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfficeId,AgencyId,Name")] Office office)
        {
            Agency agency = _context.Agency.First();
            if (ModelState.IsValid)
            {
                office.OfficeId = Guid.NewGuid();
                office.Agency = agency;
                office.AgencyId = agency.AgencyId;
                _context.Add(office);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", office.AgencyId);
            return View(office);
        }

        // GET: Offices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _context.Office.FindAsync(id);
            if (office == null)
            {
                return NotFound();
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", office.AgencyId);
            return View(office);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OfficeId,AgencyId,Name")] Office office)
        {
            if (id != office.OfficeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(office);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficeExists(office.OfficeId))
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
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", office.AgencyId);
            return View(office);
        }

        // GET: Offices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _context.Office
                .Include(o => o.Agency)
                .FirstOrDefaultAsync(m => m.OfficeId == id);
            if (office == null)
            {
                return NotFound();
            }

            return View(office);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var office = await _context.Office.FindAsync(id);
            _context.Office.Remove(office);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficeExists(Guid id)
        {
            return _context.Office.Any(e => e.OfficeId == id);
        }
    }
}
