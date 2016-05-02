namespace App.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.ApplicationName = "appFlightInfo";

            return View();
        }
    }
}
