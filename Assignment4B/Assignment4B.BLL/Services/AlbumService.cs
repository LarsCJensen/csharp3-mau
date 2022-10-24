using Assignment4B.DAL;
using Assignment4B.DAL.Models;
using Assignment4B.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4B.BLL.Services
{
    public class AlbumService: BaseService<Album> 
    {
        /// <summary>
        /// Service for Albums
        /// </summary>
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();
        private IRepository<Album> _repository;
        public AlbumService()
        {
            RecreateContext();
        }

        public override Dictionary<string, string> Save(Album album)
        {
            if (!Validate(album))
            {
                return _validationErrors;
            }
            RecreateContext();
            try
            {
                _repository.Save(album);
            } catch(InvalidOperationException)
            {
                new Dictionary<string, string>() { { "Album not found!", $"Item with id {album.id} not found in database!" } };
            }
            
            return new Dictionary<string, string>();
        }
        /// <summary>
        /// Method for delete
        /// </summary>
        /// <param name="albumId">Album ID</param>
        /// <returns>True/False</returns>
        public override bool Delete(int albumId)
        {
            RecreateContext();
            try
            {
                _repository.Delete(albumId);

            } catch(InvalidOperationException)
            {
                new Dictionary<string, string>() { { "Album not found!", $"Item with id {albumId} not found in database!" } };
            }
            return true;
        }

        public override IEnumerable<Album> GetItems()
        {
            RecreateContext();
            return _repository.GetEntities();
        }
        /// <summary>
        /// Validation for Album. Would prefer to have the validation in ViewModel, but I didn't have time to implement IDataError
        /// </summary>
        /// <param name="albumToValidate">Album to validate</param>
        /// <returns>True or False</returns>
        protected override bool Validate(Album albumToValidate)
        {
            bool isValid = true;
            if (albumToValidate.Title == null || albumToValidate.Title.Trim().Length == 0)
            {
                _validationErrors.Add(nameof(albumToValidate.Title), "Title is required.");
                isValid = false;
            }                
            
            if (albumToValidate.Description == null || albumToValidate.Description.Trim().Length == 0)
            {
                _validationErrors.Add(nameof(albumToValidate.Description), "Description is required.");
                isValid = false;
            }
                
            if (albumToValidate.Files.Count == 0)
            {
                _validationErrors.Add(nameof(albumToValidate.Files), "You need to add files!");
                isValid = false;
            }              

            return isValid;
        }
        /// <summary>
        /// Method to get by ID
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Item</returns>
        public override Album GetById(int id)
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
            _repository = new Repository<Album>(_context);
        }
        /// <summary>
        /// Helper method to search albums. 
        /// </summary>
        /// <param name="searchText">Text to search for</param>
        /// <param name="searchProperty">Property to search in</param>
        /// <returns>Search result</returns>        
        public override IEnumerable<Album> SearchItems(string searchText, string searchProperty, string searchCriteria)
        {
            RecreateContext();
            return _repository.SearchEntities(searchText, searchProperty, searchCriteria);
        }
    }
}

