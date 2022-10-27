using Assignment4B.BLL;
using Assignment4B.DAL.Models;

namespace Assignment4B.BLL.Tests.SlideshowManagerTests
{
    [TestClass]
    public class SlideshowManagerTests
    {
        [TestMethod]
        public void ItemIsAddedProperly()
        {
            // arrange
            var SlideshowManager = new SlideshowManager();
            var SlideshowFile = new SlideshowFile();
            // Make sure we are in the expected state
            Assert.IsTrue(SlideshowManager.Files.Count == 0);
            // act
            SlideshowManager.AddItem(SlideshowFile);
            // assert
            Assert.IsTrue(SlideshowManager.Files.Count == 1);
        }
        [TestMethod]
        public void ItemIsDeletedProperly()
        {
            // arrange
            var SlideshowManager = new SlideshowManager();
            var SlideshowFile1 = new SlideshowFile();
            var SlideshowFile2 = new SlideshowFile();
            var SlideshowFile3 = new SlideshowFile();
            SlideshowManager.AddItem(SlideshowFile1);
            SlideshowManager.AddItem(SlideshowFile2);
            SlideshowManager.AddItem(SlideshowFile3);
            // Make sure we are in the expected state
            Assert.IsTrue(SlideshowManager.Files.Count == 3);
            // act
            SlideshowManager.DeleteItem(1);
            // assert
            Assert.IsTrue(SlideshowManager.Files.Count() == 2);
        }
        [TestMethod]
        public void WhenItemIsMovedItGetsCorrectPositionSet()
        {
            // arrange
            var SlideshowManager = PreparedSlideshowManager();
            var SlideshowFile = new SlideshowFile();
            SlideshowFile.Name = "Test4";
            SlideshowFile.Position = 4;
            SlideshowFile.Extension = ".mov";
            SlideshowManager.AddItem(SlideshowFile);
            // act
            SlideshowManager.MoveItem(3, 0);
            // assert
            Assert.AreEqual(SlideshowFile, SlideshowManager.Files[0]);
        }
        [TestMethod]
        public void WhenAlbumIsSavedWithCorrectNumberOfImages()
        {
            // arrange            
            var SlideshowManager = PreparedSlideshowManager();
            // act
            SlideshowManager.Save();
            // assert
            Assert.AreEqual(2, SlideshowManager.Slideshow.NumberOfImages);
        }
        // I would probably not have this in a separate test since it is testing the same method as the above test
        [TestMethod]
        public void WhenAlbumIsSavedWithCorrectNumberOfVideos()
        {
            // arrange
            var SlideshowManager = PreparedSlideshowManager();
            // act
            SlideshowManager.Save();
            // assert
            Assert.AreEqual(1, SlideshowManager.Slideshow.NumberOfVideos);
        }

        private SlideshowManager PreparedSlideshowManager()
        {
            var SlideshowManager = new SlideshowManager();
            var SlideshowFile1 = new SlideshowFile();
            SlideshowFile1.Name = "Test";
            SlideshowFile1.Position = 1;
            SlideshowFile1.Extension = ".jpg";
            var SlideshowFile2 = new SlideshowFile();
            SlideshowFile2.Name = "Test2";
            SlideshowFile2.Position = 2;
            SlideshowFile2.Extension = ".jpg";
            var SlideshowFile3 = new SlideshowFile();
            SlideshowFile3.Name = "Test3";
            SlideshowFile3.Position = 3;
            SlideshowFile3.Extension = ".mov";
            SlideshowManager.AddItem(SlideshowFile1);
            SlideshowManager.AddItem(SlideshowFile2);
            SlideshowManager.AddItem(SlideshowFile3);
            return SlideshowManager;
        }

    }
}