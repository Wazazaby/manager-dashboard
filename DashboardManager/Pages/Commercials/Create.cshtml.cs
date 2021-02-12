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

        // Liste contenant les options de départements
        public List<SelectListItem> Departements { get; set; }
        private void PopulateDepartementSelect()
        {
            List<SelectListItem> listDep = new List<SelectListItem>();
            foreach (Departement dep in _context.Departement.ToList())
            {
                listDep.Add(new SelectListItem(dep.Name, dep.Id.ToString()));
            }
            Departements = listDep;
        }

        // List contenant les options de clients
        public SelectList Clients { get; set; }
        private void PopulateClientSelect()
        {
            Clients = new SelectList(_context.Client.ToList(), nameof(Client.Id), nameof(Client.Name));
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
        // Nullable dans le cas où l'utilisateur ne choisit pas de département
        [BindProperty]
        public int? SelectedDepartementId { get; set; }
        [BindProperty]
        public int[] SelectedClientsIds { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid && SelectedDepartementId == null)
            {
                return OnGet();
            }

            // Ajout du département sélectionné au commercial
            await MapSelectedDepartementToCommercial();
            // Ajout des clients sélectionnés au commercial
            await MapSelectedClientsToCommercial();
            // Ajout du commercial au context et sauvegarde de manière asynchrone
            _context.Commercial.Add(Commercial);
            await _context.SaveChangesAsync();

            // Redirection vers la liste des commerciaux
            return RedirectToPage("./Index");
        }

        private async Task<bool> MapSelectedDepartementToCommercial()
        {
            // Récupération de l'objet du département correspondant à notre id
            Commercial.Departement = await _context.Departement
                .Where(d => d.Id == SelectedDepartementId)
                .FirstOrDefaultAsync();
            return true;
        }

        private async Task<bool> MapSelectedClientsToCommercial()
        {
            // Si l'utilisateur à chosit des clients pour ce commercial, on lui les ajoute
            if (SelectedClientsIds.Length > 0)
            {
                Commercial.Clients = await _context.Client
                    .Where(c => SelectedClientsIds.Contains(c.Id))
                    .ToListAsync();
            }
            return true;
        }
    }
}
