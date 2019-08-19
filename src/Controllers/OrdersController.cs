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
    public class OrdersController : Controller
    {
        private readonly databaseContext _context;

        public OrdersController(databaseContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Order.Include(o => o.Agency).Include(o => o.Client).Include(o => o.Office).Include(o => o.ValorAduanal);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Agency)
                .Include(o => o.Client)
                .Include(o => o.Office)
                .Include(o => o.ValorAduanal)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName");
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "LastName");
            ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name");
            ViewData["ValorAduanalId"] = new SelectList(_context.ValorAduanal, "ValorAduanalId", "Article");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,AgencyId,OfficeId,ClientId,Type,Number,Date,Amount,Status,PriceLb,CantLb,ValorAduanalId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderId = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", order.AgencyId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "LastName", order.ClientId);
            ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name", order.OfficeId);
            ViewData["ValorAduanalId"] = new SelectList(_context.ValorAduanal, "ValorAduanalId", "Article", order.ValorAduanalId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", order.AgencyId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "LastName", order.ClientId);
            ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name", order.OfficeId);
            ViewData["ValorAduanalId"] = new SelectList(_context.ValorAduanal, "ValorAduanalId", "Article", order.ValorAduanalId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,AgencyId,OfficeId,ClientId,Type,Number,Date,Amount,Status,PriceLb,CantLb,ValorAduanalId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", order.AgencyId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "LastName", order.ClientId);
            ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name", order.OfficeId);
            ViewData["ValorAduanalId"] = new SelectList(_context.ValorAduanal, "ValorAduanalId", "Article", order.ValorAduanalId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Agency)
                .Include(o => o.Client)
                .Include(o => o.Office)
                .Include(o => o.ValorAduanal)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
