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
    public class ContactsController : Controller
    {
        private readonly databaseContext _context;

        public ContactsController(databaseContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Contact.Include(c => c.Address).Include(c => c.Client).Include(c => c.Phone);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Address)
                .Include(c => c.Client)
                .Include(c => c.Phone)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1");
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "LastName");
            ViewData["PhoneId"] = new SelectList(_context.Phone, "PhoneId", "Number");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,ClientId,Name,LastName,CreatedAt,AddressId,PhoneId")] Contact contact, [Bind("PhoneId,ReferenceId,Type,Current,Number")] Phone phone, [Bind("AddressId,ReferenceId,Current,Type,AddressLine1,AddressLine2,City,State,Zip,Country,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] Address address)
        {
            if (ModelState.IsValid)
            {
                contact.ContactId = Guid.NewGuid();

                //Add Agency
                contact.Client = _context.Client.First();//Pasar cliente
                contact.CreatedAt = DateTime.Now;

                //add Phone
                Phone movil = new Phone();
                movil.PhoneId = Guid.NewGuid();
                movil.Number = phone.Number;
                movil.ReferenceId = contact.ClientId;
                movil.Type = "Móvil";
                movil.Current = true;
                _context.Phone.Add(movil);
                contact.Phone = movil;

                //add Addresss
                Address a = new Address();
                a.AddressId = Guid.NewGuid();
                a.ReferenceId = contact.ClientId;
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
                contact.Address = a;

                _context.Add(contact);


                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", contact.AddressId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "LastName", contact.ClientId);
            ViewData["PhoneId"] = new SelectList(_context.Phone, "PhoneId", "Number", contact.PhoneId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", contact.AddressId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "LastName", contact.ClientId);
            ViewData["PhoneId"] = new SelectList(_context.Phone, "PhoneId", "Number", contact.PhoneId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ContactId,ClientId,Name,LastName,CreatedAt,AddressId,PhoneId")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
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
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", contact.AddressId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "LastName", contact.ClientId);
            ViewData["PhoneId"] = new SelectList(_context.Phone, "PhoneId", "Number", contact.PhoneId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Address)
                .Include(c => c.Client)
                .Include(c => c.Phone)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contact = await _context.Contact.FindAsync(id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(Guid id)
        {
            return _context.Contact.Any(e => e.ContactId == id);
        }
    }
}
