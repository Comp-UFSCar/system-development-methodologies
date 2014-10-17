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
        private readonly ApplicationUserManager   _userManager;

        public DevelopersController(ApplicationUserManager userManager, DeveloperService developers)
        {
            _userManager = userManager;
            _developers  = developers;
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
            return View(new AcceptTermsViewModel
            {
                AcceptTerms = false,
            });
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles="player")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Become(AcceptTermsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var developer = await _developers.Convert(User.Identity.GetUserId());
            var result    = await _userManager.AddToRoleAsync(developer.Id, "developer");

            return RedirectToAction("Details", "Developers", new { id = developer.Id });
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
