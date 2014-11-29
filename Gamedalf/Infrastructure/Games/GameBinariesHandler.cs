using Gamedalf.Infrastructure.Exceptions;
using System;
using System.IO;
using System.Web;

namespace Gamedalf.Infrastructure.Games
{
    public class GameBinariesHandler : GameFilesHandler
    {
        private const string BasePath = "~/GamesBinaries";
        
        private HttpPostedFileBase _binary;

        public GameBinariesHandler(int id) : this(id, null) { }
        
        public GameBinariesHandler(int id, HttpPostedFileBase binary) : this(id, binary, false) { }

        public GameBinariesHandler(int id, HttpPostedFileBase binary, bool @override) : base(id, BasePath, @override)
        {
            _binary = binary;
        }
        
        public virtual GameBinariesHandler Save()
        {
            if (_binary == null || _binary.ContentLength == 0)
            {
                throw new ApplicationException("Cannot save invalid game binaries = " + _binary);
            }
            
            var file = Path.Combine(_directory, "installer" + Path.GetExtension(_binary.FileName));

            if (File.Exists(file) && !_override)
            {
                throw new FileOverrideException();
            }

            using (var fs = new FileStream(file, FileMode.Create))
            {
                var buffer = new byte[_binary.InputStream.Length];
                _binary.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }
            
            return this;
        }

        public virtual GameBinariesHandler SaveAll()
        {
            SaveDirectory();
            return Save();
        }

        public virtual string Retrieve()
        {
            return Directory.GetFiles(_directory)[0];
        }
    }
}