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

namespace Gamedalf.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly PaymentService _payments;

        /*
        public PaymentsController(GameService games)
        {
            _games = games;
        }*/

        // GET: Payments
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


    }
}
