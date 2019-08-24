using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgenciappHome.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Text;
using NPOI.XWPF.UserModel;


namespace AgenciappHome.Controllers
{
    public class OrdersController : Controller
    {
        private readonly databaseContext _context;
       
        private static List<string> listProduct;
        private static List<string> listValor;
        private static Client client;
        IHostingEnvironment _env;

        public OrdersController(databaseContext context, IHostingEnvironment env)
        {
            _context = context;
            _env=env;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {

            var databaseContext = _context.Order.Include(o => o.Agency).Include(o => o.Client).Include(o => o.Contact).Include(o => o.Office).Include(o => o.TipoPago).Include(o => o.User).Include(o => o.ValorAduanalItem);

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
                .Include(o => o.Contact)
                .Include(o => o.Office)
                .Include(o => o.TipoPago)
                .Include(o => o.User)
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email");
            ViewData["ContactId"] = new SelectList(_context.Contact, "ContactId", "Email");
            ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name");
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "TipoPagoId", "Type");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email");
            ViewData["ValorAduanalId"] = new SelectList(_context.ValorAduanal, "Name", "Article");

            ViewData["ProductId"] = new SelectList(_context.Product, "Code", "Description");

            listProduct = new List<string>();
            listValor = new List<string>();

            return View();
        }

        [HttpPost]
        public void AddProduct([FromBody]string productId)
        {

            listProduct.Add(productId);
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<table class='table'><tr>");
           
            //    sb.Append("<th>Code</th>");
            //sb.Append("<th>Descripción</th>");
            //sb.Append("<th>Cant.</th>");
            //sb.Append("</tr>");

            //sb.AppendLine("<tr>");
            //sb.Append("<td>" + productId.ToString() + "</td>");
            //sb.AppendLine("</tr>");

            //sb.AppendLine("<tr>");
            //sb.Append("<td> Description</td>");
            //sb.AppendLine("</tr>");

            //sb.AppendLine("<tr>");
            //sb.Append("<td> Cant</td>");
            //sb.AppendLine("</tr>");

            //sb.Append("</table>");
            //return this.Content(sb.ToString());
        }




        [HttpPost]
        public void AddValue([FromBody]string valor)
        {

            listValor.Add(valor);
            ViewBag.totalVA +=_context.ValorAduanal.Find(Guid.Parse(valor)).Value;

        }



