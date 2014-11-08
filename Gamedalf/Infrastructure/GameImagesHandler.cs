using System.Collections.Generic;
using System.Web;
using System.Linq;
using System;
using System.IO;

namespace Gamedalf.Infrastructure
{
    public class GameImagesHandler
    {
        private const string BasePath = "~/Images/Games";

        private int _id;
        private HttpPostedFileBase _cover;
        private IEnumerable<HttpPostedFileBase> _artImages;

        private string _directory;
        private bool _override;

        public GameImagesHandler(int id, HttpPostedFileBase cover) : this(id, cover, null, false) { }
        public GameImagesHandler(int id, HttpPostedFileBase cover, IEnumerable<HttpPostedFileBase> artImages) : this(id, cover, artImages, false) { }
        public GameImagesHandler(int id, HttpPostedFileBase cover, IEnumerable<HttpPostedFileBase> artImages, bool @override)
        {
            // assert that id represents an valid game id
            // and the cover is a valid file
            if (id == 0
                || cover == null
                || cover.ContentLength == 0)
            {
                throw new ArgumentException("Cannot construct GameImagesHandler with given arguments");
            }

            _id = id;
            _cover = cover;
            _artImages = artImages;
            _override = @override;

            _directory = Path.Combine(
                HttpContext.Current.Server.MapPath(BasePath),
                _id.ToString()
            );
        }

        /// <summary>
        /// Returns a game's cover path.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        /// <returns>
        /// Returns a path for the game's cover image, if the cover exists.
        /// Otherwise, returns a path for the default cover image.
        /// </returns>
        public static String PathForCoverOf(int id)
        {
            // translates relative path into absolute
            var gamePath = Path.Combine(HttpContext.Current.Server.MapPath(BasePath), id.ToString(), "cover.jpg");

            // if absolute path exists, returns relative path for the cover
            return File.Exists(gamePath)
                ? Path.Combine(BasePath, id.ToString(), "cover.jpg")
                : Path.Combine(BasePath, "cover.jpg");
        }

        public static ICollection<String> ArtImagesOf(int id)
        {
            var artImages = new List<String>();

            var path = Path.Combine(HttpContext.Current.Server.MapPath(BasePath), id.ToString());
            
            if (Directory.Exists(path))
            {
                var numberOfArtImages = Directory.GetFiles(path).Length - 1;

                for (var image = 0; image < numberOfArtImages; image++)
                {
                    artImages.Add(ArtImageOf(id, image));
                }
            }

            return artImages;
        }

        public static String ArtImageOf(int id, int index)
        {
            return Path.Combine(BasePath, id.ToString(), index + ".jpg");
        }

        /// <summary>
        /// Save game's cover image inside _directory.
        /// </summary>
        /// <returns>The object GameImageHandler that called this method (self).</returns>
        public GameImagesHandler SaveCover()
        {
            var fileName = "cover" + Path.GetExtension(_cover.FileName);
            var pathToCover = Path.Combine(_directory, fileName);
            _cover.SaveAs(pathToCover);

            return this;
        }

        /// <summary>
        /// Save game's art images inside _directory.
        /// </summary>
        /// <returns>The object GameImageHandler that called this method (self).</returns>
        public virtual GameImagesHandler SaveArt()
        {
            if (_artImages != null)
            {
                int index = 0;

                foreach (var art in _artImages)
                {
                    if (art == null) continue;

                    var fileName = index++ + Path.GetExtension(_cover.FileName);
                    var pathToArt = Path.Combine(_directory, fileName);
                    art.SaveAs(pathToArt);
                }
            }

            return this;
        }

        /// <summary>
        /// Creates (or clean, when _override equals true) directory _directory and saves cover and art images.
        /// </summary>
        /// <returns>The object GameImageHandler that called this method (self).</returns>
        public virtual GameImagesHandler SaveAll()
        {
            return SaveDirectory().SaveCover().SaveArt();
        }

        protected virtual GameImagesHandler SaveDirectory()
        {
            if (Directory.Exists(_directory) && _override)
            {
                Directory.Delete(_directory, true);
            }

            Directory.CreateDirectory(_directory);

            return this;
        }
    }
}
