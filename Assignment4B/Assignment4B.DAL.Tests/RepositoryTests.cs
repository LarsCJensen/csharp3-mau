using Assignment4B.DAL.Models;
using Assignment4B.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Metadata;

namespace Assignment4B.DAL.Tests
{
    public class RepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            // Is run before each test            
        }
        [Test]
        public void Repository_SaveWithoutId_CallsSaveNewOnce()
        {
            // arrange
            // A mock set of albums
            var mockSet = new Mock<DbSet<Album>>();
            
            // A Mock of the db context
            var mockContext = new Mock<MediaPlayerDbContext>();
            // Mocks the album set on the datacontext
            mockContext.Setup(m => m.Set<Album>()).Returns(mockSet.Object);
            
            // Creates a mock repository
            var mockRepository = new Mock<Repository<Album>>(mockContext.Object);
            var album = new Album();
            album.Title = "test";

            // act
            // Calls method save of repository
            mockRepository.Object.Save(album);
            // assert that SaveNew is called once if it is a new album
            mockRepository.Verify(x => x.SaveNew(It.IsAny<Album>()), Times.Once);
        }
        [Test]
        public void Repository_SaveWithId_DoesNotCallSaveNew()
        {
            // arrange
            var mockSet = new Mock<DbSet<Album>>();

            var mockContext = new Mock<MediaPlayerDbContext>();
            mockContext.Setup(m => m.Set<Album>()).Returns(mockSet.Object);

            var album = new Album();
            album.id = 1;
            album.Title = "test";

            var mockRepository = new Mock<Repository<Album>>(mockContext.Object);
            mockRepository.Setup(x => x.GetById(1)).Returns(album);
            
            // act
            mockRepository.Object.Save(album);
            // assert SaveNew is not called
            mockRepository.Verify(x => x.SaveNew(It.IsAny<Album>()), Times.Never);
        }
        [Test]
        public void Repository_DeleteWhenItemNotFound_ThrowsInvalidOperationExceptions()
        {
            // arrange
            var mockContext = new Mock<MediaPlayerDbContext>();
            var mockRepository = new Mock<Repository<Album>>(mockContext.Object);
            // act
            // assert Delete with unknow ID throws the correct exception
            Assert.Throws<InvalidOperationException>(() => mockRepository.Object.Delete(123));
        }
        [Test]
        public void Repository_SaveWhenItemNotFound_ThrowsInvalidOperationExceptions()
        {
            // arrange
            var mockContext = new Mock<MediaPlayerDbContext>();
            var mockRepository = new Mock<Repository<Album>>(mockContext.Object);
            
            var album = new Album();
            album.id = 1;
            album.Title = "test";
            // act
            // assert Delete with unknow ID throws the correct exception
            Assert.Throws<InvalidOperationException>(() => mockRepository.Object.Save(album));
        }
    }
}