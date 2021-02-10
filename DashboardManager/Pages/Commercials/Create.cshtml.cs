using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DashboardManager.Data;
using DashboardManager.Models;

namespace DashboardManager.Pages.Commercials
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
            return Page();
        }

        [BindProperty]
        public Commercial Commercial { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Commercial.Add(Commercial);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
