using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgenciappHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;

namespace AgenciappHome.Controllers
{
    public class ContactsController : Controller
    {
        private readonly databaseContext _context;
        IHostingEnvironment _env;
        public ContactsController(databaseContext context, IHostingEnvironment env)
        {
            _context = context;
            _env=env;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Contact.Include(c => c.Client);
            return View(await databaseContext.ToListAsync());
        }
        // GET: Clients
        public IActionResult ImportContact()
        {

            return View();
        }



        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Client)
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,ClientId,Name,LastName,CreatedAt,Email")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.ContactId = Guid.NewGuid();
                contact.CreatedAt = DateTime.Now;
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email", contact.ClientId);
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email", contact.ClientId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ContactId,ClientId,Name,LastName,CreatedAt,Email")] Contact contact)
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email", contact.ClientId);
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
                .Include(c => c.Client)
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


        public ActionResult OnPostImport()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _env.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;

                    //sb.Append("<table class='table'><tr>");
                    //for (int j = 0; j < cellCount; j++)
                    //{
                    //    NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                    //    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    //    sb.Append("<th>" + cell.ToString() + "</th>");
                    //}
                    //sb.Append("</tr>");
                    //sb.AppendLine("<tr>");
                    //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    //{
                    //    IRow row = sheet.GetRow(i);
                    //    if (row == null) continue;
                    //    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    //    for (int j = row.FirstCellNum; j < cellCount; j++)
                    //    {
                    //        if (row.GetCell(j) != null)
                    //            sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                    //    }
                    //    sb.AppendLine("</tr>");
                    //}
                    //sb.Append("</table>");


                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;

                        Contact c;
                        if (!_context.Contact.Where(x => x.Name == row.GetCell(2).ToString() && x.LastName == row.GetCell(3).ToString()).Any())
                        {
                            c = new Contact();
                            c.ClientId = Guid.NewGuid();
                            c.Name = row.GetCell(0).ToString();
                            c.LastName = row.GetCell(1).ToString();
                            c.CreatedAt = DateTime.Now;
                            c.Client = _context.Client.Where(x => x.Name == row.GetCell(0).ToString() && x.LastName == row.GetCell(1).ToString()).FirstOrDefault();
                            _context.Contact.Add(c);

                        }
                        else
                        {
                            c = _context.Contact.Where(x => x.Name == row.GetCell(2).ToString() && x.LastName == row.GetCell(3).ToString()).FirstOrDefault();
                        }
                        if (!_context.Phone.Where(x => x.ReferenceId == c.ClientId && x.Number == row.GetCell(5).ToString()).Any())
                        {
                            Phone phone;
                            phone = new Phone();
                            phone.PhoneId = Guid.NewGuid();
                            phone.Type = row.GetCell(4).ToString();
                            phone.Number = row.GetCell(5).ToString();
                            phone.Current = true;
                            phone.ReferenceId = c.ContactId;
                            _context.Phone.Add(phone);
                        }
                        if (!_context.Address.Where(x => x.ReferenceId == c.ClientId && x.Type == row.GetCell(6).ToString()).Any())
                        {
                            Address add = new Address();
                            add.AddressId = Guid.NewGuid();
                            add.Type = row.GetCell(6).ToString();
                            add.AddressLine1 = row.GetCell(7).ToString();
                            add.City = row.GetCell(8).ToString();
                            add.State = row.GetCell(9).ToString();
                            add.Country = row.GetCell(10).ToString();
                            add.ReferenceId = c.ClientId;
                            add.CreatedAt = DateTime.Now;
                            add.CreatedBy = _context.User.FirstOrDefault().UserId;//Ver Aqui va el usuario registrado
                            add.UpdatedAt = DateTime.Now;
                            add.UpdatedBy = _context.User.FirstOrDefault().UserId;//Ver Aqui va el usuario registrado
                            _context.Address.Add(add);
                        }
                    }
                    _context.SaveChanges();
                    ViewData["Message"] = "Los datos han sido importados satisfactoriamente";
                }
            }
            return this.Content(sb.ToString());
        }
    }
}
