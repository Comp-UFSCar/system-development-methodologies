using Gamedalf.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace Gamedalf.Controllers.Api
{
    public class GamesController : ApiController
    {
        private GameService _games;

        public GamesController(GameService games)
        {
            _games = games;
        }

        // GET: api/Games
        public async Task<IHttpActionResult> Get()
        {
            var games = await _games.All();
            if (games == null)
            {
                return NotFound();
            }

            return Ok(games);
        }

        // GET: api/Games/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var game = await _games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }
    }
}
