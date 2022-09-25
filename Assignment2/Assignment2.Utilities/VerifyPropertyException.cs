using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Utilities
{
    /// <summary>
    /// Custom exception for when property can't be found
    /// </summary>
    public class VerifyPropertyException : Exception
    {
        public VerifyPropertyException() { }
        public VerifyPropertyException(string message) : base(message) { }
        public VerifyPropertyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
