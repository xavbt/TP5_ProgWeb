using Microsoft.AspNetCore.Mvc;
using TP5.Areas.Admin.ViewModels;
using TP5.DataAccessLayer;
using TP5.Models;
namespace TP5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        public IActionResult List()
        {
            DAL dal = new DAL();

            ReservationListVM viewModel = new ReservationListVM(
                dal.ChoixFact.GetAll(),
                dal.ReservationFact.GetAll()); // new Product[0], // Pour tester l'affichage de "Aucun produit trouvé."

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                DAL dal = new DAL();
                Reservations? reservation = dal.ReservationFact.Get(id);

                if (reservation != null)
                {
                    ReservationEditVM viewModel = new ReservationEditVM(
                        reservation,
                        dal.ChoixFact.GetAll());

                    return View("CreateEdit", viewModel);
                }
            }

            return View("AdminMessage", new AdminMessageVM("L'identifiant de réservation est introuvable."));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ReservationEditVM viewModel)
        {
            if (viewModel != null && viewModel.Reservation != null)
            {
                DAL dal = new DAL();

                Reservations? existingReservation = dal.ReservationFact.Get(viewModel.Reservation.Id);
                if (existingReservation != null && existingReservation.Id != viewModel.Reservation.Id)
                {
                    ModelState.AddModelError("Product.Code", "L'ID de réservation existe déjà.");
                }

                if (!ModelState.IsValid)
                {
                    viewModel.Choix = dal.ChoixFact.GetAll();
                    return View("CreateEdit", viewModel);
                }

                dal.ReservationFact.Save(viewModel.Reservation);
            }

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                DAL dal = new DAL();
                Reservations? reservation = dal.ReservationFact.Get(id);

                if (reservation != null)
                {
                    ReservationDeleteVM viewModel = new ReservationDeleteVM(
                       reservation,
                        dal.ChoixFact.Get(reservation.MenuChoiceId));

                    return View(viewModel);
                }
            }

            return View("AdminMessage", new AdminMessageVM("L'ID de réservation est introuvable."));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            if (id > 0)
            {
                new DAL().ReservationFact.Delete(id);
            }

            return RedirectToAction("List");
        }
    }
}
