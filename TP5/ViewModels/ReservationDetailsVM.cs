using TP5.Models;

namespace TP5.ViewModels
{
    public class ReservationDetailsVM
    {
        public Choix Choix { get; }
        public Reservations Reservation { get; }

        public string ChoixName
        {
            get
            {
                return Choix?.Description ?? string.Empty;
            }
        }
        public ReservationDetailsVM(Reservations reservation, Choix choix)
        {
            Reservation = reservation;
            Choix = choix;
        }
    }
}
