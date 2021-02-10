using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DashboardManager.Data;
using DashboardManager.Models;

namespace DashboardManager.Pages.Commercials
{
    public class DeleteModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public DeleteModel(DashboardManager.Data.DashboardManagerContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Commercial = await _context.Commercial.FindAsync(id);

            if (Commercial != null)
            {
                _context.Commercial.Remove(Commercial);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
