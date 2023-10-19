using Microsoft.VisualStudio.TestTools.UnitTesting;
using UBIKUserRights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBIKUserRights.Tests
{
    [TestClass()]
    public class UserRightsManagerTests
    {
        [TestMethod()]
        [DataRow("{\"WRITE\": [], \"READ\": []}")]
        public void GetRightsFromJsonTest(string json)
        {
            var result = UserRightsManager.GetRightsFromJson(json);
        }
    }
}