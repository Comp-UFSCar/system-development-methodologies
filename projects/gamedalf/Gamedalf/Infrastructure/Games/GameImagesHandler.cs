using System.Collections.Generic;
using System.Web;
using System.Linq;
using System;
using System.IO;

namespace Gamedalf.Infrastructure.Games
{
    public class GameImagesHandler : GameFilesHandler
    {
        private const string BasePath = "~/Images/Games";

        private HttpPostedFileBase _cover;
        private IEnumerable<HttpPostedFileBase> _artImages;

        public GameImagesHandler(int id, HttpPostedFileBase cover) : this(id, cover, null) { }
        public GameImagesHandler(int id, HttpPostedFileBase cover, IEnumerable<HttpPostedFileBase> artImages) : this(id, cover, artImages, false) { }
        public GameImagesHandler(int id, HttpPostedFileBase cover, IEnumerable<HttpPostedFileBase> artImages, bool @override)
            : base(id, BasePath, @override)
        {
            _cover     = cover;
            _artImages = artImages;
        }

        /// <summary>
        /// Save game's cover image inside _directory.
        /// </summary>
        /// <returns>The object GameImageHandler that called this method (self).</returns>
        public GameImagesHandler SaveCover()
        {
            if (_cover != null)
            {
                var file = "cover" + Path.GetExtension(_cover.FileName);
                var path = Path.Combine(_directory, file);
                _cover.SaveAs(path);
            }

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
                int index = Directory.GetFiles(_directory).Length - 1;

                foreach (var art in _artImages)
                {
                    if (art == null) continue;

                    var file = index++ + Path.GetExtension(art.FileName);
                    var path = Path.Combine(_directory, file);
                    art.SaveAs(path);
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
            SaveDirectory();
            return SaveCover().SaveArt();
        }

        /// <summary>
        /// Returns a game's cover path.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        /// <returns>
        /// Returns a path for the game's cover image, if the cover exists.
        /// Otherwise, returns a path for the default cover image.
        /// </returns>
        public static String CoverOf(int id)
        {
            // translates relative path into absolute
            var gamePath = Path.Combine(MapGamePath(BasePath, id), "cover.jpg");

            // if absolute path exists, returns relative path for the cover
            // returns default cover image, otherwise.
            return File.Exists(gamePath)
                ? Path.Combine(BasePath, id.ToString(), "cover.jpg")
                : Path.Combine(BasePath, "cover.jpg");
        }

        /// <summary>
        /// Returns a game's art images.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        /// <returns>
        /// Returns a list paths, where which path leads to a artwork of a given game.
        /// If the game has no artwork images, the list is empty.
        /// </returns>
        public static ICollection<String> ArtImagesOf(int id)
        {
            var artImages = new List<String>();

            var path = MapGamePath(BasePath, id);
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

        /// <summary>
        /// Returns a game's art image.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        /// <param name="index">The index of the artwork.</param>
        /// <returns>
        /// Returns a path to the artwork #index of a given
        /// game with id equals <paramref name="id"/>.
        /// </returns>
        public static String ArtImageOf(int id, int index)
        {
            return Path.Combine(BasePath, id.ToString(), index + ".jpg");
        }
    }
}
