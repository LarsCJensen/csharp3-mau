using Assignment4B.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4B.BLL.Tests.BaseManagerTests
{
    [TestClass]
    public class BaseManagerTests
    {
        [TestMethod]
        public void TestGetCountReturnsCorrectValuesForImages()
        {
            // arrange
            var albumManager = new AlbumManager();
            var listOfExtensions = new List<string> { ".jpg", ".png", ".mov" };
            // act
            var numberOfImages = albumManager.GetCount(listOfExtensions, ValidExtensions.ImageExtensions);            
            // assert
            Assert.AreEqual(2, numberOfImages);
        }
        public void TestGetCountReturnsCorrectValuesForVideos()
        {
            // arrange
            var albumManager = new AlbumManager();
            var listOfExtensions = new List<string> { ".jpg", ".png", ".mov" };
            // act
            var numberOfVideos = albumManager.GetCount(listOfExtensions, ValidExtensions.VideoExtensions);
            // assert
            Assert.AreEqual(1, numberOfVideos);

        }
    }
}
