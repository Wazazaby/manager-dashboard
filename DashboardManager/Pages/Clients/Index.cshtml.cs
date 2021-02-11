using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DashboardManager.Data;
using DashboardManager.Models;

namespace DashboardManager.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public IndexModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        public IList<Client> Client { get; set; }
        public IList<Departement> Departements { get; set; }


        public async Task OnGetAsync()
        {
            IQueryable<Departement> departementQuery = from m in _context.Departement orderby m.Name select m;
            Departements = await departementQuery.Distinct().ToListAsync();

            Client = await _context.Client.ToListAsync();

        }
    }
}
