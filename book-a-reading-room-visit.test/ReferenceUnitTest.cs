using Amazon.Runtime.Internal.Transform;
using book_a_reading_room_visit.web.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.test
{
    [TestClass]
    public class ReferenceUnitTest
    {
        private readonly Dictionary<string, bool> _expectedResults = new Dictionary<string, bool>()
        {
            {"FO 371", false },
            {"FO 371/1", true },
            {"FO371", false },
            {"FO371/1", true },
            {"FO/371", false },
            {"CP 24", false },
            {"CP24", false },
            {"CP 24/1", true },
            {"CP24/1", true },
            {"CP 25", false },
            {"CP25", false },
            {"CP 25/1", true },
            {"CP25/1", true },
            {"CP 26", false },
            {"CP26", false },
            {"CP 26/1", true },
            {"CP26/1", true },
            {"IR 121", false },
            {"IR121", false },
            {"IR 121/1", true },
            {"IR121/1", true },
            {"IR 124", false },
            {"IR124", false },
            {"IR 124/1", true },
            {"IR124/1", true },
            {"IR 125", false },
            {"IR125", false },
            {"IR 125/1", true },
            {"IR125/1", true },
            {"IR 126", false },
            {"IR126", false },
            {"IR 126/1", true },
            {"IR126/1", true },
            {"IR 127", false },
            {"IR127", false },
            {"IR 127/1", true },
            {"IR127/1", true },
            {"IR 128", false },
            {"IR128", false },
            {"IR 128/1", true },
            {"IR128/1", true },
            {"IR 129", false },
            {"IR129", false },
            {"IR 129/1", true },
            {"IR129/1", true },
            {"IR 130", false },
            {"IR130", false },
            {"IR 130/1", true },
            {"IR130/1", true },
            {"IR 131", false },
            {"IR131", false },
            {"IR 131/1", true },
            {"IR131/1", true },
            {"IR 132", false },
            {"IR132", false },
            {"IR 132/1", true },
            {"IR132/1", true },
            {"IR 133", false },
            {"IR133", false },
            {"IR 133/1", true },
            {"IR133/1", true },
            {"IR 134", false },
            {"IR134", false },
            {"IR 134/1", true },
            {"IR134/1", true },
            {"IR 135", false },
            {"IR135", false },
            {"IR 135/1", true },
            {"IR135/1", true },
            {"PRO 30", false },
            {"PRO30", false },
            {"PRO 30/1", true },
            {"PRO30/1", true },
            {"PRO 31", false },
            {"PRO31", false },
            {"PRO 31/1", true },
            {"PRO31/1", true },
            {"PRO 41", false },
            {"PRO41", false },
            {"PRO 41/1", true },
            {"PRO41/1", true },
            {"PRO 66", false },
            {"PRO66", false },
            {"PRO 66/1", true },
            {"PRO66/1", true },
            {"YO 371/1", false },
            {"E 134/1WandM/Hil2", true},
            {"C 2/JasI/B27/11", true },
            {"E 317/Beds/3", true },
            {"E 210/9962/viii", true }
        };

        private readonly Dictionary<string, bool> _expectedResultsParlArchives = new Dictionary<string, bool>()
        {
                        // Additional test cases for Parliamentary archives.  These have a / instead of a space after the letter code.
            // Initial letter must be a Y .
            {"YHL/123/456/789/1", true },
            {"YHC/123/456/789/1", true },
            {"YHL123/456/789/1", false },  // Does not match Parliamentary archive regex,
            {"YHC123/456/789/1", false },  // nor the general one as first letter Y. 
            {"AHL/123/456/789/1", false },
            {"BHC/123/456/789/1", false },
            {"YHL/PO/PU/1/1806/46G3n59", true }
        };

        private readonly CheckReference _checkReferenceAttribute = new CheckReference();
        private readonly CheckSeries _checkSeriesAttribute = new CheckSeries();

        [TestMethod]
        public Task References_Validate_As_Expected()
        {
            foreach (string reference in _expectedResults.Keys)
            {
                Assert.AreEqual(_expectedResults[reference], _checkReferenceAttribute.IsValid(reference));
            }

            return Task.CompletedTask;
        }

        [TestMethod]
        public Task Parly_Archive_References_Validate_As_Expected()
        {
            foreach (string reference in _expectedResultsParlArchives.Keys)
            {
                Assert.AreEqual(_expectedResultsParlArchives[reference], _checkReferenceAttribute.IsValid(reference));
            }

            return Task.CompletedTask;
        }

        [TestMethod]
        public Task Series_Ref_Single_Space_IsValid()
        {
            string series = "FO 371";
            Assert.IsTrue(_checkSeriesAttribute.IsValid(series));
            return Task.CompletedTask;
        }

        [TestMethod]
        public Task Series_Ref_No_Space_NotValid()
        {
            string series = "FO371";
            Assert.IsFalse(_checkSeriesAttribute.IsValid(series));
            return Task.CompletedTask;
        }

        [TestMethod]
        public Task Series_Ref_Multi_Space_NotValid()
        {
            string series = "FO  371";
            Assert.IsFalse(_checkSeriesAttribute.IsValid(series));
            return Task.CompletedTask;
        }
    }
}
