using TP5.Models;

namespace TP5.Areas.Admin.ViewModels
{
    public class ChoixDeleteVM
    {
        public Choix Choix { get; set; }

        public ChoixDeleteVM(Choix choix)
        {
            Choix = choix;
        }
    }
}
