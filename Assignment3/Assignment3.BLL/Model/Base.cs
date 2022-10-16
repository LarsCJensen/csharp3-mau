using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.BLL.Model
{
    [Serializable]
    public class Base
    {
        private static int _idCounter = 0;
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        internal int getId()
        {
            _idCounter = _idCounter += 1;
            return _idCounter;
        }

    }
}
