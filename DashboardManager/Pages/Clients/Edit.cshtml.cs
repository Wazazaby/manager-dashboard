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

namespace DashboardManager.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly DashboardManager.Data.DashboardManagerContext _context;

        // Liste des départements
        public List<SelectListItem> Departements { get; set; }
        [BindProperty]
        public Client Client { get; set; }
        [BindProperty]
        public int? SelectedDepartementId { get; set; }

        public EditModel(DashboardManager.Data.DashboardManagerContext context)
        {
            _context = context;
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

        /**
         * Séléctionner le departement par defaut du client
         * @param id: id du departement
         * @return id departement
         */
        private int SelectDepartementByClient(int id)
        {
            SelectedDepartementId = id;
            return (int)SelectedDepartementId;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PopulateDepartementSelect();
            Client = await _context.Client.FirstOrDefaultAsync(m => m.Id == id);
            SelectDepartementByClient(Client.Departement.Id);

            if (Client == null)
            {
                return NotFound();
            }
            return Page();
        }

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
                return Page();
            }

            _context.Attach(Client).State = EntityState.Modified;

            try
            {
                await MapSelectedDepartementToClient();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(Client.Id))
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

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
