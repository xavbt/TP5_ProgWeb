using TP5.DataAccessLayer.Factories;

namespace TP5.DataAccessLayer
{
    public class DAL
    {
        private ChoixFactory? _choixFact = null;
        private ReservationFactory? _reservationFact = null;

        public static string? ConnectionString { get; set; }
        public ChoixFactory ChoixFact
        {
            get
            {
                if (_choixFact == null)
                {
                    _choixFact = new ChoixFactory();
                }
                return _choixFact;
            }
        }
        public ReservationFactory ReservationFact
        {
            get
            {
                if (_reservationFact == null)
                {
                    _reservationFact = new ReservationFactory();
                }
                return _reservationFact;
            }
        }
    }
}
