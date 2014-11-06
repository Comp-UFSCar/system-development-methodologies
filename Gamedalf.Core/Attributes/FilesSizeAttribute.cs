using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Gamedalf.Core.Attributes
{
    public class FilesSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FilesSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            foreach (var item in value as IEnumerable<HttpPostedFileBase>)
            {
                if (item != null && item.ContentLength > _maxSize)
                {
                    return false;
                }
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("The file size should not exceed {0}", _maxSize);
        }
    }
}
