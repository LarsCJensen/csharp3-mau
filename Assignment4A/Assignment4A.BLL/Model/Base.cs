using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4A.BLL.Model
{
    /// <summary>
    /// Base model class
    /// </summary>
    [Serializable]
    public class Base
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;        
    }
}
