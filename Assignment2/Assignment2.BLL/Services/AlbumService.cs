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
    // TODO Create base service?
    public class AlbumService: BaseService<Album> 
    {
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
            // TODO try catch
            _repository.Save(album);
            return new Dictionary<string, string>();
        }
        public override bool Delete(int albumId)
        {
            // TODO Go through servicelayer
            // See IDataErrorInfo region in MyGames
            // VALIDATE this, create data model
            
            RecreateContext();
            _repository.Delete(albumId);
            return true;
        }

        public override IEnumerable<Album> GetItems()
        {
            RecreateContext();
            return _repository.GetEntities();
        }
        protected override bool Validate(Album albumToValidate)
        {
            bool isValid = true;
            // TODO How to return this to the view?

            // CHeck if description is set
            // Check if files.descriptions etc is set

            // Create a dictionary to expose?
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

        public override Album GetById(int id)
        {
            RecreateContext();
            return _repository.GetById(id);
        }

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
        public override IEnumerable<Album> SearchItems(string searchText, string searchProperty)
        {
            RecreateContext();
            return _repository.SearchEntities(searchText, searchProperty);
        }
    }
}

