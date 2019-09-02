using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Agenciapp.Models;

namespace Agenciapp.Pages.Offices.Users
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
            return Page();
        }

        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public bool EmailConfirmed { get; set; }
        [BindProperty]
        public string Phone { get; set; }

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
            User.UserId = Guid.NewGuid();
            _context.User.Add(User);
            await _context.SaveChangesAsync();


            Phone phone = new Phone();
            phone.PhoneId = Guid.NewGuid(); ;
            phone.ReferenceId = User.UserId;
            phone.Number = Phone;
            phone.Current = true;
            phone.Type = "Oficina";
            _context.Add(phone);

            Address address = new Address();
            address.AddressId = Guid.NewGuid();
            address.ReferenceId = User.UserId;
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

            return RedirectToPage("./Index");
        }
    }
}