using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using PagedList;
using Gamedalf.ViewModels;
using Microsoft.AspNet.Identity;

namespace Gamedalf.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameService games;

        public GamesController(GameService _games)
        {
            games = _games;
        }

        // GET: Games
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await games.Search(q))
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
            Game game = await games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles="developer")]
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
                
                await games.Add(game);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Games/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
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
        public async Task<ActionResult> Edit(GameEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Game game = await games.Find(model.Id);

                game.Title       = model.Title;
                game.Description = model.Description;

                await  games.Update(game);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Games/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await games.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                games.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
