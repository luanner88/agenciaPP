using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenciapp.Models;
using Microsoft.AspNetCore.Hosting;
using NPOI.XWPF.UserModel;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace Agenciapp.Pages.Orders
{
    public class ExportComprobantePagoModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;
        IHostingEnvironment _env;
        public ExportComprobantePagoModel(Agenciapp.Models.databaseContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [BindProperty]
        public Order Order { get; set; }
        public List<PackageItem> listPackageItem;
       static Guid? _id;
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            _id = id;

            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order
                .Include(o => o.Agency)
                .Include(o => o.Client)
                .Include(o => o.Contact)
                .Include(o => o.Office)
                .Include(o => o.TipoPago)
                .Include(o => o.User).FirstOrDefaultAsync(m => m.OrderId == id);
            listPackageItem = _context.PackageItem.Where(x => x.Package.PackageNavigation.OrderId == id).ToList();

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
           public async Task<IActionResult> OnPostAsync()
        {

            Order = _context.Order.Find(_id);
                
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

            //Envio
            XWPFRun r1 = p1.CreateRun();
            r1.SetText("Comprobante de Pago: " + DateTime.Now.ToString("MM/dd/yyyy"));
            r1.AddBreak(BreakType.TEXTWRAPPING);
            r1.FontFamily = "Courier";
            //r1.SetUnderline(UnderlinePatterns.DotDotDash);
            r1.SetTextPosition(20);

            XWPFRun r2 = p1.CreateRun();
            r2.SetText("Factura No.: ");
            r1.IsBold = true;
            r2.AddBreak(BreakType.TEXTWRAPPING);
            r2.FontFamily = "Courier";
            //r1.SetUnderline(UnderlinePatterns.DotDotDash);
            //r1.SetTextPosition(100);
            XWPFRun r3 = p1.CreateRun();
            r3.SetText(Order.Number);
            r3.AddBreak(BreakType.TEXTWRAPPING);
            r3.FontFamily = "Courier";
            r3.SetTextPosition(20);

            //Oficina
            XWPFRun r4 = p1.CreateRun();
            Address add = _context.Address.Where(x => x.ReferenceId == Order.OfficeId).First();
            r4.SetText("Dirección Oficina: " + add.AddressLine1 + " " + add.City + "," + add.State);
            r4.AddBreak(BreakType.TEXTWRAPPING);
            r4.FontFamily = "Courier";
            r4.SetTextPosition(20);
            XWPFRun r5 = p1.CreateRun();
            r5.SetText("Tel.(Oficina): " + _context.Phone.Where(x => x.ReferenceId == Order.OfficeId).First().Number);
            r5.AddBreak(BreakType.TEXTWRAPPING);
            r5.FontFamily = "Courier";

            //User
            XWPFRun r6 = p1.CreateRun();
            r6.SetText("No del Empleado" + Order.UserId);
            r6.AddBreak(BreakType.TEXTWRAPPING);
            r6.FontFamily = "Courier";
            r6.SetTextPosition(20);
            XWPFRun r7 = p1.CreateRun();
            r7.SetText("Nombre Empleado: " + _context.User.Find(Order.UserId).Name);
            r7.AddBreak(BreakType.TEXTWRAPPING);
            r7.FontFamily = "Courier";
            r7.SetTextPosition(20);


            //Cliente
            XWPFRun r8 = p1.CreateRun();
            r8.IsBold = true;
            r8.SetText("Datos del cliene");
            r8.AddBreak(BreakType.TEXTWRAPPING);
            r8.FontFamily = "Courier";
            XWPFRun r9 = p1.CreateRun();
            Client client = _context.Client.Find(Order.ClientId);
            r9.SetText("Cliente No." + client.ClientId + "    " + "Cliente Nombre: " + client.Name + client.LastName);
            r9.AddBreak(BreakType.TEXTWRAPPING);
            r9.FontFamily = "Courier";
            XWPFRun r10 = p1.CreateRun();
            r10.SetText("Tel.(Móvil)" + _context.Phone.Where(x => x.ReferenceId == Order.ClientId).First().Number);
            r10.AddBreak(BreakType.TEXTWRAPPING);
            r10.FontFamily = "Courier";

            //Orden
            XWPFRun r11 = p1.CreateRun();
            r11.SetText("Tipo de envío: " + Order.Type);
            r11.AddBreak(BreakType.TEXTWRAPPING);
            r11.FontFamily = "Courier";
            r11.SetTextPosition(20);

            XWPFRun r12 = p1.CreateRun();
            r12.SetText("Fecha del Pago: " + DateTime.Now.ToString("MM/dd/yyyy") + "            Tipo de Pago: " + _context.TipoPago.Find(Order.TipoPagoId).Type);//Ver
            r12.AddBreak(BreakType.TEXTWRAPPING);
            r12.FontFamily = "Courier";
            XWPFRun r13 = p1.CreateRun();
            r13.SetText("Monto Total: " + Order.Amount);//Ver
            r13.AddBreak(BreakType.TEXTWRAPPING);
            r13.FontFamily = "Courier";
            r13.SetTextPosition(20);

            XWPFRun r14 = p1.CreateRun();
            r14.SetText("Total Pagado: " + Order.ValorPagado);//Ver
            r14.AddBreak(BreakType.TEXTWRAPPING);
            r14.FontFamily = "Courier";
            XWPFRun r15 = p1.CreateRun();
            r15.SetText("Monto Total: " + Order.Balance);//Ver
            r15.AddBreak(BreakType.TEXTWRAPPING);
            r15.FontFamily = "Courier";
            r15.SetTextPosition(20);

            //Contacto
            XWPFRun r16 = p1.CreateRun();
            r16.IsBold = true;
            r16.SetText("Datos: Contacto (Recibe Envió)");
            r16.AddBreak(BreakType.TEXTWRAPPING);
            r16.FontFamily = "Courier";
            XWPFRun r17 = p1.CreateRun();
            Contact contact = _context.Contact.Find(Order.ContactId);
            r17.SetText("Contacto No." + contact.ContactId);
            r17.AddBreak(BreakType.TEXTWRAPPING);
            r17.FontFamily = "Courier";
            XWPFRun r18 = p1.CreateRun();
            r18.SetText("Contacto Nombre: " + contact.Name + contact.Name);
            r18.AddBreak(BreakType.TEXTWRAPPING);
            r18.FontFamily = "Courier";

            XWPFRun r19 = p1.CreateRun();
            r16.SetText("Tel.(Fijo)" + _context.Phone.Where(x => x.ReferenceId == Order.ContactId && x.Type=="Fijo").FirstOrDefault().Number);
           // r19.SetText("Tel.(Fijo)");
            r19.AddBreak(BreakType.TEXTWRAPPING);
            r19.FontFamily = "Courier";
            XWPFRun r20 = p1.CreateRun();
            r17.SetText("Tel.(Móvil)" + _context.Phone.Where(x => x.ReferenceId == Order.ContactId && x.Type == "Movil").FirstOrDefault().Number);
           // r20.SetText("Tel.(Móvil)");

            r20.AddBreak(BreakType.TEXTWRAPPING);
            r20.FontFamily = "Courier";
            XWPFRun r21 = p1.CreateRun();
            if (_context.Phone.Where(x => x.ReferenceId == Order.ContactId && x.Type != "Movil" && x.Type != "Fijo").Any())
                r18.SetText("Tel.(Otro): " + _context.Phone.Where(x => x.ReferenceId == Order.ContactId && x.Type != "Movil" && x.Type != "Fijo").FirstOrDefault().Number);
            else
                r18.SetText("Tel.(Otro):");

          r21.AddBreak(BreakType.TEXTWRAPPING);
            r21.FontFamily = "Courier";
            XWPFRun r22 = p1.CreateRun();
             Address add1 = _context.Address.Where(x => x.ReferenceId == contact.ContactId).First();
             r22.SetText("Dirección: " + add.AddressLine1 + " " + add.City + "," + add.State);
          

            r22.AddBreak(BreakType.TEXTWRAPPING);
            r22.FontFamily = "Courier";
            r22.SetTextPosition(20);

            XWPFRun r23 = p1.CreateRun();
            r23.IsBold = true;
            r23.SetText("Firma del cliente: _________    Firma del empleado: __________");
            r23.AddBreak(BreakType.TEXTWRAPPING);
            r23.FontFamily = "Courier";

            FileStream fs = new FileStream(Path.Combine(sWebRootFolder, "Comprobante de Pago.docx"), FileMode.Create, FileAccess.Write);

            doc.Write(fs);
            fs.Close();

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, "Comprobante de Pago.docx"), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            EnviarCorreo(Path.Combine(sWebRootFolder, "Comprobante de Pago.docx"));
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.docx", "Comprobante de Pago.docx");
            //return RedirectToPage("././Index");
        }
        public void EnviarCorreo(string attch)
        {
            MailMessage correo = new MailMessage("zuleidyrg@gmail.com", "zuleidyrg@gmail.com", "comprobante de pago", "mensaje");
            correo.Attachments.Add(new Attachment(attch));
            SmtpClient servidor = new SmtpClient("smtp.live.com", 587);
            NetworkCredential credenciales = new NetworkCredential("zuleidyrg", "maluma123");
            servidor.Credentials = credenciales;
            servidor.EnableSsl = true;

            try
            {
                servidor.Send(correo);
                Console.WriteLine("\t\tCorreo enviado de manera exitosa");
                correo.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
