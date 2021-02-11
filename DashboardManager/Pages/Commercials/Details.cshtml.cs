using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DashboardManager.Models;
using System.Linq;

namespace DashboardManager.Pages.Commercials
{
    public class DetailsModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public DetailsModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        public Commercial Commercial { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Commercial = await _context.Commercial.FirstOrDefaultAsync(c => c.Id == id);
            await _context.Client.ToListAsync();
            await _context.Departement.ToListAsync();

            if (Commercial == null)
            {
                return NotFound();
            }

            return Page();
        }

    }
}
