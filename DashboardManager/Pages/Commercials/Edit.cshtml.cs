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
            SelectedClientsIds = Commercial.Clients?.Select(c => c.Id).ToArray();
            Clients = new SelectList(_context.Client.ToList(), nameof(Client.Id), nameof(Client.Name), SelectedClientsIds);
        }

        [BindProperty]
        public Commercial Commercial { get; set; }
        [BindProperty]
        public int[] SelectedClientsIds { get; set; }
        [BindProperty]
        public int? SelectedDepartementId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Commercial = await _context.Commercial.FirstOrDefaultAsync(c => c.Id == id);
            await _context.Departement.ToListAsync();
            await _context.Client.ToListAsync();

            if (Commercial == null)
            {
                return NotFound();
            }

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

            await MapSelectedDepartementToCommercial();
            await MapSelectedClientsToCommercial();
            _context.Attach(Commercial).State = EntityState.Modified;

            try
            {
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
