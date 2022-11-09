using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assignment4A.BLL.Model
{
    /// <summary>
    /// Model for Developer
    /// </summary>
    [Serializable]
    public class Developer : Base
    {
        private static int _idCounter = 0;
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [XmlIgnore]
        public string FullName { 
            get { 
                return $"{FirstName} {LastName}"; 
            }             
        }
        public Developer()
        {
            Id = getId();
        }
        private int getId()
        {
            _idCounter = _idCounter += 1;
            return _idCounter;
        }

    }
}
