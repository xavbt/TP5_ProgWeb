using TP5.Models;
namespace TP5.Areas.Admin.ViewModels
{
    public class ReservationListVM
    {
        private readonly Dictionary<int, Choix> _ChoixById = new Dictionary<int, Choix>();

        public List<Choix> Choices
        {
            get
            {
                return _ChoixById.Values.ToList();
            }
        }

        public List<Reservations> Reservations { get; }

        public ReservationListVM(List<Choix> choices, List<Reservations> reservations)
        {
            foreach(Choix choice in choices)
            {
                _ChoixById.Add(choice.Id, choice);
            }
            Reservations = reservations;
        }

        public string GetChoixName(int id)
        {
            if (_ChoixById.ContainsKey(id))
            {
                return _ChoixById[id].Description;
            }
            return string.Empty;
        }
    }
}
