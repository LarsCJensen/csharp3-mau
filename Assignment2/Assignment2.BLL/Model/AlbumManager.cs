using Assignment2.BLL.Services;
using Assignment2.DAL.Models;
using Assignment2.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL
{
    public class AlbumManager: BaseManager
    {
        private AlbumService _albumService = new AlbumService();
        public Album Album { get; set; } = new Album();
        public bool CopyToFolder { get; set; }  
        public AlbumManager()
        {
            // TODO Remove if needed
        }

        public override bool Save()
        {
            // TODO Go through servicelayer
            // See IDataErrorInfo region in MyGames
            // VALIDATE this, create data model 
            Album.Files = Files;
            if (_albumService.Save(Album))
            {
                return false;
            }
            
            return true;
        }
        // TODO REMOVE?
        //protected bool ValidateAlbum(Album albumToValidate)
        //{
        //    //if (productToValidate.Name.Trim().Length == 0)
        //    //    _validatonDictionary.AddError("Name", "Name is required.");
        //    //if (productToValidate.Description.Trim().Length == 0)
        //    //    _validatonDictionary.AddError("Description", "Description is required.");
        //    //if (productToValidate.UnitsInStock < 0)
        //    //    _validatonDictionary.AddError("UnitsInStock", "Units in stock cannot be less than zero.");
        //    //return _validatonDictionary.IsValid;
        //    return false;
        //}
    }
}

