using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenciapp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Agenciapp.Pages.Clients
{
    public class ImportModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;
        IHostingEnvironment _env;
        public ImportModel(Agenciapp.Models.databaseContext context, IHostingEnvironment env)
        {
            _context = context;
             _env=env;
        }

        public IActionResult OnGet()
        {
            return Page();
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

                    sb.Append("<table class='table'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");



                    var clientList = new List<Client>();
                    var phoneList = new List<Phone>();
                    var addressList = new List<Address>();

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;

                        Client c;
                        if (!_context.Client.Where(x => x.Name == row.GetCell(0).ToString() && x.LastName == row.GetCell(1).ToString()).Any())
                        {
                            c = new Client();
                            c.ClientId = Guid.NewGuid();
                            c.Name = row.GetCell(0).ToString();
                            c.LastName = row.GetCell(1).ToString();
                            c.Email = row.GetCell(2).ToString();
                            c.CreatedAt = DateTime.Now;
                            c.Agency = _context.Agency.First();//Ver aki va la agencia registrada
                            _context.Client.Add(c);

                        }
                        else
                        {
                            c = _context.Client.Where(x => x.Name == row.GetCell(0).ToString() && x.LastName == row.GetCell(1).ToString()).FirstOrDefault();
                        }
                        if (!_context.Phone.Where(x => x.ReferenceId == c.ClientId && x.Number == row.GetCell(4).ToString()).Any())
                        {
                            Phone phone;
                            phone = new Phone();
                            phone.PhoneId = Guid.NewGuid();
                            phone.Type = row.GetCell(3).ToString();
                            phone.Number = row.GetCell(4).ToString();
                            phone.Current = true;
                            phone.ReferenceId = c.ClientId;
                            _context.Phone.Add(phone);
                        }
                        if (!_context.Address.Where(x => x.ReferenceId == c.ClientId && x.Type == row.GetCell(6).ToString()).Any())
                        {
                            Address add = new Address();
                            add.AddressId = Guid.NewGuid();
                            add.Type = row.GetCell(5).ToString();
                            add.AddressLine1 = row.GetCell(6).ToString();
                            add.City = row.GetCell(7).ToString();
                            add.State = row.GetCell(8).ToString();
                            add.Country = row.GetCell(9).ToString();
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