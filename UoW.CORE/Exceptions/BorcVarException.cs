using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.CORE.Exceptions
{
    public class BorcVarException : Exception
    {
        public BorcVarException(string message) : base(message)
        {
        }

        public BorcVarException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
