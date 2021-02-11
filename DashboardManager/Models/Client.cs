using System.ComponentModel.DataAnnotations;

namespace DashboardManager.Models
{
    public class Client
    {
        // Id
        public int Id { get; set; }

        // Nom
        [Required(ErrorMessage = "Champ obligatoire")]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        // Departement
        [Required(ErrorMessage = "Champ obligatoire")]
        public Departement Departement { get; set; }

        // Ville
        [Required(ErrorMessage = "Champ obligatoire")]
        [Display(Name = "Ville")]
        public string City { get; set; }
    }
}
