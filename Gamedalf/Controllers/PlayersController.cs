using Gamedalf.Core.Models;
using Gamedalf.Services;
using PagedList;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gamedalf.Controllers
{
    public class PlayersController : Controller
    {
        private readonly PlayerService            players;
        private readonly ApplicationUserManager   UserManager;

        public PlayersController(ApplicationUserManager _userManager, PlayerService _players)
        {
            UserManager   = _userManager;
            players       = _players;
        }

        // GET: Players
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await players.Search(q))
                .ToPagedList(page, size);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }

            return View(list);
        }

        // GET: Players/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = await players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                players.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
