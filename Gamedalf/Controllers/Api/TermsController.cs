using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services;

namespace Gamedalf.Controllers.Api
{
    public class TermsController : ApiController
    {
        private readonly TermsService _terms;

        public TermsController(TermsService terms)
        {
            _terms = terms;
        }

        // GET: api/Terms
        public async Task<ICollection<Terms>> GetTerms()
        {
            return await _terms.All();
        }

        // GET: api/Terms/5
        [ResponseType(typeof(Terms))]
        public async Task<IHttpActionResult> GetTerms(int id)
        {
            Terms terms = await _terms.Find(id);
            if (terms == null)
            {
                return NotFound();
            }

            return Ok(terms);
        }

        [ResponseType(typeof(Terms))]
        public async Task<IHttpActionResult> GetTerms(string title)
        {
            var terms = await _terms.Latest(title);
            if (terms == null)
            {
                return NotFound();
            }

            return Ok(terms);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _terms.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<bool> TermsExists(int id)
        {
            return await _terms.Exists(id);
        }
    }
}