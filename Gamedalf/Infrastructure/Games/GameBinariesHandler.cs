using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Gamedalf.Infrastructure.Games
{
    public class GameBinariesHandler : GameFilesHandler
    {
        private const string BasePath = "~/GamesBinaries";
        
        private HttpPostedFile _binary;
        
        public GameBinariesHandler(int id, HttpPostedFile binary) : this(id, binary, false) { }

        public GameBinariesHandler(int id, HttpPostedFile binary, bool @override) : base(id, BasePath, @override)
        {
            if (binary == null || binary.ContentLength == 0)
            {
                throw new ArgumentException("Cannot construct GameBinariesHandler with given arguments: binary = " + binary);
            }

            _binary = binary;
        }
        
        public virtual GameBinariesHandler Save()
        {
            var file = Path.Combine(_directory, "installer" + Path.GetExtension(_binary.FileName));
            _binary.SaveAs(file);
            
            return this;
        }

        public virtual GameBinariesHandler SaveAll()
        {
            SaveDirectory();
            return Save();
        }
    }
}