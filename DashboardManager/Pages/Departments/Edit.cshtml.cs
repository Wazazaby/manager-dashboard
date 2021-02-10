using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DashboardManager.Data;
using DashboardManager.Models;

namespace DashboardManager.Pages.Departments
{
    public class EditModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public EditModel(DashboardManager.Data.DashboardManagerContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Departement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartementExists(Departement.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DepartementExists(int id)
        {
            return _context.Departement.Any(e => e.Id == id);
        }
    }
}
