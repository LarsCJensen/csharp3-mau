using Assignment2.BLL.Model;
using Assignment2.BLL.Services;
using Assignment2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL
{
    public class SlideshowManager: BaseManager 
    {
        private SlideshowService _slideshowService = new SlideshowService();
        public Slideshow Slideshow { get; set; } = new Slideshow();
        public SlideshowManager()
        {
            // TODO remove if not needed
        }

        public override bool Save()
        {
            Slideshow.Files = Files;
            if (_slideshowService.Save(Slideshow))
            {
                return false;
            }

            return true;
        }
    }
}
