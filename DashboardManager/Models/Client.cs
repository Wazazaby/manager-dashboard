using System.ComponentModel.DataAnnotations;

namespace DashboardManager.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public Departement Departement { get; set; }
        [Display(Name = "Ville")]
        public string City { get; set; }
    }
}
