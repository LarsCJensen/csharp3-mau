using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment3.BLL.Enums;

namespace Assignment3.BLL.Model
{
    [Serializable]
    public class Bug : Base
    {
        private static int _idCounter = 0;
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CategoryEnum Category { get; set; }
        public StatusEnum Status { get; set; }
        public Developer AssignedDeveloper { get; set; }
        public int StoryPoints { get; set; }
        public Bug()
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
