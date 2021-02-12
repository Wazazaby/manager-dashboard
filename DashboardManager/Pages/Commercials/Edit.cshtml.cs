using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DashboardManager.Data;
using DashboardManager.Models;

namespace DashboardManager.Pages.Commercials
{
    public class EditModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        public EditModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Departements { get; set; }
        private void PopulateDepartementSelect()
        {
            // Ajout des valeurs au select du département
            SelectedDepartementId = Commercial.Departement.Id;
            List<SelectListItem> listDep = new List<SelectListItem>();
            foreach (Departement dep in _context.Departement.ToList())
            {
                listDep.Add(new SelectListItem(dep.Name, dep.Id.ToString(), dep.Id == SelectedDepartementId));
            }
            Departements = listDep;
        }

        public SelectList Clients { get; set; }
        private void PopulateClientSelect()
        {
            // Ajout des valeurs au select des clients
            SelectedClientsIds = Commercial.Clients?.Select(c => c.Id).ToArray();
            Clients = new SelectList(
                _context.Client.ToList().OrderBy(c => c.Name), 
                nameof(Client.Id), 
                nameof(Client.Name), 
                SelectedClientsIds
            );
        }

        // Le commercial qui sera passé à la vue
        [BindProperty]
        public Commercial Commercial { get; set; }

        // La liste des clients du commercial
        [BindProperty]
        public int[] SelectedClientsIds { get; set; }

        // Le département du commercial
        [BindProperty]
        public int? SelectedDepartementId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // On recupère le commercial souhaité pour l'affichage en filtrant sur son ID
            Commercial = await _context.Commercial.FirstOrDefaultAsync(c => c.Id == id);
            // On charge son département et ses clients
            await _context.Departement.ToListAsync();
            await _context.Client.ToListAsync();

            if (Commercial == null)
            {
                return NotFound();
            }

            // On créé les listes pour les listes déroulantes
            PopulateDepartementSelect();
            PopulateClientSelect();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid && SelectedDepartementId == null)
            {
                return await OnGetAsync(Commercial.Id);
            }

            // On charge la liste des clients
            await _context.Client.ToListAsync();
            // On passe le statut du commercial comme modifié
            _context.Attach(Commercial).State = EntityState.Modified;

            try
            {
                // On lui assigne son nouveau département ainsi que ses clients
                await MapSelectedDepartementToCommercial();
                await MapSelectedClientsToCommercial();
                // On enregistre
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommercialExists(Commercial.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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
            // Si le commercial a déjà un ou plusieurs clients
            if (Commercial.Clients != null && Commercial.Clients?.Count > 0)
            {
                // Si des clients ont été sélectionnés
                if (SelectedClientsIds.Length > 0)
                {
                    // Pour chaque client, on va voir si il se trouve dans la liste des clients sélectionnés
                    // Si ce n'est pas le cas, on le détache du commercial
                    foreach (Client c in Commercial.Clients.ToList())
                    {
                        if (!SelectedClientsIds.Contains(c.Id))
                        {
                            Commercial.Clients.Remove(c);
                        }
                    }
                }
                // Si aucun client n'est sélectionné, alors on les détache tous du commercial
                else
                {
                    Commercial.Clients.Clear();
                }
            }
            // Si l'utilisateur à chosit des clients pour ce commercial, on lui les ajoute
            if (SelectedClientsIds.Length > 0)
            {
                Commercial.Clients = await _context.Client
                    .Where(c => SelectedClientsIds.Contains(c.Id))
                    .ToListAsync();
            }
            return true;
        }

        private bool CommercialExists(int id)
        {
            return _context.Commercial.Any(e => e.Id == id);
        }
    }
}
