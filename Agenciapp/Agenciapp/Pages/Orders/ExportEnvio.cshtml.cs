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
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Agenciapp.Pages
{
    public class ExportEnvioModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;
        IHostingEnvironment _env;
        public ExportEnvioModel(Agenciapp.Models.databaseContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public Order Order { get; set; }
        public List<PackageItem> listPackageItem;
        static Guid? _id;
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
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

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Order = _context.Order.Find(_id);
            Package pac = _context.Package.Where(x => x.PackageNavigation.OrderId == _id).First();
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


            //Orden
            XWPFRun r1 = p1.CreateRun();
            r1.SetText("Envío No.: " + Order.Number);
            r1.AddBreak(BreakType.TEXTWRAPPING);
            r1.IsBold = true;
            r1.FontFamily = "Courier";
            //r1.SetUnderline(UnderlinePatterns.DotDotDash);
            //r1.SetTextPosition(100);
            XWPFRun r2 = p1.CreateRun();
            r2.SetText("Tipo Envío" + Order.Type);
            r2.AddBreak(BreakType.TEXTWRAPPING);
            r2.FontFamily = "Courier";
            r2.SetTextPosition(20);

            //Oficina
            XWPFRun r3 = p1.CreateRun();
            Address add = _context.Address.Where(x => x.ReferenceId == Order.OfficeId).First();
            r3.SetText("Dirección Oficina: " + add.AddressLine1 + " " + add.City + "," + add.State);
            r3.AddBreak(BreakType.TEXTWRAPPING);
            r3.FontFamily = "Courier";
            r3.SetTextPosition(20);
            XWPFRun r4 = p1.CreateRun();
            r4.SetText("Tel.(Oficina): " + _context.Phone.Where(x => x.ReferenceId == Order.OfficeId).First().Number);
            r4.AddBreak(BreakType.TEXTWRAPPING);
            r4.FontFamily = "Courier";
            r4.SetTextPosition(20);

            //User
            XWPFRun r5 = p1.CreateRun();
            r5.SetText("No del Empleado" + Order.UserId);
            r5.AddBreak(BreakType.TEXTWRAPPING);
            r5.FontFamily = "Courier";
            r5.SetTextPosition(20);
            XWPFRun r6 = p1.CreateRun();
            r6.SetText("Nombre Empleado: " + _context.User.Find(Order.UserId).Name);
            r6.AddBreak(BreakType.TEXTWRAPPING);
            r6.FontFamily = "Courier";
            r6.SetTextPosition(20);

            //Cliente
            XWPFRun r7 = p1.CreateRun();
            r7.IsBold = true;
            r7.SetText("Datos del cliene");
            r7.AddBreak(BreakType.TEXTWRAPPING);
            r7.FontFamily = "Courier";
            XWPFRun r8 = p1.CreateRun();
            Client client = _context.Client.Find(Order.ClientId);
            r8.SetText("Cliente No." + client.ClientId + "    " + "Cliente Nombre: " + client.Name + client.LastName);
            r8.AddBreak(BreakType.TEXTWRAPPING);
            r8.FontFamily = "Courier";
            XWPFRun r9 = p1.CreateRun();
            r9.SetText("Tel.(Cliente)" + _context.Phone.Where(x => x.ReferenceId == Order.ClientId).FirstOrDefault().Number);
            r9.AddBreak(BreakType.TEXTWRAPPING);
            r9.FontFamily = "Courier";
            r9.SetTextPosition(20);
            XWPFRun r10 = p1.CreateRun();
            r10.IsBold = true;
            r10.SetText("Relación de artículos");
            r10.AddBreak(BreakType.TEXTWRAPPING);
            r10.FontFamily = "Courier";

            // XWPFRun r11 = p1.CreateRun();
            //// r11.SetTextPosition(-10);
            // r11.SetText("For in that sleep of death what dreams may come");
            // r11.AddCarriageReturn();
            // r11.SetText("When we have shuffled off this mortal coil,"
            //         + "Must give us pause: there's the respect"
            //         + "That makes calamity of so long life;");
            // r11.AddBreak();
            // r11.SetText("For who would bear the whips and scorns of time,"
            //         + "The oppressor's wrong, the proud man's contumely,");

            // r11.AddBreak(BreakClear.ALL);
            // r11.SetText("The pangs of despised love, the law's delay,"
            //         + "The insolence of office and the spurns" + ".......");


            XWPFParagraph p2 = doc.CreateParagraph();
            p2.Alignment = ParagraphAlignment.LEFT;

                  int total = 0;
            listPackageItem = _context.PackageItem.Where(x => x.PackageId == pac.PackageId).ToList();
                for (int i = 0; i < listPackageItem.Count(); i++)
            {
                //Productos
                XWPFRun r11 = p2.CreateRun();
                r11.IsBold = true;
                r11.FontFamily = "Courier";
                r11.SetText(listPackageItem[i].Qty.ToString()+"  "+ listPackageItem[i].Product.Description + "  " + listPackageItem[i].Description);
                r11.AddBreak(BreakType.TEXTWRAPPING);
                total++;
            }


            XWPFParagraph p3 = doc.CreateParagraph();
            p3.Alignment = ParagraphAlignment.LEFT;

            XWPFRun r31 = p3.CreateRun();
            r31.SetText("Total de Artículos" + total);
            r31.AddBreak(BreakType.TEXTWRAPPING);
            r31.FontFamily = "Courier";

            XWPFRun r32 = p3.CreateRun();
            r32.SetText("Total" + Order.Amount);
            r32.AddBreak(BreakType.TEXTWRAPPING);
            r32.FontFamily = "Courier";
            r32.SetTextPosition(20);

            //Contacto
            XWPFRun r14 = p3.CreateRun();
            r14.IsBold = true;
            r14.SetText("Datos: Contacto (Recibe Envió)");
            r14.AddBreak(BreakType.TEXTWRAPPING);
            r14.FontFamily = "Courier";
            XWPFRun r15 = p1.CreateRun();
            r15.SetText("Contacto No." + Order.ContactId);
            r15.AddBreak(BreakType.TEXTWRAPPING);
            r15.FontFamily = "Courier";
            XWPFRun r16 = p3.CreateRun();
            string nombre = _context.Contact.Find(Order.ContactId).Name +" "+ _context.Contact.Find(Order.ContactId).LastName;
            r16.SetText("Contacto Nombre: " +nombre);
            r16.AddBreak(BreakType.TEXTWRAPPING);
            r16.FontFamily = "Courier";

            XWPFRun r17 = p3.CreateRun();
            // r16.SetText("Tel.(Fijo)" + _context.Phone.Where(x => x.ReferenceId == o.ContactId && x.Type=="Fijo").FirstOrDefault().Number);
            r17.SetText("Tel.(Fijo)");
            r17.AddBreak(BreakType.TEXTWRAPPING);
            r17.FontFamily = "Courier";
            XWPFRun r18 = p1.CreateRun();
            //r17.SetText("Tel.(Móvil)" + _context.Phone.Where(x => x.ReferenceId == o.ContactId && x.Type == "Movil").FirstOrDefault().Number);
            r18.SetText("Tel.(Móvil)");

            r18.AddBreak(BreakType.TEXTWRAPPING);
            r18.FontFamily = "Courier";
            XWPFRun r19 = p3.CreateRun();
            //r18.SetText("Tel.(Otro)" + _context.Phone.Where(x => x.ReferenceId == o.ContactId && x.Type != "Movil" && x.Type != "Fijo").FirstOrDefault().Number);
            r19.SetText("Tel.(Otro)");

            r19.AddBreak(BreakType.TEXTWRAPPING);
            r19.FontFamily = "Courier";
            XWPFRun r20 = p3.CreateRun();
            // Address add1 = _context.Address.Where(x => x.ReferenceId == contact.ContactId).First();
            // r19.SetText("Dirección: " + add.AddressLine1 + " " + add.City + "," + add.State);
            r20.SetText("Dirección: ");

            r20.AddBreak(BreakType.TEXTWRAPPING);
            r20.FontFamily = "Courier";
            r20.SetTextPosition(20);

            XWPFRun r21 = p3.CreateRun();
            r21.IsBold = true;
            r21.SetText("Firma del cliente                         Firma del empleado");
            r21.AddBreak(BreakType.TEXTWRAPPING);
            r21.FontFamily = "Courier";
     

     

            FileStream fs = new FileStream(Path.Combine(sWebRootFolder, "Envio.docx"), FileMode.Create, FileAccess.Write);
            doc.Write(fs);
            fs.Close();
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, "Envio.docx"), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.docx", "Envio.docx");

            return RedirectToPage("././Index");
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
