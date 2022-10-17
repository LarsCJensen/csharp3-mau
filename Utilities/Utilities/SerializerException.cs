using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class SerializerException : Exception
    {
        public SerializerException() { }
        public SerializerException(string information) : base(information) { }
        public SerializerException(string information, Exception e) : base(information, e) { }
    }
}