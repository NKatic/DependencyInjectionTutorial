using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleViewer.Common;

namespace PersonDataReader.CSV.Tests
{
    [TestClass]
    public class CSVReaderTests
    {
        [TestMethod]
        public void GetPeople_WithGoodRecords_ReturnsAllRecords()
        {
            // ARRANGE
            CSVReader reader = new CSVReader();
            reader.FileLoader = new FakeFileLoader("Good");

            // ACT
            IEnumerable<Person> result = reader.GetPeople();

            // RESOLVE
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetPeople_WithNoFile_ThrowsFileNotFoundException()
        {
            // ARRANGE
            CSVReader reader = new CSVReader();

            // ACT

            // RESOLVE
            Assert.ThrowsException<FileNotFoundException>(() => reader.GetPeople());
        }

        [TestMethod]
        public void GetPeople_WithMixedRecords_ReturnsGoodRecords()
        {
            // ARRANGE
            CSVReader reader = new CSVReader();
            reader.FileLoader = new FakeFileLoader("Mixed");

            // ACT
            IEnumerable<Person> result = reader.GetPeople();

            // RESOLVE
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetPeople_WithBadRecords_ReturnsEmptyList()
        {
            // ARRANGE
            CSVReader reader = new CSVReader();
            reader.FileLoader = new FakeFileLoader("Bad");

            // ACT
            IEnumerable<Person> result = reader.GetPeople();

            // RESOLVE
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetPeople_WithEmptyFile_ReturnsEmptyList()
        {
            // ARRANGE
            CSVReader reader = new CSVReader();
            reader.FileLoader = new FakeFileLoader("Empty");

            // ACT
            IEnumerable<Person> result = reader.GetPeople();

            // RESOLVE
            Assert.AreEqual(0, result.Count());
        }
    }
}