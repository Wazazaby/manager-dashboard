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
        public List<Departement> Departements { get; set; }
        public List<Client> Clients { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<Client> clientQuery = from c in _context.Client select c;
            Clients = await clientQuery.ToListAsync();

            IQueryable<Departement> departementQuery = from m in _context.Departement orderby m.Name select m;
            Departements = await departementQuery.Distinct().ToListAsync();

            Commercial = await _context.Commercial.FirstOrDefaultAsync(m => m.Id == id);

            if (Commercial == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
