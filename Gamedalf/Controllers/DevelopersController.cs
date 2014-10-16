using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.ViewModels;
using Microsoft.Ajax.Utilities;
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
        private readonly DeveloperService         _developers;
        private readonly PlayerService            _players;
        private readonly ApplicationUserManager   _userManager;
        private          ApplicationSignInManager _signInManager;
        public ApplicationSignInManager           SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        public DevelopersController(ApplicationUserManager userManager, DeveloperService developers, PlayerService players)
        {
            _userManager = userManager;
            _developers  = developers;
            _players     = players;
        }

        public DevelopersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, DeveloperService developers, PlayerService _players)
        {
            _userManager   = userManager;
            SignInManager = signInManager;
            this._developers    = developers;
            this._players       = _players;
        }

        // GET: Developers
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await _developers.Search(q))
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
            Developer developer = await _developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        //
        // GET: Developers/Register
        [Authorize(Roles = "player")]
        public ActionResult Become()
        {
            return View();
        }

        //
        // GET: Developers/Terms
        public ActionResult Terms()
        {
            return View(new DeveloperAcceptTermsViewModel
            {
                AcceptTerms = false,
            });
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles="player")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Become(DeveloperAcceptTermsViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            //var player = await _players.Find(User.Identity.GetUserId());
            //var developer = await _developers.Convert(player);
            //var result = await _userManager.AddToRoleAsync(developer.Id, "player");
            //result = await _userManager.AddToRoleAsync(developer.Id, "developer");

            //return RedirectToAction("Index", "Home");

            throw new NotImplementedException();
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
                _developers.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
