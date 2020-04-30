namespace PetStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Models.Category;
    using Services;
    using Services.Models.Category;
    using static Data.Models.DataValidation;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        public IActionResult All()
        {
            var categories = this._categoryService
                .All();

            this.ViewBag.Title = "All Categories";

            return this.View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var categoryServiceModel = new CreateCategoryServiceModel()
            {
                Name = model.Name,
                Description = model.Description
            };

            this._categoryService.Create(categoryServiceModel);

            return this.RedirectToAction("All", "Categories");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = this._categoryService.GetById(id);

            if (category.Name == null)
            {
                return this.BadRequest();
            }

            
            if (category.Description == null)
            {
                category.Description = "No description.";
            }

            return this.View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = this._categoryService.GetById(id);

            if (category.Name == null)
            {
                return this.BadRequest();
            }

            return this.View(category);
        }

        [HttpPost]
        public IActionResult Edit(CategoryListingServiceModel model)
        {
            if (!this._categoryService.Exists(model.Id))
            {
                return this.BadRequest();
            }
            
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (model.Name.Length > NameMaxLength)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this._categoryService.Edit(model);

            return this.RedirectToAction("Details", "Categories", new { id = model.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = this._categoryService.GetById(id);

            if (category.Name == null)
            {
                return this.BadRequest();
            }

            if (category.Description == null)
            {
                category.Description = "No description.";
            }

            return this.View(category);
        }

        [HttpPost]
        public IActionResult Delete(CategoryListingServiceModel model)
        {
            var success = this._categoryService.Remove(model.Id);

            if (!success)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("All", "Categories");
        }
    }
}