        [HttpPost]
        public void AddClient([FromBody]string client)
        {

            int i = 0;

        }
        [HttpPost]
        public void Importar()
        {
      
            int i = 0;

        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,AgencyId,OfficeId,ClientId,Type,Number,Date,Amount,Status,PriceLb,CantLb,TipoPagoId,ContactId,UserId,ValorPagado,Balance,OtrosCostos,ValorAduanal")] Order order, List<ValorAduanalItem> items)
        {
            if (ModelState.IsValid)
            {
                order.OrderId = Guid.NewGuid();
                order.Agency = _context.Agency.First();
                order.Office = _context.Office.First();
                order.User = _context.User.First();
              
                order.Date = DateTime.Now.Date;
                order.Number = "mx" + DateTime.Now.ToString("MMddyyyyHHmm");
                //Crear un Pacquete
                Package package = new Package();
                //package.PackageId = Guid.NewGuid();
                package.PackageNavigation = order;
                _context.Add(package);
              

                for (int i = 0; i < listProduct.Count; i++)
                {
                    PackageItem packageItem = new PackageItem();
                    packageItem.PackageItemId =  Guid.NewGuid();
                    packageItem.PackageId = package.PackageId;
                    packageItem.Package = package;
                    packageItem.ProductId = Guid.Parse(listProduct[i]);
                    packageItem.Product= _context.Product.Find(Guid.Parse(listValor[i]));
                    _context.Add(packageItem);
                }
              
                for (int i = 0; i < listValor.Count; i++)
                {
                    ValorAduanalItem value = new ValorAduanalItem();
                    value.ValorAduanalItemId = Guid.NewGuid();
                    value.OrderId = order.OrderId;
                    var va = _context.ValorAduanal.Find(Guid.Parse(listValor[i]));
                    value.ValorAduanalId = Guid.Parse(listValor[i]);
                    value.ValorAduanal = va;
                    order.ValorAduanal += va.Value;
                    _context.Add(value);
                }
                order.Amount = order.ValorAduanal + (order.CantLb * order.PriceLb) + order.OtrosCostos;
                order.Balance = order.Amount - order.ValorPagado;
                if (order.Balance != 0)//si el balance no es 0 el status es pendiente si el valanece es 0 iniciado, 
                    order.Status = "Pendiente";
                else
                    order.Status = "Iniciada";

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "LegalName", order.AgencyId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email", order.ClientId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "ContactId", "Email", order.ContactId);
            ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name", order.OfficeId);
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "TipoPagoId", "Type", order.TipoPagoId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", order.UserId);
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email", order.ClientId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "ContactId", "Email", order.ContactId);
            ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name", order.OfficeId);
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "TipoPagoId", "Type", order.TipoPagoId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,AgencyId,OfficeId,ClientId,Type,Number,Date,Amount,Status,PriceLb,CantLb,TipoPagoId,ContactId,UserId,ValorPagado,Balance,OtrosCostos,ValorAduanal")] Order order)
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email", order.ClientId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "ContactId", "Email", order.ContactId);
            ViewData["OfficeId"] = new SelectList(_context.Office, "OfficeId", "Name", order.OfficeId);
            ViewData["TipoPagoId"] = new SelectList(_context.TipoPago, "TipoPagoId", "Type", order.TipoPagoId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", order.UserId);
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
                .Include(o => o.Contact)
                .Include(o => o.Office)
                .Include(o => o.TipoPago)
                .Include(o => o.User)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void CreateClient([Bind("ClientId,AgencyId,Name,LastName,CreatedAt,AddressId,PhoneId")] Client client, string phone, string email, string calle, string city, string state, string zip)
        {
            if (ModelState.IsValid)
            {
                client.ClientId = Guid.NewGuid();
                //Add Agency
                client.AgencyId = _context.Agency.First().AgencyId;
                client.CreatedAt = DateTime.Now;

                //add Phone
                Phone movil = new Phone();
                movil.PhoneId = Guid.NewGuid();
                movil.Number = phone;
                movil.ReferenceId = client.ClientId;
                movil.Type = "Móvil";
                movil.Current = true;
                _context.Phone.Add(movil);
        

                //add Addresss
                Address a = new Address();
                a.AddressId = Guid.NewGuid();
                a.ReferenceId = client.ClientId;
                a.Current = true;
                a.Type = "Oficina";
                a.AddressLine1 = calle;
                a.City = city;
                a.State = state;
                a.Zip = zip;
                a.Country = "Usa";
                a.CreatedAt = DateTime.Now;
                a.CreatedBy = _context.User.First().UserId;
                a.UpdatedAt = DateTime.Now;
                a.UpdatedBy = _context.User.First().UserId;
                _context.Address.Add(a);
          
                client.CreatedAt = DateTime.Now;
                _context.Add(client);
                _context.SaveChangesAsync();

                //_order= new Order();
                //_order.OrderId = Guid.NewGuid();
                //_order.Agency = _context.Agency.First();
                //_order.Office = _context.Office.First();
                //_order.Client = client;
                //_order.Status = "Enviada";//enviada, recibida, entregada
                //_order.Type = "Mixto";//Mixto mx, Paquete pa,Alimento al, Medicina me, Remesas re
                //_order.Date = DateTime.Now.Date;
                //_order.Number = "mx" + DateTime.Now.ToString("MMddyyyyHHmm");
                //_context.Order.Add(_order);
                //await _context.SaveChangesAsync();

                //return RedirectToAction("Edit", _order.OrderId);
            }
    
            //return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void CreateContacto([Bind("ClientId,AgencyId,Name,LastName,CreatedAt,AddressId,PhoneId")] Contact contact, [Bind("PhoneId,ReferenceId,Type,Current,Number")] Phone phone, [Bind("AddressId,ReferenceId,Current,Type,AddressLine1,AddressLine2,City,State,Zip,Country,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] Address address)
        {
            if (ModelState.IsValid)
            {
                contact.ContactId = Guid.NewGuid();
                contact.Client = _context.Client.First();


                //Add Agency
                contact.CreatedAt = DateTime.Now;

                //add Phone
                Phone movil = new Phone();
                movil.PhoneId = Guid.NewGuid();
                movil.Number = phone.Number;
                movil.ReferenceId = contact.ContactId;
                movil.Type = "Móvil";
                movil.Current = true;
                _context.Phone.Add(movil);
             

                //add Addresss
                Address a = new Address();
                a.AddressId = Guid.NewGuid();
                a.ReferenceId = contact.ContactId;
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
           
                contact.CreatedAt = DateTime.Now;
                _context.Add(contact);
                _context.SaveChangesAsync();

            }
          
        }

        public async Task<IActionResult> OnPostExport(Guid id)
        {

            Order o = _context.Order.Find(id);

            string sWebRootFolder = _env.WebRootPath;
            string sFileName = @"demo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");

                IRow row = excelSheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("Envío No: " + o.Number);
                row = excelSheet.CreateRow(1);
                row.CreateCell(0).SetCellValue("Tipo de Envío: " + o.Type);
                row = excelSheet.CreateRow(2);
                row.CreateCell(0).SetCellValue("Dirección oficina: ");
                row = excelSheet.CreateRow(3);
                row.CreateCell(0).SetCellValue("Teléfono oficina: ");
                row = excelSheet.CreateRow(4);
                row.CreateCell(0).SetCellValue("No Empleado: ");
                row = excelSheet.CreateRow(5);
                row.CreateCell(0).SetCellValue("Nombre Empleado: ");

                //Dirección Oficina 9732 SW 40 ST MIAMI, FL 33165

                //IRow row = excelSheet.CreateRow(0);
                //row.CreateCell(0).SetCellValue("ID");
                //row.CreateCell(1).SetCellValue("Name");
                //row.CreateCell(2).SetCellValue("Age");

                //row = excelSheet.CreateRow(1);
                //row.CreateCell(0).SetCellValue(1);
                //row.CreateCell(1).SetCellValue("Kane Williamson");
                //row.CreateCell(2).SetCellValue(29);

                //row = excelSheet.CreateRow(2);
                //row.CreateCell(0).SetCellValue(2);
                //row.CreateCell(1).SetCellValue("Martin Guptil");
                //row.CreateCell(2).SetCellValue(33);

                //row = excelSheet.CreateRow(3);
                //row.CreateCell(0).SetCellValue(3);
                //row.CreateCell(1).SetCellValue("Colin Munro");
                //row.CreateCell(2).SetCellValue(23);

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);



        }

        public async Task<IActionResult> Exxport(Guid id)//
        {

            Order o = _context.Order.Find(id);
            string sWebRootFolder = _env.WebRootPath;
            var memory = new MemoryStream();
            XWPFDocument doc = new XWPFDocument();

            XWPFParagraph p1 = doc.CreateParagraph();
            p1.Alignment = ParagraphAlignment.LEFT;

            //p1.BorderBottom = Borders.Double;
            //p1.BorderTop = Borders.Double;
            //p1.BorderRight = Borders.Double;
            //p1.BorderLeft = Borders.Double;
            //p1.BorderBetween = Borders.Single;

           // p1.VerticalAlignment = TextAlignment.TOP;

            XWPFRun r1 = p1.CreateRun();
            
            r1.SetText("Envío No.: "+o.Number);
            r1.AddBreak(BreakType.TEXTWRAPPING);
            r1.IsBold = true;
            r1.FontFamily = "Courier";
            //r1.SetUnderline(UnderlinePatterns.DotDotDash);
            //r1.SetTextPosition(100);

            XWPFRun r2 = p1.CreateRun();
            r2.SetText("Tipo Envío" + o.Type);
            r2.AddBreak(BreakType.TEXTWRAPPING);
            r2.FontFamily = "Courier";
            r2.SetTextPosition(20);

            XWPFRun r3 = p1.CreateRun();
            r1.IsBold = true;
            r3.SetText("Datos del cliene");
            r3.AddBreak(BreakType.TEXTWRAPPING);
            r3.FontFamily = "Courier";

            XWPFRun r4 = p1.CreateRun();
            //r4.SetText("Cliente No." + o.Client.ClientId+ "    " + "Cliente Nombre: "+ o.Client.Name+ o.Client.LastName);
            r4.SetText("Cliente No");
            r4.AddBreak(BreakType.TEXTWRAPPING);
            r4.FontFamily = "Courier";

            XWPFRun r5 = p1.CreateRun();
            //r5.SetText("Cliente Telefono." + _context.Phone.Where(x=>x.ReferenceId== o.Client.ClientId).FirstOrDefault());
            r5.SetText("Cliente Phone");
            r5.AddBreak(BreakType.TEXTWRAPPING);
            r5.FontFamily = "Courier";
            r5.SetTextPosition(20);

            XWPFRun r6 = p1.CreateRun();
            r6.IsBold = true;
            r6.SetText("Relación de artículos");
            r6.AddBreak(BreakType.TEXTWRAPPING);
            r6.FontFamily = "Courier";

            XWPFRun r7 = p1.CreateRun();
            r7.IsBold = true;
            // r7.SetText(o.Package.PackageItem.First().Qty.ToString()+"  "+ o.Package.PackageItem.First().Product.Description);
             r7.SetText("                   4 ghgdsfvhdsoidshzgv");

            r7.AddBreak(BreakType.TEXTWRAPPING);
            r7.FontFamily = "Courier";
           // r7.Subscript=VerticalAlign.;

            //XWPFParagraph p2 = doc.CreateParagraph();
            //p2.Alignment = ParagraphAlignment.LEFT;

            ////BORDERS
            ////p2.BorderBottom = Borders.Double;
            ////p2.BorderTop = Borders.Double;
            ////p2.BorderRight = Borders.Double;
            ////p2.BorderLeft = Borders.Double;
            ////p2.BorderBetween = Borders.Single;

            //XWPFRun r2 = p2.CreateRun();
            //r2.SetText("jumped over the lazy dog");
            //r2.FontSize = 20;


            //XWPFRun r3 = p2.CreateRun();
            //r3.SetText("and went away");
            //r3.FontSize = 20;
            //r3.Subscript = VerticalAlign.SUPERSCRIPT;


            //XWPFParagraph p3 = doc.CreateParagraph();
            //p3.IsWordWrap=true;
            //p3.IsPageBreak= true;

            ////p3.SetAlignment(ParagraphAlignment.DISTRIBUTE);
            //p3.Alignment=ParagraphAlignment.BOTH;
            //p3.SpacingLineRule=LineSpacingRule.EXACT;

            //p3.IndentationFirstLine=600;


            //XWPFRun r4 = p3.CreateRun();
            //r4.SetTextPosition(20);
            //r4.SetText("To be, or not to be: that is the question: "
            //        + "Whether 'tis nobler in the mind to suffer "
            //        + "The slings and arrows of outrageous fortune, "
            //        + "Or to take arms against a sea of troubles, "
            //        + "And by opposing end them? To die: to sleep; ");
            //r4.AddBreak(BreakType.PAGE);
            //r4.SetText("No more; and by a sleep to say we end "
            //        + "The heart-ache and the thousand natural shocks "
            //        + "That flesh is heir to, 'tis a consummation "
            //        + "Devoutly to be wish'd. To die, to sleep; "
            //        + "To sleep: perchance to dream: ay, there's the rub; "
            //        + ".......");
            //r4.IsItalic=true;
            ////This would imply that this break shall be treated as a simple line break, and break the line after that word:

            //XWPFRun r5 = p3.CreateRun();
            //r5.SetTextPosition(-10);
            //r5.SetText("For in that sleep of death what dreams may come");
            //r5.AddCarriageReturn();
            //r5.SetText("When we have shuffled off this mortal coil,"
            //        + "Must give us pause: there's the respect"
            //        + "That makes calamity of so long life;");
            //r5.AddBreak();
            //r5.SetText("For who would bear the whips and scorns of time,"
            //        + "The oppressor's wrong, the proud man's contumely,");

            //r5.AddBreak(BreakClear.ALL);
            //r5.SetText("The pangs of despised love, the law's delay,"
            //        + "The insolence of office and the spurns" + ".......");


            FileStream fs = new FileStream(Path.Combine(sWebRootFolder, "simple.docx"), FileMode.Create, FileAccess.Write);
            
                doc.Write(fs);
                fs.Close();

            

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, "simple.docx"), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.docx", "simple.docx");

        }
        [HttpPost]
        public void Add([FromBody]string productId)
        {

            int i = 0;

        }

        [HttpPost]

        public JsonResult GetClient()

        {

            //This can be replaced with database call.

            List<Client> clientList = new List<Client>(){

                new Client {Name="zuly",LastName="rodriguez" },

                new Client {Name="saul",LastName="Football" }
            };


           

            return Json(clientList);

        }


        ////    [HttpPost]
        ////    public JsonResult InsertProductos(List<PackageItem> productos)
        ////    {
        ////            //Check for NULL.
        ////            if (productos == null)
        ////            {
        ////                productos = new List<PackageItem>();
        ////            }

        ////            //Loop and insert records.
        ////            foreach (PackageItem customer in productos)
        ////            {
        ////                _context.PackageItem.Add(customer);
        ////            }
        ////            int insertedRecords = _context.SaveChanges();
        ////            return Json(insertedRecords);

        ////}

    }
}
