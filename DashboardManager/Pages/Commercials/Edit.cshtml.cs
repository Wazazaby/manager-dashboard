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

namespace DashboardManager.Pages.Commercials
{
    public class EditModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public EditModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Commercial Commercial { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Commercial = await _context.Commercial.FirstOrDefaultAsync(m => m.Id == id);

            if (Commercial == null)
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

            _context.Attach(Commercial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommercialExists(Commercial.Id))
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

        private bool CommercialExists(int id)
        {
            return _context.Commercial.Any(e => e.Id == id);
        }
    }
}
