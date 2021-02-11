using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DashboardManager.Data;
using DashboardManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DashboardManager.Pages.Commercials
{
    public class IndexModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public IndexModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        public IList<Commercial> Commercial { get; set; }
        public IList<Departement> Departements { get; set; }
        public IList<Client> Clients { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Name { get; set; }

        public async Task OnGetAsync()
        {
            await _context.Departement.Distinct().ToListAsync();
            await _context.Client.ToListAsync();
            // Utilisation de LINQ pour faire notre requête
            IQueryable<Commercial> commercials = from c in _context.Commercial select c;
            if (!string.IsNullOrEmpty(SearchString))
            {
                commercials = commercials
                    .Where(
                        s => s.Name.Contains(SearchString) 
                        || s.Departement.Name.Contains(SearchString)
                        || s.Departement.Code.ToString().Contains(SearchString)
                    );
            }
            Commercial = await commercials.ToListAsync();
        }
    }
}
