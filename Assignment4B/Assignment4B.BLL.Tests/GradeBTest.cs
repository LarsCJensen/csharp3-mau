using Assignment4B.BLL.Services;
using Assignment4B.DAL.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4B.BLL.Tests.GradeB
{
    [TestClass]
    public class GradeBTest
    {
        [TestMethod]
        public void FindHardToFindBug1()
        {
            // arrange
            var albumManager = new AlbumManager();
            var idNotFound = 123;
            // act
            var result = albumManager.Delete(idNotFound);
            // assert deleting album returns false if not found
            // This won't "work" since the method always return True after raise
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void FindHardToFindBug2()
        {
            // arrange
            var albumService = new AlbumService();
            var album = new Album();
            album.Title = "My album";
            album.Description = "My album description";
            // act
            var isValid = albumService.Validate(album);
            // assert album is valid, but will throw exception because album.Files is null which is not checked for
            Assert.IsTrue(isValid);
        }
    }
}
