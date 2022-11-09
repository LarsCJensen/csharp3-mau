using Assignment4A.BLL.Enums;
using Assignment4A.BLL.Model;
using Assignment4A.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4A.Tests.MainViewModelTests
{
    public class MainViewModelTests
    {
        [Test]
        public void BugsCountText_CollectionChangedAdd_SetsCorrectBugCountText()
        {
            // arrange
            var mainVm = new MainViewModel();
            Assert.That(mainVm.BugsCountText, Is.EqualTo($"0 bugs in the system!"));
            var bug = new Bug()
            {
                Id = 1,
                Title = "Bug Title",
                Description = "Bug Description",
                Status = StatusEnum.Finished,
                CloseReason = "This bug is finished",
            };
            // act
            mainVm.UnFilteredBugs.Add(bug);
            // assert that BugCountText is correct
            Assert.That(mainVm.BugsCountText, Is.EqualTo($"1 bugs in the system!"));
        }
        [Test]
        public void BugsCountText_CollectionChangedDelete_SetsCorrectBugCountText()
        {
            // arrange
            var mainVm = new MainViewModel();
            var bug = new Bug()
            {
                Id = 1,
                Title = "Bug Title",
                Description = "Bug Description",
                Status = StatusEnum.Finished,
                CloseReason = "This bug is finished",
            };
            mainVm.UnFilteredBugs.Add(bug);
            mainVm.SelectedBug = bug;
            Assert.That(mainVm.BugsCountText, Is.EqualTo($"1 bugs in the system!"));
            // act
            mainVm.UnFilteredBugs.Remove(mainVm.SelectedBug);
            // assert that BugCountText is correct
            Assert.That(mainVm.BugsCountText, Is.EqualTo($"0 bugs in the system!"));
        }
    }
    

}
