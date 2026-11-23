using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace MIDIcom4._6.Tests
{
    [TestClass]
    public class Form1IdAndPathTests
    {
        [TestMethod]
        public void NewStudyID_WhenReuseDisabled_ReturnsMaxPlusOne()
        {
            var form = new Form1();
            form.knownSubjects.Clear();
            form.cf.IDFormat = "0000";
            form.cf.reuseStudyID = false;

            form.knownSubjects.Add("P1", new Form1.Subject("P1", "0002"));
            form.knownSubjects.Add("P2", new Form1.Subject("P2", "0007"));
            form.knownSubjects.Add("P3", new Form1.Subject("P3", "0004"));

            var next = form.NewStudyID();

            Assert.AreEqual("0008", next);
        }

        [TestMethod]
        public void NewStudyID_WhenReuseEnabled_ReturnsNextNonUsedAfterInitialIncrement()
        {
            var form = new Form1();
            form.knownSubjects.Clear();
            form.cf.IDFormat = "0000";
            form.cf.reuseStudyID = true;

            form.knownSubjects.Add("P1", new Form1.Subject("P1", "0002"));
            form.knownSubjects.Add("P2", new Form1.Subject("P2", "0003"));

            var next = form.NewStudyID();

            Assert.AreEqual("0004", next);
        }

        [TestMethod]
        public void ReplaceInvalidChars_RemovesInvalidFileNameCharacters()
        {
            var form = new Form1();
            var input = "A<B>C:D\"E/F|G?H*I";

            var output = form.ReplaceInvalidChars(input);

            Assert.IsFalse(Path.GetInvalidFileNameChars().Any(ch => output.Contains(ch)));
        }
    }
}
