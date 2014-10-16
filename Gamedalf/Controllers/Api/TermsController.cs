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

namespace Gamedalf.Controllers.Api
{
    public class TermsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Terms
        public IQueryable<Terms> GetTerms()
        {
            return db.Terms;
        }

        // GET: api/Terms/5
        [ResponseType(typeof(Terms))]
        public async Task<IHttpActionResult> GetTerms(int id)
        {
            Terms terms = await db.Terms.FindAsync(id);
            if (terms == null)
            {
                return NotFound();
            }

            return Ok(terms);
        }

        // PUT: api/Terms/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTerms(int id, Terms terms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != terms.Id)
            {
                return BadRequest();
            }

            db.Entry(terms).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Terms
        [ResponseType(typeof(Terms))]
        public async Task<IHttpActionResult> PostTerms(Terms terms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Terms.Add(terms);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = terms.Id }, terms);
        }

        // DELETE: api/Terms/5
        [ResponseType(typeof(Terms))]
        public async Task<IHttpActionResult> DeleteTerms(int id)
        {
            Terms terms = await db.Terms.FindAsync(id);
            if (terms == null)
            {
                return NotFound();
            }

            db.Terms.Remove(terms);
            await db.SaveChangesAsync();

            return Ok(terms);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TermsExists(int id)
        {
            return db.Terms.Count(e => e.Id == id) > 0;
        }
    }
}