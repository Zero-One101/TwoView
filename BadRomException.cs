using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoView
{
    class BadRomException : Exception
    {
        public BadRomException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}
