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
        private readonly IRepository<Album> _repository;
        public AlbumService()
        {
            MediaPlayerDbContext _context = new MediaPlayerDbContext();
            _repository = new Repository<Album>(_context);
        }

        public override Dictionary<string, string> Save(Album album)
        {
            if (!Validate(album))
            {
                return _validationErrors;
            }

            _repository.Save(album);
            return new Dictionary<string, string>();
        }
        public override bool Delete(int albumId)
        {
            // TODO Go through servicelayer
            // See IDataErrorInfo region in MyGames
            // VALIDATE this, create data model            
            _repository.Delete(albumId);
            return true;
        }

        public override IEnumerable<Album> GetItems()
        {
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
            return _repository.GetById(id);
        }
    }
}

