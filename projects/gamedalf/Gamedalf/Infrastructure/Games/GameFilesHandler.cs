using System;
using System.IO;
using System.Web;

namespace Gamedalf.Infrastructure.Games
{
    public abstract class GameFilesHandler
    {
        protected int    _id;
        protected string _directory;
        protected bool   _override;

        public GameFilesHandler(int id, string directory) : this(id, directory, false) { }

        public GameFilesHandler(int id, string directory, bool @override)
        {
            // assert that id represents an valid game id
            // and the cover is a valid file
            if (id == 0
                || String.IsNullOrEmpty(directory))
            {
                throw new ArgumentException("Cannot construct GameFilesHandler with given arguments:\n"
                    + "\n\tid = " + id.ToString() + "\n\t directory = " + directory);
            }

            _id        = id;
            _override  = @override;
            _directory = MapGamePath(directory, id);
        }

        protected virtual GameFilesHandler SaveDirectory()
        {
            if (Directory.Exists(_directory) && _override)
            {
                Directory.Delete(_directory, true);
            }

            Directory.CreateDirectory(_directory);
            return this;
        }

        protected static string MapGamePath(string basePath, int id)
        {
            return Path.Combine(
                HttpContext.Current.Server.MapPath(basePath),
                id.ToString());
        }
    }
}