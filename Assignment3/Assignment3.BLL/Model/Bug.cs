using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment3.BLL.Enums;

namespace Assignment3.BLL.Model
{
    public class Bug : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public CategoryEnum Category { get; set; }
        public StatusEnum Status { get; set; }
        public Developer AssignedTo { get; set; }
        public int StoryPoints { get; set; }
        public Bug()
        {
            Id = getId();
        }

    }
}
