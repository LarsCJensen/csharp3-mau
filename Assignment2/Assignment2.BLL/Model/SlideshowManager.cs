using Assignment2.BLL.Interface;
using Assignment2.BLL.Model;
using Assignment2.BLL.Services;
using Assignment2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL
{
    public class SlideshowManager: BaseManager<Slideshow>, ICollectionManager<SlideshowFile>
    {
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
        // Not implemented
        public int CountItems()
        {
            throw new NotImplementedException();
        }

        public bool MoveItem(int oldPos, int newPos)
        {
            Files = Utilities.Utilities.Move(Files, oldPos, newPos);
            return true;
        }
        public SlideshowManager()
        {
            // TODO remove if not needed
            Slideshow = new Slideshow();
        }
        public SlideshowManager(int slideshowId)
        {
            Slideshow = _slideshowService.GetById(slideshowId);
        }
        // TODO
        public override Dictionary<string, string> Save()
        {
            Slideshow.Files = Files;
            List<string> fileExtensions = Files.Select(f => f.Extension.ToLower()).ToList();
            Slideshow.NumberOfImages = GetCount(fileExtensions, ValidExtensions.ImageExtensions);
            Slideshow.NumberOfVideos = GetCount(fileExtensions, ValidExtensions.VideoExtensions);
            return _slideshowService.Save(Slideshow);
        }
        public override bool Delete(int slideshowId)
        {
            if (_slideshowService.Delete(slideshowId))
            {
                return false;
            }

            return true;
        }
        public override List<Slideshow> GetItems()
        {
            return _slideshowService.GetItems().ToList();
        }
        public override List<Slideshow> SearchItems(string searchText, string searchProperty)
        {
            return _slideshowService.SearchItems(searchText, searchProperty).ToList();
        }
    }
}
