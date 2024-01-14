using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.BL
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message) { }

        public NotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public NotFoundException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
