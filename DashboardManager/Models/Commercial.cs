using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DashboardManager.Models
{
    public class Commercial
    {
        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Zone de chalandise")]
        public string CatchmentArea { get; set; }
        public List<Client> Clients { get; set; }
        [Display(Name = "Nombre de devis")]
        public int NbQuotes { get; set; }
        [Display(Name = "Nombre de contrats")]
        public int NbContracts { get; set; }
        [Display(Name = "Date d'arrivée dans l'entreprise")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public Departement Departement { get; set; }
    }
}
