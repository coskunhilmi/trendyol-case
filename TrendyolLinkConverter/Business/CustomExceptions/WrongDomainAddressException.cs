using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CustomExceptions
{
    public class WrongDomainAddressException : Exception
    {
        public WrongDomainAddressException(string message):base(message)
        {

        }
    }
}
