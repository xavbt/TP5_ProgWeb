using TP5.Models;

namespace TP5.Areas.Admin.ViewModels
{
    public class ReservationDeleteVM
    {
        public Choix Choix { get;}
        public Reservations Reservation { get; }

        public string ChoixName
        {
            get
            {
                return Choix?.Description ?? string.Empty;
            }
        }
        public ReservationDeleteVM(Reservations reservation ,Choix choix)
        {
            Reservation = reservation;
            Choix = choix;
        }
    }
}
