using System.ComponentModel.DataAnnotations;

namespace DashboardManager.Models
{
    public class Departement
    {
        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Numéro de departement")]
        public int Code { get; set; }
    }
}
