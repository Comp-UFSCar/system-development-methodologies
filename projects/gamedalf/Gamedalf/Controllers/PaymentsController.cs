using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using System.Threading.Tasks;
using Gamedalf.Services;
using PagedList;
using Microsoft.AspNet.Identity;
using Gamedalf.ViewModels;

namespace Gamedalf.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly PaymentService      _payments;
        private readonly SubscriptionService _subscriptions;
        private readonly TermsService        _terms;
        
        public PaymentsController(PaymentService payments, SubscriptionService subscriptions, TermsService terms)
        {
            _payments      = payments;
            _subscriptions = subscriptions;
            _terms         = terms;
        }

        // GET: Payments
        [Authorize(Roles = "employee")]
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await _payments.Search(q))
                .ToPagedList(page, size);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }

            return View(list);
        }

        // GET: Payments/My
        [Authorize(Roles = "player")]
        public async Task<ActionResult> My(int page = 1, int size = 10)
        {
            
            var list = (await _payments.ByUser(User.Identity.GetUserId()))
                .ToPagedList(page, size);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }

            return View(list);
        }

        // GET: Payments/Terms
        [Authorize(Roles = "player")]
        public async Task<ActionResult> Terms()
        {
            return View(new AcceptTermsViewModel
            {
                AcceptTerms = false,
                Terms       = await _terms.Latest("Subscription")
            });
        }

        // POST: Payments/Make
        [HttpPost]
        [Authorize(Roles = "player")]
        public async Task<ActionResult> Make(AcceptTermsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Terms = await _terms.Latest("Subscription");
                return View("Terms", model);
            }

            var latest = _subscriptions.Latest();

            // no subscripts were registered in the database
            if (latest == null)
            {
                return View("_error", new HandleErrorInfo(
                        new NullReferenceException("_subscriptions.Latest() has returned an null value."),
                        "payments",
                        "create"));
            }

            return View(new PaymentMakeViewModel
            {
                LatestSubscription = latest
            });
        }
    }
}
