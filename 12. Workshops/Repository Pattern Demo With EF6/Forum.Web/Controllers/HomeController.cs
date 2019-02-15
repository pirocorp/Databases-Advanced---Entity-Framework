namespace Forum.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dbContext = new ForumDbContext();

            var categories = dbContext.Categories.ToList();
            this.ViewBag.Categories = categories;

            return this.View(categories);
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}