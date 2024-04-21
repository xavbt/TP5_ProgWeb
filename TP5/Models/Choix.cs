using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TP5.Models
{
    public class Choix
    {
        public int Id { get; set; }

        [Display(Name = "Choix")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Le choix est requis.")]
        [StringLength(30, ErrorMessage = "Le choix ne doit pas avoir plus de {1} caractères.")]
        public string Description { get; set; } = string.Empty;

        // Constructeur vide requis pour la désérialisation
        public Choix()
        {
        }

        public Choix(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
