using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UoW.ADMIN.Helpers
{
    public class AdminException : Exception
    {
        public AdminException(string message) : base(message)
        {
        }

        public AdminException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
