using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;
using Newtonsoft.Json;

namespace Agenciapp.Controllers
{
    public class UsersController : Controller
    {
        private readonly databaseContext _context;

        public UsersController(databaseContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Email,EmailConfirmed,PasswordHash,PasswordSalt,SecureCode,ExpiresSecureCode,Status,Type,AccountId,Timestamp")] User user)
        {
            Agency agency = _context.Agency.First();
            if (ModelState.IsValid)
            {
                user.UserId = Guid.NewGuid();
                user.AccountId = agency.AgencyId;
                user.Status = "Inactivo";
                user.Timestamp = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,Name,Email,EmailConfirmed,PasswordHash,PasswordSalt,SecureCode,ExpiresSecureCode,Status,Type,AccountId,Timestamp")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
       
        public JsonResult GetValues(/*string sidx, string sord, int page, int rows*/) //Gets the todo Lists.  
        {
            int pageIndex = Convert.ToInt32(10/*page*/) - 1;
            int pageSize = 5;//rows;
            int page = 1;
            var Results = _context.User.Select(
                a => new
                {
                    a.Name,
                    //a.Type,
                    a.Email,
                    //a.Status
                }).ToList();
            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / 5/*(float)rows*/);
            //if (sord.ToUpper() == "DESC")
            //{
            //    Results = Results.OrderByDescending(s => s.Name);
            //    Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            //}
            //else
            //{
            //    Results = Results.OrderBy(s => s.Name);
            //    Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            //}
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Results
            };
         //   return Json(jsonData);
         //   var userlist = _context.User.ToList<User>();
            return Json(new { rows = Results });

            //var citylist = new SelectList(_context.User.ToList<User>(), "Name", "Email");
            //return Json(citylist);
        }

    }
}
