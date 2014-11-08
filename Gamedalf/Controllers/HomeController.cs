using Gamedalf.Services;
using Gamedalf.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gamedalf.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameService _games;

        public HomeController(GameService games)
        {
            _games = games;
        }

        public async Task<ActionResult> Index(int games = 10)
        {
            return View(new HomeViewModel
            {
                MostRecent     = await _games.Recent(games),
                MostDownloaded = await _games.MostDownloaded(games)
            });
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}