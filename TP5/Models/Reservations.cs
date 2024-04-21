using System.ComponentModel.DataAnnotations;
namespace TP5.Models
{
    public class Reservations
    {
        
        public int Id { get; set; }

        [Display(Name = "Nom")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Le nom est requis.")]
        [StringLength(20, ErrorMessage = "Le nom ne doit pas avoir plus de {1} caractères.")]
        public string Nom { get; set; } = string.Empty;

        [Display(Name = "Courriel")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le courriel est requis.")]
        [StringLength(50, ErrorMessage = "Le courriel ne doit pas avoir plus de {1} caractères.")]
        public string Courriel { get; set; } = string.Empty;

        [Display(Name = "NbPersonne")]
        [Range(1, short.MaxValue, ErrorMessage = "Le nombre de personnes est requis.")]
        [Required(ErrorMessage = "Le nombre de personnes est requis.")]
        public int NbPersonne { get; set; }

        [Display(Name = "DateReservation")]
        [Required(ErrorMessage = "La date de réservation est requise.")]
        public DateTime DateReservation { get; set; }

        [Display(Name = "MenuChoiceId")]
        [Range(1, short.MaxValue, ErrorMessage = "Le code d'identification de menu est requis.")]
        [Required(ErrorMessage = "Le code d'identification de menu est requis.")]
        public int MenuChoiceId { get; set; }

        // Constructeur vide requis pour la désérialisation
        public Reservations()
        {

        }

        public Reservations(int id, string nom, string courriel, int nbPersonne, DateTime dateReservation, int menuChoiceId)
        {
            Id = id;
            Nom = nom;
            Courriel = courriel;
            NbPersonne = nbPersonne;
            DateReservation = dateReservation;
            MenuChoiceId = menuChoiceId;
        }
    }
}
