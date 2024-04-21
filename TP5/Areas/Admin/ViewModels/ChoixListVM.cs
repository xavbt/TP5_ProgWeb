using TP5.Models;
namespace TP5.Areas.Admin.ViewModels
{
    public class ChoixListVM
    {
        private readonly Dictionary<int, Choix> _choixByID = new Dictionary<int, Choix>();

        public List<Choix> Choices
        {
            get
            {
                return _choixByID.Values.ToList();
            }
        }
        public ChoixListVM(List<Choix> choices)
        {
            foreach(Choix choice in choices)
            {
                _choixByID.Add(choice.Id, choice);
            }
        }

        public string GetChoixDescription(int id)
        {
            if (_choixByID.ContainsKey(id))
            {
                return _choixByID[id].Description;
            }
            return string.Empty;
        }
    }
}
