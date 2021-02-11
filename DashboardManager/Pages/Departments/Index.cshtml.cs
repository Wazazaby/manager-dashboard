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
    public class IndexModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public IndexModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        public IList<Departement> Departement { get;set; }

        public async Task OnGetAsync()
        {
            Departement = await _context.Departement
                .OrderBy(d => d.Name)
                .ToListAsync();
        }
    }
}
