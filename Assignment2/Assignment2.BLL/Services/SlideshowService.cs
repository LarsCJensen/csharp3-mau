using Assignment2.DAL;
using Assignment2.DAL.Models;
using Assignment2.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Assignment2.BLL.Services
{
    public class SlideshowService : BaseService<Slideshow>
    {
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();
        private readonly IRepository<Slideshow> _repository;
        public SlideshowService()
        {
            MediaPlayerDbContext _context = new MediaPlayerDbContext();
            _repository = new Repository<Slideshow>(_context);
        }

        public override Dictionary<string, string> Save(Slideshow slideshow)
        {
            // TODO Go through servicelayer
            // See IDataErrorInfo region in MyGames
            // VALIDATE this, create data model
            if (!Validate(slideshow))
            {
                return _validationErrors;
            }

            _repository.Save(slideshow);
            return new Dictionary<string, string>();
        }
        public override bool Delete(int slideshowId)
        {
            // TODO Go through servicelayer
            // See IDataErrorInfo region in MyGames
            // VALIDATE this, create data model
            _repository.Delete(slideshowId);
            return true;
        }
        public override IEnumerable<Slideshow> GetItems()
        {
            return _repository.GetEntities();
        }
        protected override bool Validate(Slideshow slideshowToValidate)
        {
            bool isValid = true;
            // TODO How to return this to the view?

            // CHeck if description is set
            // Check if files.descriptions etc is set

            // Create a dictionary to expose?
            if (slideshowToValidate.Title == null || slideshowToValidate.Title.Trim().Length == 0)
            {
                _validationErrors.Add(nameof(slideshowToValidate.Title), "Title is required.");
                isValid = false;
            }

            if (slideshowToValidate.Description == null || slideshowToValidate.Description.Trim().Length == 0)
            {
                _validationErrors.Add(nameof(slideshowToValidate.Description), "Description is required.");
                isValid = false;
            }

            if (slideshowToValidate.Files.Count == 0)
            {
                _validationErrors.Add(nameof(slideshowToValidate.Files), "You need to add files!");
                isValid = false;
            }

            return isValid;
        }

        public override Slideshow GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}

