using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DashboardManager.Data;
using DashboardManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DashboardManager.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public CreateModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var test = _context.Departement.ToListAsync();
            return Page();
        }

        [BindProperty]
        public Client Client { get; set; }
        public List<Departement> Departments { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Client.Add(Client);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
