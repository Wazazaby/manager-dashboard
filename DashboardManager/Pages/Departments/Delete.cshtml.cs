using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DashboardManager.Data;
using DashboardManager.Models;

namespace DashboardManager.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public DeleteModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Departement Departement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departement = await _context.Departement.FirstOrDefaultAsync(m => m.Id == id);

            if (Departement == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departement = await _context.Departement.FindAsync(id);

            if (Departement != null)
            {
                _context.Departement.Remove(Departement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
