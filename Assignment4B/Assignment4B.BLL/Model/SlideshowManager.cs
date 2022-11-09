using Assignment4B.BLL.Interface;
using Assignment4B.BLL.Model;
using Assignment4B.BLL.Services;
using Assignment4B.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4B.BLL
{
    public class SlideshowManager: BaseManager<Slideshow>, ICollectionManager<SlideshowFile>
    {
        /// <summary>
        /// Manager for Slideshows
        /// </summary>
        private SlideshowService _slideshowService = new SlideshowService();
        public Slideshow Slideshow { get; set; }
        public List<SlideshowFile> Files { get; set; } = new List<SlideshowFile>();
        public bool AddItem(SlideshowFile file)
        {
            Files.Add(file);
            return true;
        }
        public bool DeleteItem(int position)
        {
            Files.RemoveAt(position);
            return true;
        }
        public SlideshowFile GetItemAt(int pos)
        {
            return Files[pos];
        }
        
        public bool MoveItem(int oldPos, int newPos)
        {
            Files = Utilities.Utilities.Move(Files, oldPos, newPos);
            int pos = 0;
            foreach (SlideshowFile file in Files)
            {
                file.Position = pos;
                pos++;
            }
            return true;
        }
        public SlideshowManager()
        {
            Slideshow = new Slideshow();
        }
        public SlideshowManager(int slideshowId)
        {
            Slideshow = _slideshowService.GetById(slideshowId);
        }
        public override Dictionary<string, string> Save()
        {
            Slideshow.Files = Files;
            // Get all fileExtensions
            List<string> fileExtensions = Files.Select(f => f.Extension.ToLower()).ToList();
            // Count number if Images
            Slideshow.NumberOfImages = GetCount(fileExtensions, ValidExtensions.ImageExtensions);
            // Count number if Videos
            Slideshow.NumberOfVideos = GetCount(fileExtensions, ValidExtensions.VideoExtensions);
            return _slideshowService.Save(Slideshow);
        }
        public override bool Delete(int slideshowId)
        {
            return _slideshowService.Delete(slideshowId);            
        }
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>List of items</returns>
        public override List<Slideshow> GetItems()
        {
            return _slideshowService.GetItems().ToList();
        }
        // <summary>
        /// Search Album items
        /// </summary>
        /// <param name="searchText">String to search for </param>
        /// <param name="searchProperty">In which property to search in</param>
        /// <returns>List of results</returns>
        public override List<Slideshow> SearchItems(string searchText, string searchProperty, string searchCriteria)
        {
            return _slideshowService.SearchItems(searchText, searchProperty, searchCriteria).ToList();
        }
    }
}
