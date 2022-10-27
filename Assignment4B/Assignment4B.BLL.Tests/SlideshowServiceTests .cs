using Assignment4B.BLL;
using Assignment4B.BLL.Services;
using Assignment4B.DAL.Models;

namespace Assignment4B.BLL.Tests.SlideshowServiceTests
{
    // Importing SlideshowService to be able to test the protected methods
    [TestClass]
    public class SlideshowServiceTests: SlideshowService
    {
        [TestMethod]
        public void ValidateSlideshowWithTitleReturnsTrue()
        {
            // arrange
            var Slideshow = PreparedSlideshow();
            // act            
            // assert
            Assert.IsTrue(Validate(Slideshow)==true);

        }
        [TestMethod]
        public void ValidateSlideshowWithNoTitleReturnsFalse()
        {
            // arrange
            var Slideshow = PreparedSlideshow();
            Slideshow.Title = String.Empty;
            // act
            // assert
            Assert.IsTrue(Validate(Slideshow) == false);
        }
        [TestMethod]
        public void ValidateSlideshowWithDescriptionReturnsTrue()
        {
            // arrange
            var Slideshow = PreparedSlideshow();
            // act
            // assert
            Assert.IsTrue(Validate(Slideshow) == true);
        }
        [TestMethod]
        public void ValidateSlideshowWithNoDescriptionReturnsFalse()
        {
            // arrange
            var Slideshow = PreparedSlideshow();
            Slideshow.Description = String.Empty;
            // act
            // assert that no title return false
            Assert.IsTrue(Validate(Slideshow) == false);
        }
        [TestMethod]
        public void ValidateSlideshowWithFilesReturnsTrue()
        {
            // arrange
            var Slideshow = PreparedSlideshow();
            // act
            // assert that no description return false
            Assert.IsTrue(Validate(Slideshow) == true);
        }
        [TestMethod]
        public void ValidateSlideshowWithNoFilesReturnsFalse()
        {
            // arrange
            var Slideshow = PreparedSlideshow();
            Slideshow.Files = new List<SlideshowFile>();
            // act
            // assert that no files return false
            Assert.IsTrue(Validate(Slideshow) == false);
        }     
        /// <summary>
        /// Helper method to prepare many tests
        /// </summary>
        /// <returns></returns>
        private Slideshow PreparedSlideshow()
        {
            Slideshow Slideshow = new Slideshow();
            Slideshow.Title = "New Slideshow";
            Slideshow.Description = "New description";
            Slideshow.Files = new List<SlideshowFile>();
            Slideshow.Files.Add(new SlideshowFile());
            return Slideshow;
        }
    }
}