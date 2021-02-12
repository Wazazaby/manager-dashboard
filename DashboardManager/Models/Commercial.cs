using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DashboardManager.Models
{
    public class Commercial
    {
        // ID
        public int Id { get; set; }

        // NOM
        [Required(ErrorMessage = "Champ obligatoire")]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        // ZONE
        [Required(ErrorMessage = "Champ obligatoire")]
        [Display(Name = "Zone de chalandise")]
        public string CatchmentArea { get; set; }

        // CLIENTS
        [Display(Name = "Nb clients")]
        public List<Client> Clients { get; set; }

        // DEVIS
        [Required(ErrorMessage = "Champ obligatoire")]
        [Display(Name = "Nb devis")]
        public int NbQuotes { get; set; }

        // CONTRATS
        [Required(ErrorMessage = "Champ obligatoire")]
        [Display(Name = "Nb contrats")]
        public int NbContracts { get; set; }

        // DATE
        [Required(ErrorMessage = "Champ obligatoire")]
        [Display(Name = "Arrivée dans l'entreprise")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        // DEPARTEMENT
        [Required(ErrorMessage = "Champ obligatoire")]
        public Departement Departement { get; set; }
    }
}
