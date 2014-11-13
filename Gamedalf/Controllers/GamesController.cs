using Gamedalf.Core.Models;
using Gamedalf.Infrastructure.Games;
using Gamedalf.Services;
using Gamedalf.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Net;
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

        // GET: Games/My
        [Authorize(Roles = "player")]
        public async Task<ActionResult> My(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await _games.RegisteredByUser(User.Identity.GetUserId(), q))
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
        [Authorize(Roles = "developer")]
        public async Task<ActionResult> Create(GameCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Game game = new Game
                {
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    DeveloperId = User.Identity.GetUserId(),
                    Secret = Guid.NewGuid().ToString()
                };

                await _games.Add(game);
                return RedirectToAction("Images", new { id = game.Id });
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
                Id = game.Id,
                Title = game.Title,
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

                game.Title = model.Title;
                game.Description = model.Description;

                await _games.Update(game);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "developer,employee")]
        public async Task<ActionResult> Images(int? id)
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

            // assert that game sought belongs to the developer manipulating it
            // or that the loggedin user is an Employee
            if (game.Developer.Id != User.Identity.GetUserId() && !User.IsInRole("employee"))
            {
                return new HttpUnauthorizedResult();
            }

            return View(new GameImagesViewModel
            {
                Id    = game.Id,
                Title = game.Title,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "developer,employee")]
        public async Task<ActionResult> Images(GameImagesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Images", model);
            }

            // assert that game sought belongs to the developer manipulating it
            // or that the loggedin user is an Employee
            var game = await _games.Find(model.Id);
            if (game.Developer.Id != User.Identity.GetUserId() && !User.IsInRole("employee"))
            {
                return new HttpUnauthorizedResult();
            }

            // Instanciates a handler to save all files in the appropriate game's folder
            try
            {
                new GameImagesHandler(model.Id, model.Cover, model.Images, model.Override)
                    .SaveAll();
            }
            catch (Exception e)
            {
                return View("_Error", new HandleErrorInfo(e, "Games", "Images"));
            }

            // Ok
            return RedirectToAction("Details", new { id = model.Id });
        }

        [Authorize(Roles = "developer")]
        public async Task<ActionResult> Upload(int? id)
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

            // assert that game sought belongs to the developer manipulating it
            if (game.Developer.Id != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            return View(new GameBinaryViewModel
            {
                Id    = game.Id,
                Title = game.Title,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "developer")]
        public async Task<ActionResult> Upload(GameBinaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // assert that game sought belongs to the developer manipulating it
            var game = await _games.Find(model.Id);
            if (game.Developer.Id != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            // Instanciates a handler to save the installer in the appropriate game's folder
            try
            {
                new GameBinariesHandler(model.Id, model.Binary, model.Override)
                    .SaveAll();
            }
            catch (Exception e)
            {
                return View("_Error", new HandleErrorInfo(e, "Games", "Images"));
            }

            // Ok
            return RedirectToAction("Details", new { id = model.Id });
        }

        // GET: Games/Delete/5
        [Authorize(Roles = "employee,admin")]
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
