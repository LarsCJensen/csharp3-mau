using Assignment4B.BLL;
using Assignment4B.DAL.Models;

namespace Assignment4B.BLL.Tests.AlbumManagerTests
{
    // TODO This should be test base test
    [TestClass]
    public class AlbumManagerTests
    {
        [TestMethod]
        public void ItemIsAddedProperly()
        {
            // arrange
            var albumManager = new AlbumManager();
            var albumFile = new AlbumFile();
            // Make sure we are in the expected state
            Assert.IsTrue(albumManager.Files.Count == 0);
            // act
            albumManager.AddItem(albumFile);
            // assert
            Assert.IsTrue(albumManager.Files.Count == 1);
        }
        [TestMethod]
        public void ItemIsDeletedProperly()
        {
            // arrange
            var albumManager = new AlbumManager();
            var albumFile1 = new AlbumFile();            
            var albumFile2 = new AlbumFile();
            var albumFile3 = new AlbumFile();
            albumManager.AddItem(albumFile1);
            albumManager.AddItem(albumFile2);
            albumManager.AddItem(albumFile3);
            // Make sure we are in the expected state
            Assert.IsTrue(albumManager.Files.Count == 3);
            // act
            albumManager.DeleteItem(1);
            // assert
            Assert.IsTrue(albumManager.Files.Count() == 2);            
        }
        [TestMethod]
        public void WhenItemIsMovedItGetsCorrectPositionSet()
        {
            // arrange
            var albumManager = PreparedAlbumManager();
            var albumFile = new AlbumFile();
            albumFile.Name = "Test4";
            albumFile.Position = 4;
            albumFile.Extension = ".mov";
            albumManager.AddItem(albumFile);
            // act
            albumManager.MoveItem(3, 0);
            // assert
            Assert.AreEqual(albumFile, albumManager.Files[0]);
        }
        [TestMethod]
        public void WhenAlbumIsSavedWithCorrectNumberOfImages()
        {
            // arrange            
            var albumManager = PreparedAlbumManager();
            // act
            albumManager.Save();
            // assert
            Assert.AreEqual(2, albumManager.Album.NumberOfImages);
        }
        // I would probably not have this in a separate test since it is testing the same method as the above test
        [TestMethod]
        public void WhenAlbumIsSavedWithCorrectNumberOfVideos()
        {
            // arrange
            var albumManager = PreparedAlbumManager();
            // act
            albumManager.Save();
            // assert
            Assert.AreEqual(1, albumManager.Album.NumberOfVideos);
        }

        private AlbumManager PreparedAlbumManager()
        {
            var albumManager = new AlbumManager();
            var albumFile1 = new AlbumFile();
            albumFile1.Name = "Test";
            albumFile1.Position = 1;
            albumFile1.Extension = ".jpg";
            var albumFile2 = new AlbumFile();
            albumFile2.Name = "Test2";
            albumFile2.Position = 2;
            albumFile2.Extension = ".jpg";
            var albumFile3 = new AlbumFile();
            albumFile3.Name = "Test3";
            albumFile3.Position = 3;
            albumFile3.Extension = ".mov";
            albumManager.AddItem(albumFile1);
            albumManager.AddItem(albumFile2);
            albumManager.AddItem(albumFile3);
            return albumManager;
        }
    }
}