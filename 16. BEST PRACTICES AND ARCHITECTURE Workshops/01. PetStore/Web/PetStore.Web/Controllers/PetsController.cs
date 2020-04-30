namespace PetStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Models.Pet;
    using Services;

    public class PetsController : Controller
    {
        private readonly IPetService _petService;

        /// <param name="petService">Dependency injection by ASP.NET Core must be configured in Startup class</param>
        public PetsController(IPetService petService)
        {
            this._petService = petService;
        }

        /// <summary>
        /// /Pets/All
        /// </summary>
        /// <param name="page">Page number start from 1</param>
        public IActionResult All(int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            var allPets = this._petService.All(page);
            var totalPets = this._petService.Total();

            var model = new AllPetsViewModel
            {
                Pets = allPets,
                CurrentPage = page,
                Total = totalPets,
            };

            this.ViewBag.Title = "All Pets";
            return this.View(model);
        }


        /// <summary>
        /// /Pets/Delete
        /// </summary>
        /// <param name="id">Pet id</param>
        public IActionResult Delete(int id)
        {
            var pet = this._petService.Details(id);

            if (pet == null)
            {
                return this.NotFound();
            }

            return this.View(pet);
        }


        /// <summary>
        /// /Pets/ConfirmDelete
        /// </summary>
        /// <param name="id">Pet id</param>
        public IActionResult ConfirmDelete(int id)
        {
            var success = this._petService.Delete(id);

            if (!success)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
