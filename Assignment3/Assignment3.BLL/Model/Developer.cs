using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assignment3.BLL.Model
{
    [Serializable]
    public class Developer : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [XmlIgnore]
        public string FullName { 
            get { 
                return $"{FirstName} {LastName}"; 
            }             
        }
        //public Developer(string firstName, string lastName, string email)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //}
    }
}
