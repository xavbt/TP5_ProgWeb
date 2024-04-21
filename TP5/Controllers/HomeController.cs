using Microsoft.AspNetCore.Mvc;
using TP5.Areas.Admin.ViewModels;
using TP5.DataAccessLayer;
using TP5.Models;
using TP5.ViewModels;

namespace TP5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Confirmation(int id)
        {
            if (id > 0)
            {
                DAL dal = new DAL();
                Reservations? reservation = dal.ReservationFact.Get(id);

                if (reservation != null)
                {
                    ReservationDetailsVM viewModel = new ReservationDetailsVM(
                       reservation,
                        dal.ChoixFact.Get(reservation.MenuChoiceId));

                    return View(viewModel);
                }
            }
            return View("VueMessage", new VueMessageVM("L'ID de réservation est introuvable."));
        }

        public IActionResult Index()
        {
            DAL dal = new DAL();

            ReservationCreateVM viewModel = new ReservationCreateVM(
                dal.ReservationFact.CreateEmpty(),
                dal.ChoixFact.GetAll());

            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReservationCreateVM viewModel)
        {
            if (viewModel != null && viewModel.Reservation != null)
            {
                DAL dal = new DAL();

                Reservations? existingReservation = dal.ReservationFact.Get(viewModel.Reservation.Id);
                if (existingReservation != null)
                {
                    // Il est possible d'ajouter une erreur personnalisée.
                    // Le premier paramètre est la propriété touchée (à partir du viewModel ici)

                    ModelState.AddModelError("Reservation.Id", "L'ID de réservation existe déjà.");
                }

                if (!ModelState.IsValid)
                {
                    // Si le modèle n'est pas valide, on retourne à la vue CreateEdit où les messages seront affichés.
                    // Le ViewModèle reçu en POST n'est pas complet (seulement les info dans le <form> sont conservées),
                    // il faut donc réaffecter les Catégories.

                    viewModel.Choix = dal.ChoixFact.GetAll();
                    return View("Create", viewModel);
                }

                dal.ReservationFact.Save(viewModel.Reservation);
            }

            return RedirectToAction("Confirmation", new {id = viewModel.Reservation.Id});
        }
    }
}
