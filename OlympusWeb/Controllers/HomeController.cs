using System.Linq;
using System.Web.Mvc;
using OlympusWeb.Data;

namespace OlympusWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ActionResult Index()
        {
            var result = _dataContext.GetAllNews().OrderByDescending(x => x.Order).ThenByDescending(x => x.Date).Take(30);
            ViewBag.News = result; 
            return View();
        }
    }
}
