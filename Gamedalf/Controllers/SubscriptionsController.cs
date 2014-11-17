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

namespace Gamedalf.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly SubscriptionService _subscriptions;

        public SubscriptionsController(SubscriptionService subscriptions)
        {
            _subscriptions = subscriptions;
        }
        // GET: Subscriptions
        public async Task<ActionResult> Index(int page = 1, int size = 10)
        {
            var list = (await _subscriptions.All())
                .ToPagedList(page, size);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }

            return View(list);
        }


        // GET: Subscriptions/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subscriptions/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubscriptionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subscription = new Subscription
                {
                    Cost = model.Cost,
                };

                await _subscriptions.Add(subscription);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _subscriptions.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
