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
    public class ClientsController : Controller
    {
        private readonly databaseContext _context;
       // private static Guid? idClient;

        public ClientsController(databaseContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Client.Include(c => c.Address).Include(c => c.Agency).Include(c => c.Phone);
          //  idClient = idClientaux;
            return View(await databaseContext.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.Address)
                .Include(c => c.Agency)
                .Include(c => c.Phone)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1");
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName");
            ViewData["PhoneId"] = new SelectList(_context.Phone, "PhoneId", "Number");


          
           return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,AgencyId,Name,LastName,CreatedAt,AddressId,PhoneId")] Client client, [Bind("PhoneId,ReferenceId,Type,Current,Number")] Phone phone, [Bind("AddressId,ReferenceId,Current,Type,AddressLine1,AddressLine2,City,State,Zip,Country,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] Address address)
        {
            if (ModelState.IsValid)
            {
                //Add Client
                client.ClientId = Guid.NewGuid();

                //Add Agency
                client.AgencyId = _context.Agency.First().AgencyId;
                client.CreatedAt = DateTime.Now;

                //add Phone
                Phone movil = new Phone();
                movil.PhoneId = Guid.NewGuid();
                movil.Number = phone.Number;
                movil.ReferenceId = client.ClientId;
                movil.Type = "Móvil";
                movil.Current = true;
                _context.Phone.Add(movil);
                client.Phone = movil;

                //add Addresss
                Address a = new Address();
                a.AddressId = Guid.NewGuid();
                a.ReferenceId = client.ClientId;
                a.Current = true;
                a.Type = "Oficina";
                a.AddressLine1 = address.AddressLine1;
                a.City = address.City;
                a.State = address.State;
                a.Country = "Usa";
                a.CreatedAt = DateTime.Now;
                a.CreatedBy = _context.User.First().UserId;
                a.UpdatedAt = DateTime.Now;
                a.UpdatedBy = _context.User.First().UserId;
                _context.Address.Add(a);
                client.Address = a;

                _context.Add(client);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", client.AddressId);
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", client.AgencyId);
            ViewData["PhoneId"] = new SelectList(_context.Phone, "PhoneId", "Number", client.PhoneId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", client.AddressId);
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", client.AgencyId);
            ViewData["PhoneId"] = new SelectList(_context.Phone, "PhoneId", "Number", client.PhoneId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ClientId,AgencyId,Name,LastName,CreatedAt,AddressId,PhoneId")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", client.AddressId);
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", client.AgencyId);
            ViewData["PhoneId"] = new SelectList(_context.Phone, "PhoneId", "Number", client.PhoneId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.Address)
                .Include(c => c.Agency)
                .Include(c => c.Phone)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(Guid id)
        {
            return _context.Client.Any(e => e.ClientId == id);
        }
    }
}
