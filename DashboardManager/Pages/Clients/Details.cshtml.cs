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

namespace DashboardManager.Pages.Clients
{
    public class DetailsModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public string DepartementName { get; set; }
        public List<SelectListItem> Departements { get; set; }
        public DetailsModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        public Client Client { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            PopulateDepartementSelect();

            if (id == null)
            {
                return NotFound();
            }

            Client = await _context.Client.FirstOrDefaultAsync(m => m.Id == id);

            if (Client == null)
            {
                return NotFound();
            }

            DepartementName = GetNameDepartement();

            return Page();
        }

        public string GetNameDepartement()
        {
            return Client.Departement.Name;
        }


        // Récupére toute les départements
        private void PopulateDepartementSelect()
        {
            IQueryable<Departement> departmentQuery = from d in _context.Departement select d;
            List<SelectListItem> listDep = new List<SelectListItem>();
            foreach (Departement dep in departmentQuery.ToList())
            {
                listDep.Add(new SelectListItem(dep.Name, dep.Id.ToString()));
            }
            Departements = listDep;
        }
    }
}
