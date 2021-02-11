using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DashboardManager.Data;
using DashboardManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DashboardManager.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public CreateModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        // Liste des départements
        public List<SelectListItem> Departements { get; set; }
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

        public IActionResult OnGet()
        {
            PopulateDepartementSelect();
            return Page();
        }

        [BindProperty]
        public Client Client { get; set; }
        [BindProperty]
        public int? SelectedDepartementId { get; set; }

        private async Task<bool> MapSelectedDepartementToClient()
        {
            // Récupération de l'objet du département correspondant à notre id
            IQueryable<Departement> departmentQuery =
                from d in _context.Departement
                where d.Id == SelectedDepartementId
                select d;
            Client.Departement = await departmentQuery.FirstOrDefaultAsync();
            return true;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid && SelectedDepartementId == null)
            {
                return OnGet();
            }
            // Ajout du département sélectionné au client
            await MapSelectedDepartementToClient();

            _context.Client.Add(Client);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
