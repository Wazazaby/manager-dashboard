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

namespace DashboardManager.Pages.Commercials
{
    public class CreateModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public CreateModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }


        public List<SelectListItem> Departements { get; set; }
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
        public List<SelectListItem> Clients { get; set; }
        private void PopulateClientSelect()
        {
            IQueryable<Client> clientQuery = from c in _context.Client select c;
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (Client client in clientQuery.ToList())
            {
                listItems.Add(new SelectListItem(client.Name, client.Id.ToString()));
            }
            Clients = listItems;
        }


        public IActionResult OnGet()
        {
            // À l'initialisation, on va chercher les valeur qui peupleuront les selects dans notre template
            PopulateDepartementSelect();
            PopulateClientSelect();
            return Page();
        }

        // Binding de la data du commercial
        [BindProperty]
        public Commercial Commercial { get; set; }
        // Binding du departement du commercial qui est traité à part
        // Nullable dans le cas où l'utilisateur de remplit pas le champ
        [BindProperty]
        public int? SelectedDepartementId { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid && SelectedDepartementId == null)
            {
                return OnGet();
            }

            // Ajout du département sélectionné au commercial
            await MapSelectedDepartementToCommercial();
            _context.Commercial.Add(Commercial);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task<bool> MapSelectedDepartementToCommercial()
        {
            // Récupération de l'objet du département correspondant à notre id
            IQueryable<Departement> departmentQuery =
                from d in _context.Departement
                where d.Id == SelectedDepartementId
                select d;
            Commercial.Departement = await departmentQuery.FirstOrDefaultAsync();
            return true;
        }
    }
}
