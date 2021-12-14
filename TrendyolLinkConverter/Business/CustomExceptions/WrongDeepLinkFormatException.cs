using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CustomExceptions
{
    public class WrongDeepLinkFormatException : Exception
    {
        public WrongDeepLinkFormatException():base()
        {

        }
        public WrongDeepLinkFormatException(string message) : base(message)
        {

        }
    }
}
