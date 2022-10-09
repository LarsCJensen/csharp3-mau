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
        /// <summary>
        /// Manage for Albums
        /// </summary>
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
            int pos = 0;
            foreach(AlbumFile file in Files)
            {
                file.Position = pos;
                pos++;
            }
            return true;
        }
        // Not inplemented
        public bool CopyToFolder { get; set; }  
        public AlbumManager()
        {
            Album = new Album();
        }
        public AlbumManager(int albumId)
        {
            Album = _albumService.GetById(albumId);
        }
        /// <summary>
        /// Method to save
        /// </summary>
        /// <returns>Dictionary of validation errors</returns>
        public override Dictionary<string, string> Save()
        {
            Album.Files = Files;
            // Get all fileextensions
            List<string> fileExtensions = Files.Select(f => f.Extension.ToLower()).ToList();  
            // Count images
            Album.NumberOfImages = GetCount(fileExtensions, ValidExtensions.ImageExtensions);
            // Count videos
            Album.NumberOfVideos = GetCount(fileExtensions, ValidExtensions.VideoExtensions);
            return _albumService.Save(Album);
        }
        /// <summary>
        /// Method to delete
        /// </summary>
        /// <param name="albumId">Album ID to delete</param>
        /// <returns>Bool if success or not</returns>
        public override bool Delete(int albumId)
        {
            if (_albumService.Delete(albumId))
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>List of items</returns>
        public override List<Album> GetItems()
        {
            return _albumService.GetItems().ToList();
        }
        /// <summary>
        /// Search Album items
        /// </summary>
        /// <param name="searchText">String to search for </param>
        /// <param name="searchProperty">In which property to search in</param>
        /// <returns>List of results</returns>
        public override List<Album> SearchItems(string searchText, string searchProperty, string searchCriteria)
        {
            return _albumService.SearchItems(searchText, searchProperty, searchCriteria).ToList();
        }
    }
}

