using Microsoft.VisualStudio.TestTools.UnitTesting;
using UBIKUserRights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBIK.Service.DTO.V240;

namespace UBIKUserRights.Tests
{
    [TestClass()]
    public class UserRightsManagerTests
    {
        [TestMethod()]
        [DynamicData(nameof(JsonData))]
        public void GetRightsFromJsonTest(string value)
        {
            IEnumerable<GroupRight> result = UserRightsManager.GetRightsFromJson(value);
            Assert.IsNotNull(result);
        }

        public static IEnumerable<object[]> JsonData
        {
            get
            {
                yield return new object[] { "{\"WRITE\": [\"A4TSWSWCKE\", \"A4TSWSWCWE\", \"A4TWWSWCWE\"],\"READ\": [\"A4TSWSWCWE\", \"A4TSWSACWE\", \"A4TSWSWTWE\"]}" };
            }
        }
    }
}