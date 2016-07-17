﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TwoView
{
    [Serializable]
    public class BadRomException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public BadRomException()
        {
        }

        public BadRomException(string message) : base(message)
        {
        }

        public BadRomException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BadRomException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}