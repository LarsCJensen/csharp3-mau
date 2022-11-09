using Assignment4B.BLL;
using Assignment4B.BLL.Services;
using Assignment4B.DAL.Models;

namespace Assignment4B.BLL.Tests.AlbumServicesTest
{
    // Importing AlbumService to be able to test the protected methods
    [TestClass]
    public class AlbumServiceTests: AlbumService
    {
        [TestMethod]
        public void ValidateAlbumWithTitleReturnsTrue()
        {
            // arrange
            var album = PreparedAlbum();
            // act            
            // assert Validate album with title is true
            Assert.IsTrue(Validate(album) == true);

        }
        [TestMethod]
        public void ValidateAlbumWithNoTitleReturnsFalse()
        {
            // arrange
            var album = PreparedAlbum();
            album.Title = String.Empty;
            // act
            // assert Validate album with no title is false
            Assert.IsTrue(Validate(album) == false);
        }
        [TestMethod]
        public void ValidateAlbumWithDescriptionReturnsTrue()
        {
            // arrange
            var album = PreparedAlbum();
            // act
            // assert Validate album with description is true
            Assert.IsTrue(Validate(album) == true);
        }
        [TestMethod]
        public void ValidateAlbumWithNoDescriptionReturnsFalse()
        {
            // arrange
            var album = PreparedAlbum();
            album.Description = String.Empty;
            // act
            // assert Validate album with description is false
            Assert.IsTrue(Validate(album) == false);
        }
        [TestMethod]
        public void ValidateAlbumWithFilesReturnsTrue()
        {
            // arrange
            var album = PreparedAlbum();
            // act
            // assert album with files returns true
            Assert.IsTrue(Validate(album) == true);
        }
        [TestMethod]
        public void ValidateAlbumWithNoFilesReturnsFalse()
        {
            // arrange
            var album = PreparedAlbum();
            album.Files = new List<AlbumFile>();
            // act
            // assert that album with no files return false
            Assert.IsTrue(Validate(album) == false);
        }     
        private Album PreparedAlbum()
        {
            Album album = new Album();
            album.Title = "New Album";
            album.Description = "New description";
            album.Files = new List<AlbumFile>();
            album.Files.Add(new AlbumFile());
            return album;
        }
    }
}