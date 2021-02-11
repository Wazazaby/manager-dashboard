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
    public class DetailsModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public List<Client> clientsArray{ get; set; }
        public IList<Client> Clients { get; set; }
        public IList<Departement> Departements { get; set; }

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

            IQueryable<Client> clientQuery = from c in _context.Client select c;
            Clients = await clientQuery.ToListAsync();

            IQueryable<Departement> departementQuery = from m in _context.Departement orderby m.Name select m;
            Departements = await departementQuery.Distinct().ToListAsync();

            Commercial = await _context.Commercial.FirstOrDefaultAsync(m => m.Id == id);

            if (Commercial == null)
            {
                return NotFound();
            }

            if (Commercial.Clients != null && Commercial.Clients.Count > 0)
            {
                Commercial.Clients.ForEach((c) =>
                {
                    clientsArray = Commercial.Clients;
                });
            } else
            {
                clientsArray = new List<Client>();
            }


            return Page();
        }

    }
}
