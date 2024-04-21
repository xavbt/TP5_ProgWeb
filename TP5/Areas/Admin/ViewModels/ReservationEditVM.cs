using TP5.Models;

namespace TP5.Areas.Admin.ViewModels
{
    public class ReservationEditVM
    {
        public Reservations Reservation { get; set; } = new Reservations();
        public List<Choix>? Choix { get; set; }

        // Constructeur vide requis pour la désérialisation
        public ReservationEditVM()
        {
        }

        public ReservationEditVM(Reservations reservation, List<Choix> choix)
        {
            Reservation = reservation;
            Choix = choix;
        }
    }
}
