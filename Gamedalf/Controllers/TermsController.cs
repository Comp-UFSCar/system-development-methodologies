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
    [Authorize(Roles = "admin")]
    public class TermsController : Controller
    {
        private readonly TermsService _terms;

        public TermsController(TermsService terms)
        {
            _terms = terms;
        }

        // GET: Terms
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await _terms.Search(q))
                .ToPagedList(page, size);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }

            return View(list);
        }


        // GET: Terms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terms terms = await _terms.Find(id);
            if (terms == null)
            {
                return HttpNotFound();
            }
            return View(terms);
        }

        // GET: Terms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Terms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TermsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var terms = await _terms.Add(new Terms
                {
                    Title      = model.Title,
                    Content    = model.Content,
                    EmployeeId = User.Identity.GetUserId()
                });

                return RedirectToAction("Details", new { id = terms.Id });
            }

            return View(model);
        }

        // GET: Terms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terms terms = await _terms.Find(id);
            if (terms == null)
            {
                return HttpNotFound();
            }
            return View(terms);
        }

        // POST: Terms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TermsEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var terms = await _terms.Find(model.Id);

                terms.Title      = model.Title;
                terms.Content    = model.Content;
                terms.EmployeeId = User.Identity.GetUserId();

                terms = await _terms.Add(terms);
                return RedirectToAction("Details", new { Id = terms.Id });
            }
            return View(model);
        }

        // GET: Terms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terms terms = await _terms.Find(id);
            if (terms == null)
            {
                return HttpNotFound();
            }
            return View(terms);
        }

        // POST: Terms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _terms.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _terms.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
