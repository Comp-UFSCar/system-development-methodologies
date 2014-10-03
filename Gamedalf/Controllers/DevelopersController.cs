using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.ViewModels;
using PagedList;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;

namespace Gamedalf.Controllers
{
    public class DevelopersController : Controller
    {
        private readonly DeveloperService         developers;
        private readonly PlayerService            players;
        private readonly ApplicationUserManager   UserManager;
        private          ApplicationSignInManager _signInManager;
        public ApplicationSignInManager           SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        public DevelopersController(ApplicationUserManager userManager, DeveloperService _developers, PlayerService _players)
        {
            UserManager = userManager;
            developers  = _developers;
            players     = _players;
        }

        public DevelopersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, DeveloperService _developers, PlayerService _players)
        {
            UserManager   = userManager;
            SignInManager = signInManager;
            developers    = _developers;
            players       = _players;
        }

        // GET: Developers
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await developers.Search(q))
                .ToPagedList(page, size);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }

            return View(list);
        }

        // GET: Developers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = await developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        //
        // GET: Developers/Register
        [Authorize(Roles = "player")]
        public ActionResult Register()
        {
            return View(new DeveloperRegisterViewModel
            {
                AcceptTerms = false
            });
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles="player")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(DeveloperRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var player    = await players.Find(User.Identity.GetUserId());
            var developer = await developers.Convert(player);
            var result    = await UserManager.AddToRoleAsync(developer.Id, "player");
                result    = await UserManager.AddToRoleAsync(developer.Id, "developer");

            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                developers.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
