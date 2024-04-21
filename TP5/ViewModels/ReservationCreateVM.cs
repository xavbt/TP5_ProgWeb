using TP5.Models;

namespace TP5.ViewModels
{
    public class ReservationCreateVM
    {
            public Reservations Reservation { get; set; } = new Reservations();
            public List<Choix>? Choix { get; set; }

            // Constructeur vide requis pour la désérialisation
            public ReservationCreateVM()
            {
            }

            public ReservationCreateVM(Reservations reservation, List<Choix> choix)
            {
                Reservation = reservation;
                Choix = choix;
            }
        }
    }
