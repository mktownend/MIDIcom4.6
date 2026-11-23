using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace MIDIcom4._6.Tests
{
    [TestClass]
    public class ConfigSettingsTests
    {
        private string _tempDir = string.Empty;

        [TestInitialize]
        public void Init()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "MIDIcom4.6.Tests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tempDir);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, true);
            }
        }

        [TestMethod]
        public void LoadFromTxt_ParsesValuesAndBooleans()
        {
            var enrolPath = Path.Combine(_tempDir, "enrolment_log.xlsx");
            File.WriteAllText(enrolPath, "placeholder");

            var configPath = Path.Combine(_tempDir, "config.txt");
            File.WriteAllLines(configPath, new[]
            {
                "# comment",
                "STUDYCODE=midi",
                "CENTRECODE=kch",
                "SITENAME=Kings College Hospital",
                "PRINCIPALINV=Dr Smith",
                "IDFORMAT=00000",
                "IGNORENOID=TRUE",
                "IGNOREUNASSIGNED=TRUE",
                "REUSESTUDYID=TRUE",
                "BACKUPFILES=TRUE",
                "SAVETXT=TRUE",
                $"ENROLFILEPATH={enrolPath}"
            });

            var settings = new Form1.ConfigSettings();
            settings.LoadFromTxt(configPath);

            Assert.AreEqual("MIDI", settings.studyCode);
            Assert.AreEqual("KCH", settings.centreCode);
            Assert.AreEqual("KINGS COLLEGE HOSPITAL", settings.siteName);
            Assert.AreEqual("DR SMITH", settings.principalInv);
            Assert.AreEqual("00000", settings.IDFormat);
            Assert.IsTrue(settings.ignoreNoID);
            Assert.IsTrue(settings.ignoreUnassigned);
            Assert.IsTrue(settings.reuseStudyID);
            Assert.IsTrue(settings.backupFiles);
            Assert.IsTrue(settings.saveTxt);
            Assert.AreEqual(enrolPath.ToUpper(), settings.enrolFilePath.ToUpper());
        }

        [TestMethod]
        public void SaveToTxt_PersistsUpdatedValues()
        {
            var enrolPath = Path.Combine(_tempDir, "enrolment_log.xlsx");
            File.WriteAllText(enrolPath, "placeholder");

            var configPath = Path.Combine(_tempDir, "config.txt");
            File.WriteAllLines(configPath, new[]
            {
                "STUDYCODE=OLD",
                "CENTRECODE=OLD",
                "SITENAME=OLD",
                "PRINCIPALINV=OLD",
                "IDFORMAT=0000",
                "IGNORENOID=FALSE",
                "IGNOREUNASSIGNED=FALSE",
                "REUSESTUDYID=FALSE",
                "BACKUPFILES=FALSE",
                "SAVETXT=FALSE",
                $"ENROLFILEPATH={enrolPath}"
            });

            var settings = new Form1.ConfigSettings();
            settings.LoadFromTxt(configPath);

            settings.studyCode = "NEWSTUDY";
            settings.centreCode = "NEWCENTRE";
            settings.siteName = "NEW SITE";
            settings.principalInv = "NEW PI";
            settings.IDFormat = "000000";
            settings.ignoreNoID = true;
            settings.ignoreUnassigned = true;
            settings.reuseStudyID = true;
            settings.backupFiles = true;
            settings.saveTxt = true;
            settings.enrolFilePath = enrolPath;

            settings.SaveToTxt(configPath);

            var output = File.ReadAllLines(configPath);
            Assert.IsTrue(output.Contains("STUDYCODE=NEWSTUDY"));
            Assert.IsTrue(output.Contains("CENTRECODE=NEWCENTRE"));
            Assert.IsTrue(output.Contains("SITENAME=NEW SITE"));
            Assert.IsTrue(output.Contains("PRINCIPALINV=NEW PI"));
            Assert.IsTrue(output.Contains("IDFORMAT=000000"));
            Assert.IsTrue(output.Contains("IGNORENOID=TRUE"));
            Assert.IsTrue(output.Contains("IGNOREUNASSIGNED=TRUE"));
            Assert.IsTrue(output.Contains("REUSESTUDYID=TRUE"));
            Assert.IsTrue(output.Contains("BACKUPFILES=TRUE"));
            Assert.IsTrue(output.Contains("SAVETXT=TRUE"));
        }
    }
}
