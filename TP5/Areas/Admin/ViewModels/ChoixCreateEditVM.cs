using TP5.Models;
namespace TP5.Areas.Admin.ViewModels
{
    public class ChoixCreateEditVM
    {
        public Choix Choix { get; set; } = new Choix();

        public ChoixCreateEditVM() { }

        public ChoixCreateEditVM(Choix choix)
        {
            Choix = choix;
        }
    }
}
