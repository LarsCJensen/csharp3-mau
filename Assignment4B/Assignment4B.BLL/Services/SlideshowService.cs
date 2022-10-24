using Assignment4B.DAL;
using Assignment4B.DAL.Models;
using Assignment4B.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Assignment4B.BLL.Services
{
    public class SlideshowService : BaseService<Slideshow>
    {
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();
        private IRepository<Slideshow> _repository;
        public SlideshowService()
        {
        }

        public override Dictionary<string, string> Save(Slideshow slideshow)
        {
            if (!Validate(slideshow))
            {
                return _validationErrors;
            }
            RecreateContext();
            try
            {
                _repository.Save(slideshow);
            }
            catch (InvalidOperationException)
            {
                new Dictionary<string, string>() { { "Slideshow not found!", $"Item with id {slideshow.id} not found in database!" } };
            }
            return new Dictionary<string, string>();
        }
        public override bool Delete(int slideshowId)
        {
            RecreateContext();
            try
            {
                _repository.Delete(slideshowId);
            }
            catch (InvalidOperationException)
            {
                new Dictionary<string, string>() { { "Slideshow not found!", $"Item with id {slideshowId} not found in database!" } };
            }
            
            return true;
        }
        public override IEnumerable<Slideshow> GetItems()
        {
            RecreateContext();
            return _repository.GetEntities();
        }
        /// <summary>
        /// Validation for Slideshow. Would prefer to have the validation in ViewModel, but I didn't have time to implement IDataError
        /// </summary>
        /// <param name="slideshowToValidate">Slideshow to validate</param>
        /// <returns></returns>
        protected override bool Validate(Slideshow slideshowToValidate)
        {
            bool isValid = true;
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
        /// <summary>
        /// Method to get by id
        /// </summary>
        /// <param name="id">Id to get</param>
        /// <returns>Slideshow with id</returns>
        public override Slideshow GetById(int id)
        {
            RecreateContext();
            return _repository.GetById(id);
        }
        /// <summary>
        /// Work around to make sure not to get old values based on context cache
        /// </summary>
        public override void RecreateContext()
        {
            MediaPlayerDbContext _context = new MediaPlayerDbContext();
            _repository = new Repository<Slideshow>(_context);
        }
        /// <summary>
        /// Helper method to search slideshows. 
        /// </summary>
        /// <param name="searchText">Text to search for</param>
        /// <param name="searchProperty">Property to search in</param>
        /// <returns>Search result</returns>        
        public override IEnumerable<Slideshow> SearchItems(string searchText, string searchProperty, string searchCriteria)
        {
            RecreateContext();
            return _repository.SearchEntities(searchText, searchProperty, searchCriteria);
        }
    }
}

