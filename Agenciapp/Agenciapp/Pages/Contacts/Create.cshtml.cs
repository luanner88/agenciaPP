using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Agenciapp.Models;

namespace Agenciapp.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        private readonly Agenciapp.Models.databaseContext _context;

        public CreateModel(Agenciapp.Models.databaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email");
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; }
        [BindProperty]
        public Client Client { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string Mobil { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string City { get; set; }

        [BindProperty]
        public string State { get; set; }

        [BindProperty]
        public string Zip { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Contact.ContactId = Guid.NewGuid();
            Contact.CreatedAt = DateTime.Now;
            _context.Contact.Add(Contact);

            Phone phone = new Phone();
            phone.PhoneId = Guid.NewGuid(); 
            phone.ReferenceId = Contact.ContactId;
            phone.Number = Phone;
            phone.Current = true;
            phone.Type = "Fijo";
            _context.Add(phone);

            Phone movil = new Phone();
            movil.PhoneId = Guid.NewGuid();
            movil.ReferenceId = Contact.ContactId;
            movil.Number = Phone;
            movil.Current = true;
            movil.Type = "Movil";
            _context.Add(movil);


            Address address = new Address();
            address.AddressId = Guid.NewGuid();
            address.ReferenceId = Contact.ContactId;
            address.AddressLine1 = Address;
            address.City = City;
            address.State = State;
            address.Zip = Zip;
            address.Country = "Estados Unidos";
            address.Type = "Oficina";
            address.CreatedAt = DateTime.Now;
            address.CreatedBy = _context.User.FirstOrDefault().UserId;//Ver Aqui va el usuario registrado
            address.UpdatedAt = DateTime.Now;
            address.UpdatedBy = _context.User.FirstOrDefault().UserId;//Ver Aqui va el usuario registrado
            _context.Add(address);


            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}