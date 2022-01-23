using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PeopleViewer.Presentation.Tests
{
    [TestClass]
    public class PeopleViewModelTests
    {
        [TestMethod]
        public void People_OnRefreshPeople_IsPopulated()
        {
            // ARRANGE
            FakeReader reader = new FakeReader();
            PeopleViewModel viewModel = new PeopleViewModel(reader);

            // ACT
            viewModel.RefreshPeople();

            // ASSERT
            Assert.IsNotNull(viewModel.People);
            Assert.AreEqual(2, viewModel.People.Count());
        }

        [TestMethod]
        public void People_OnClearPeople_IsEmpty()
        {
            // ARRANGE
            FakeReader reader = new FakeReader();
            PeopleViewModel viewModel = new PeopleViewModel(reader);
            viewModel.RefreshPeople();
            Assert.AreNotEqual(0, viewModel.People.Count(), "Invalid arrange");

            // ACT
            viewModel.ClearPeople();

            // ASSERT
            Assert.AreEqual(0, viewModel.People.Count());
        }
    }
}
