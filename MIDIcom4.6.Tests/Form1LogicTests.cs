using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MIDIcom4._6.Tests
{
    [TestClass]
    public class Form1LogicTests
    {
        [TestMethod]
        public void Accession_ArrayConstructor_DeduplicatesAndSkipsEmptyDescriptions()
        {
            var accession = new Form1.Accession("ACC1", "20240101", new[] { "AX T1", "", "AX T1", "COR T2" });

            CollectionAssert.AreEquivalent(new[] { "AX T1", "COR T2" }, accession.series_descs.ToArray());
        }

        [TestMethod]
        public void Accession_SingleDescriptionConstructor_AddsDescription()
        {
            var accession = new Form1.Accession("ACC1", "20240101", "AX T1");

            Assert.AreEqual(1, accession.series_descs.Count);
            Assert.AreEqual("AX T1", accession.series_descs[0]);
        }

        [TestMethod]
        public void Subject_DcmImageDate_ConvertsDateToDicomFormat()
        {
            var subject = new Form1.Subject("NHS1", "0001", image: "2024-01-31");

            Assert.AreEqual("20240131", subject.DcmImageDate());
        }
    }
}
