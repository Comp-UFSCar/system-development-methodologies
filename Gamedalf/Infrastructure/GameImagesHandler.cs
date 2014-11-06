using System.Collections.Generic;
using System.Web;
using System.Linq;
using System;
using System.IO;

namespace Gamedalf.Infrastructure
{
    public class GameImagesHandler
    {
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
        }

        public GameImagesHandler SaveCover()
        {
            var fileName = "cover" + Path.GetExtension(_cover.FileName);
            var pathToCover = Path.Combine(_directory, fileName);
            _cover.SaveAs(pathToCover);

            return this;
        }

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

        public virtual GameImagesHandler SaveAll()
        {
            return SaveDirectory().SaveCover().SaveArt();
        }

        protected virtual GameImagesHandler SaveDirectory()
        {
            _directory = HttpContext.Current.Server.MapPath("~/App_Data/Games/" + _id + "/");

            if (Directory.Exists(_directory) && _override)
            {
                Directory.Delete(_directory, true);
            }

            Directory.CreateDirectory(_directory);

            return this;
        }
    }
}
