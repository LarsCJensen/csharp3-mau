using Assignment2.DAL;
using Assignment2.DAL.Models;
using Assignment2.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL.Services
{
    public class SlideshowService : BaseService<Slideshow>
    {
        private readonly IRepository<Slideshow> _repository;
        public SlideshowService()
        {
            MediaPlayerDbContext _context = new MediaPlayerDbContext();
            _repository = new Repository<Slideshow>(_context);
        }

        public override bool Save(Slideshow slideshow)
        {
            // TODO Go through servicelayer
            // See IDataErrorInfo region in MyGames
            // VALIDATE this, create data model
            if (!Validate(slideshow))
            {
                return false;
            }

            _repository.Save(slideshow);
            return true;
        }
        protected override bool Validate(Slideshow slideshowToValidate)
        {
            // TODO How to return this to the view?

            // CHeck if description is set
            // Check if files.descriptions etc is set

            // Create a dictionary to expose?
            //if (albumToValidate.Name.Trim().Length == 0)
            //    albumToValidate.AddError("Name", "Name is required.");
            //if (albumToValidate.Description.Trim().Length == 0)
            //    albumToValidate.AddError("Description", "Description is required.");
            //if (albumToValidate.UnitsInStock < 0)
            //    albumToValidate.AddError("UnitsInStock", "Units in stock cannot be less than zero.");
            //return albumToValidate.IsValid;
            return true;
        }
    }
}

