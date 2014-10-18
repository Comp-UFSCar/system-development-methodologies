using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.ViewModels;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Gamedalf.Controllers.Api
{
    public class TermsController : ApiController
    {
        private readonly TermsService _terms;

        public TermsController(TermsService terms)
        {
            _terms = terms;
        }

        // GET: api/Terms/5
        [ResponseType(typeof(TermsJsonViewModel))]
        public async Task<IHttpActionResult> GetTerms(int id)
        {
            Terms terms = await _terms.Find(id);
            if (terms == null)
            {
                return NotFound();
            }

            return Ok(new TermsJsonViewModel
            {
                Id = terms.Id,
                Title = terms.Title,
                Content = terms.Content,
                DateCreated = terms.DateCreated
            });
        }

        [ResponseType(typeof(TermsJsonViewModel))]
        public async Task<IHttpActionResult> GetTerms(string title)
        {
            var terms = await _terms.Latest(title);
            if (terms == null)
            {
                return NotFound();
            }

            return Ok(new TermsJsonViewModel {
                Id          = terms.Id,
                Title       = terms.Title,
                Content     = terms.Content,
                DateCreated = terms.DateCreated
            });
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