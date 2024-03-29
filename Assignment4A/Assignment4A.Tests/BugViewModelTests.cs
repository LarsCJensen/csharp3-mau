

using Assignment4A.BLL.Enums;
using Assignment4A.BLL.Model;
using Assignment4A.View;
using Assignment4A.ViewModel;
using Moq;
using System.ComponentModel;
using System.Net.Sockets;
using System.Reflection;

namespace Assignment4A.Tests.BugViewModelTests
{
    public class BugViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void ValidateBug_ValidBug_OnSaveIsCalledAndNoValidationErrorMessage()
        {
            // arrange
            var bugVm = GetPreparedBugVM();
            var bug = new Bug() { 
                Id=1, 
                Title="Bug Title", 
                Description="Bug Description",
                Status=BLL.Enums.StatusEnum.Reported                
            };
            bugVm.Bug = bug;
            var mockOnSave= new Mock<EventHandler<Bug>>();
            bugVm.OnSave += mockOnSave.Object;

            // act
            bugVm.Save();            
            // assert that OnSave is called with the correct input
            mockOnSave.Verify(x => x(bugVm, bug), Times.Once);
            // assert that no validation message exists
            Assert.IsTrue(bugVm.ValidationMessage == "");
        }

        [Test]
        public void ValidateBug_ValidBug_OnSaveIsNotCalledAndValidationErrorMessageIncludesTitle()
        {
            var bugVm = GetPreparedBugVM();
            var bug = new Bug()
            {
                Id = 1,
                Title = "",
                Description = "Bug Description",
                Status = BLL.Enums.StatusEnum.Reported
            };
            bugVm.Bug = bug;
            var mockOnSave = new Mock<EventHandler<Bug>>();
            bugVm.OnSave += mockOnSave.Object;

            // act
            bugVm.Save();
            // assert that OnSave is not called
            mockOnSave.Verify(x => x(bugVm, bug), Times.Never);
            // assert that validation message includes string
            Assert.IsTrue(bugVm.ValidationMessage.Contains("add a title"));
        }
        [Test]
        public void ValidateBug_ValidBug_OnSaveIsNotCalledAndValidationErrorMessageIncludesDescription()
        {
            var bugVm = GetPreparedBugVM();
            var bug = new Bug()
            {
                Id = 1,
                Title = "Bug Title",
                Description = "",
                Status = BLL.Enums.StatusEnum.Reported
            };
            bugVm.Bug = bug;
            var mockOnSave = new Mock<EventHandler<Bug>>();
            bugVm.OnSave += mockOnSave.Object;

            // act
            bugVm.Save();
            // assert that OnSave is not called
            mockOnSave.Verify(x => x(bugVm, bug), Times.Never);
            // assert that validation message includes string
            Assert.IsTrue(bugVm.ValidationMessage.Contains("add a description"));
        }

        [Test]
        public void ValidateBug_ValidBug_OnSaveIsNotCalledAndValidationErrorMessageIncludesClosingReason()
        {
            // arrange
            var bugVm = GetPreparedBugVM();
            var bug = new Bug()
            {
                Id = 1,
                Title = "Bug Title",
                Description = "Bug Description",
                Status = BLL.Enums.StatusEnum.Finished
            };
            bugVm.Bug = bug;
            var mockOnSave = new Mock<EventHandler<Bug>>();
            bugVm.OnSave += mockOnSave.Object;

            // act
            bugVm.Save();
            // assert that OnSave is not called
            mockOnSave.Verify(x => x(bugVm, bug), Times.Never);
            // assert that validation message includes string
            Assert.IsTrue(bugVm.ValidationMessage.Contains("provide a closing reason"));
        }
        [Test]
        public void ValidBug_CloseReason_ShowCloseReasonIsTrueWhenStatusIsRejected()
        {
            // arrange
            var bugVm = GetPreparedBugVM();
            Assert.That(bugVm.ShowCloseReason, Is.False);
            // act
            bugVm.SelectedStatus = StatusEnum.Rejected;
            // assert that ShowCloseReason is true
            Assert.That(bugVm.ShowCloseReason, Is.True);
        }
        [Test]
        public void ValidBug_CloseReason_ShowCloseReasonIsTrueWhenStatusIsFinished()
        {
            // arrange
            var bugVm = GetPreparedBugVM();
            Assert.That(bugVm.ShowCloseReason, Is.False);
            // act
            bugVm.SelectedStatus = StatusEnum.Finished;
            // assert that ShowCloseReason is true
            Assert.That(bugVm.ShowCloseReason, Is.True);
        }
        [Test]
        public void ValidBug_CloseReason_CloseReasonIsEmptyStringTrueWhenStatusIsChangedToPlanned()
        {
            // arrange
            var bugVm = GetPreparedBugVM();
            var bug = new Bug()
            {
                Id = 1,
                Title = "Bug Title",
                Description = "Bug Description",
                Status = StatusEnum.Finished,
                CloseReason = "This bug is finished",
            };
            bugVm.Bug = bug;
            // act
            bugVm.SelectedStatus = StatusEnum.Planned;
            // assert that ShowCloseReason is true
            Assert.That(bugVm.Bug.CloseReason, Is.EqualTo(String.Empty));
        }
        private BugViewModel GetPreparedBugVM()
        {
            var developers = new List<Developer>()
                {
                   new Developer{ FirstName="Test", LastName="Testsson", Email="test.testsson@company.com" },
                   new Developer{ FirstName="Test2", LastName="Testsson", Email="test2.testsson@company.com" },
                   new Developer{ FirstName="Test3", LastName="Testsson", Email="test3.testsson@company.com" },
                };
            var bugVm = new BugViewModel(developers);
            return bugVm;
        }
    }

}