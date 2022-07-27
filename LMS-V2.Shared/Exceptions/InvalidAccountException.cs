using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Shared.Exceptions
{
    [Serializable]
    public class InvalidAccountExceptions : Exception 
    {
        public InvalidAccountExceptions() { }

        public InvalidAccountExceptions(string message)
            : base(message) { }

        public InvalidAccountExceptions(string message, Exception inner)
            : base(message, inner) { }
    }
}
