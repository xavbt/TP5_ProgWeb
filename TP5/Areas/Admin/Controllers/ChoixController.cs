using Microsoft.AspNetCore.Mvc;
using TP5.Areas.Admin.ViewModels;
using TP5.DataAccessLayer;
using TP5.Models;

namespace TP5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChoixController : Controller
    {
        public IActionResult List()
        {
            DAL dal = new DAL();

            ChoixListVM viewModel = new ChoixListVM(
                dal.ChoixFact.GetAll());
            return View(viewModel);
        }

        public IActionResult Create()
        {
            
                DAL dal = new DAL();

            ChoixCreateEditVM viewmodel = new ChoixCreateEditVM(
                dal.ChoixFact.CreateEmpty());
            return View("CreateEdit", viewmodel);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ChoixCreateEditVM viewModel)
        {
            if (viewModel.Choix != null)
            {
                DAL dal = new DAL();

                Choix? existingChoix = dal.ChoixFact.Get(viewModel.Choix.Id);
                if (existingChoix != null)
                {
                    ModelState.AddModelError("Choix.Id", "Le numéro de menu existe déjà,");
                }
                if (!ModelState.IsValid)
                {
                    return View("CreateEdit", viewModel);
                }

                dal.ChoixFact.Save(viewModel.Choix);
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                DAL dal = new DAL();
                Choix? choix = dal.ChoixFact.Get(id);

                if (choix != null)
                {
                    ChoixCreateEditVM viewModel = new ChoixCreateEditVM(
                        choix);
                    return View("CreateEdit", viewModel);
                }
                
            }

            return View("AdminMessage", new AdminMessageVM("L'identifiant du choix est introuvable."));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ChoixCreateEditVM viewModel)
        {
            if (viewModel != null && viewModel.Choix != null)
            {
                DAL dal = new DAL();

                Choix? existingChoix = dal.ChoixFact.Get(viewModel.Choix.Id);
                if (existingChoix != null && existingChoix.Id != viewModel.Choix.Id)
                {
                    ModelState.AddModelError("Choix.Code", "L'ID de choix existe déjà.");
                }

                if (!ModelState.IsValid)
                {
                    return View("CreateEdit", viewModel);
                }

                dal.ChoixFact.Edit(viewModel.Choix);
            }

            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                DAL dal = new DAL();
                Choix? choix = dal.ChoixFact.Get(id);

                if (choix != null)
                {
                    ChoixDeleteVM viewModel = new ChoixDeleteVM(
                        choix);

                    return View(viewModel);
                }
            }

            return View("AdminMessage", new AdminMessageVM("L'identifiant du choix est introuvable."));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            if (id > 0)
            {
                new DAL().ChoixFact.Delete(id);
            }

            return RedirectToAction("List");
        }

    }
}
