using Assignment2.BLL.Interface;
using Assignment2.BLL.Model;
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
    public class AlbumManager: BaseManager<Album>, ICollectionManager<AlbumFile>
    {
        private AlbumService _albumService = new AlbumService();
        public Album Album { get; set; }
        public List<AlbumFile> Files { get; set; } = new List<AlbumFile>();
        public bool AddItem(AlbumFile file)
        {
            // I could change Album.NumberOfImages with each add/delete
            Files.Add(file);
            return true;
        }
        public bool DeleteItem(int position)
        {
            Files.RemoveAt(position);
            return true;
        }
        public AlbumFile GetItemAt(int pos)
        {
            return Files[pos];
        }
        
        public bool MoveItem(int oldPos, int newPos)
        {
            Files = Utilities.Utilities.Move(Files, oldPos, newPos);
            return true;
        }
        public bool CopyToFolder { get; set; }  
        public AlbumManager()
        {
            // TODO Remove if needed
            Album = new Album();
        }
        public AlbumManager(int albumId)
        {
            // TODO Remove if needed
            Album = _albumService.GetById(albumId);
        }

        public override Dictionary<string, string> Save()
        {
            // TODO Go through servicelayer
            // See IDataErrorInfo region in MyGames
            // VALIDATE this, create data model 
            Album.Files = Files;   
            List<string> fileExtensions = Files.Select(f => f.Extension.ToLower()).ToList();  
            Album.NumberOfImages = GetCount(fileExtensions, ValidExtensions.ImageExtensions);
            Album.NumberOfVideos = GetCount(fileExtensions, ValidExtensions.VideoExtensions);
            return _albumService.Save(Album);
        }
        public override bool Delete(int albumId)
        {
            if (_albumService.Delete(albumId))
            {
                return false;
            }

            return true;
        }
        public override List<Album> GetItems()
        {
            return _albumService.GetItems().ToList();
        }

        public override List<Album> SearchItems(string searchText, string searchProperty)
        {
            return _albumService.SearchItems(searchText, searchProperty).ToList();
        }
    }
}

