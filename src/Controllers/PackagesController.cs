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
    public class PackagesController : Controller
    {
        private readonly databaseContext _context;

        public PackagesController(databaseContext context)
        {
            _context = context;
        }

        // GET: Packages
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Package.Include(p => p.PackageNavigation);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Packages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Package
                .Include(p => p.PackageNavigation)
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // GET: Packages/Create
        public IActionResult Create()
        {
            ViewData["PackageId"] = new SelectList(_context.Order, "OrderId", "Number");
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackageId")] Package package)
        {
            if (ModelState.IsValid)
            {
                package.PackageId = Guid.NewGuid();
                _context.Add(package);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PackageId"] = new SelectList(_context.Order, "OrderId", "Number", package.PackageId);
            return View(package);
        }

        // GET: Packages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            ViewData["PackageId"] = new SelectList(_context.Order, "OrderId", "Number", package.PackageId);
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PackageId")] Package package)
        {
            if (id != package.PackageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(package);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.PackageId))
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
            ViewData["PackageId"] = new SelectList(_context.Order, "OrderId", "Number", package.PackageId);
            return View(package);
        }

        // GET: Packages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Package
                .Include(p => p.PackageNavigation)
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var package = await _context.Package.FindAsync(id);
            _context.Package.Remove(package);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(Guid id)
        {
            return _context.Package.Any(e => e.PackageId == id);
        }
    }
}
