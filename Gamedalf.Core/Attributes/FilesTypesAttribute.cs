using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gamedalf.Core.Attributes
{
    public class FilesTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types;

        public FilesTypesAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            foreach (var item in value as IEnumerable<HttpPostedFileBase>)
            {
                if (item != null
                    && !_types.Contains(
                    System.IO.Path.GetExtension(item.FileName).Substring(1),
                        StringComparer.OrdinalIgnoreCase)
                    )
                {
                    return false;
                }
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Invalid file type. Only the following types {0} are supported.", String.Join(", ", _types));
        }
    }
}
