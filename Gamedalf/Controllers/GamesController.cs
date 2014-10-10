using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gamedalf.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameService _games;

        public GamesController(GameService games)
        {
            _games = games;
        }

        // GET: Games
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await _games.Search(q))
                .ToPagedList(page, size);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }

            return View(list);
        }

        // GET: Games/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await _games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }            

            return View(game);
        }

        [Authorize(Roles = "developer,admin")]
        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "developer,admin")]
        public async Task<ActionResult> Create(GameCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Game game = new Game
                {
                     Title = model.Title,
                     Description = model.Description,
                     Price = model.Price,
                     DeveloperId = User.Identity.GetUserId()
                };
                
                await _games.Add(game);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Games/Edit/5
        [Authorize(Roles = "developer,employee,admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await _games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            if (game.DeveloperId != User.Identity.GetUserId()
                && !User.IsInRole("employee") && !User.IsInRole("admin"))
            {
                return new HttpUnauthorizedResult();
            }

            return View(new GameEditViewModel
            {
                Id          = game.Id,
                Title       = game.Title,
                Description = game.Description
            });
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "developer,employee,admin")]
        public async Task<ActionResult> Edit(GameEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Game game = await _games.Find(model.Id);

                if (game.DeveloperId != User.Identity.GetUserId()
                && !User.IsInRole("employee") && !User.IsInRole("admin"))
                {
                    return new HttpUnauthorizedResult();
                }

                game.Title       = model.Title;
                game.Description = model.Description;

                await  _games.Update(game);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Games/Delete/5
        [Authorize(Roles="employee,admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await _games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "employee,admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _games.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _games.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
