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

        public IList<Commercial> Commercial { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Name { get; set; }

        public async Task OnGetAsync()
        {
            // Utilisation de LINQ pour faire notre requête
            var commercials = from c in _context.Commercial select c;
            if (!string.IsNullOrEmpty(SearchString))
            {
                commercials = commercials.Where(s => s.Name.Contains(SearchString));
            }
            Commercial = await commercials.ToListAsync();
        }
    }
}
