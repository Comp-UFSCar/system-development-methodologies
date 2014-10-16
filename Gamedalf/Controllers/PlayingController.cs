using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gamedalf.Controllers
{
    [Authorize(Roles = "player")]
    public class PlayingController : Controller
    {
        private readonly PlayingService _playings;

        public PlayingController(PlayingService playings)
        {
            _playings = playings;
        }

        // GET: Playing
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await _playings.PlayingDoneByUser(User.Identity.GetUserId(), q))
                .ToPagedList(page, size);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }

            return View(list);
        }

        // GET: Playing/Evalute
        public async Task<ActionResult> Evaluate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var playing = await _playings.Find(id);
            if (playing == null)
            {
                return HttpNotFound();
            }

            var model = new EvaluationViewModel
            {
                Id = playing.Id,
                GameTitle = playing.Game.Title,
                PlayerEmail = playing.Player.Email,
                Review = playing.Review,
                Score  = (short) (playing.Score ?? 0)
            };

            return View(model);
        }

        // POST: Playing/Evaluate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Evaluate(EvaluationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var playing = new Playing
                {
                    Id = model.Id,
                    Review = model.Review,
                    Score = model.Score
                };

                playing = await _playings.Evaluate(playing);
                return RedirectToAction("Details", new { id = playing.Id });
            }

            return View(model);
        }
    }
}
