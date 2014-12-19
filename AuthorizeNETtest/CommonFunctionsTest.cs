using AuthorizeNet;
using NUnit.Framework;
using System;
using System.Globalization;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for CommonFunctionsTest and is intended to contain all CommonFunctions Unit Tests
    /// </summary>
    [TestFixture()]
    public class CommonFunctionsTest
    {
        /// <summary>
        /// ParseDateTime - success
        /// </summary>
        [Test()]
        public void ParseDateTimeTest()
        {
            int year = 16; 
            int month = 2;
            int day = 1;
            DateTime dtExpected = new DateTime(2016, month, day);
            DateTime dt;

            bool actual = CommonFunctions.ParseDateTime(year, month, day, out dt);

            Assert.IsTrue(actual);
            Assert.AreEqual(dtExpected, dt);
        }

        /// <summary>
        /// ParseDateTime FR Culture - success
        /// </summary>
        [Test()]
        public void ParseDateTimeTest_FRCulture()
        {
            int year = 16;
            int month = 2;
            int day = 1;
            DateTime dtExpected = DateTime.Parse("2016-2-1", CultureInfo.CreateSpecificCulture("fr-FR"));

            DateTime dt;

            bool actual = CommonFunctions.ParseDateTime(year, month, day, out dt);

            Assert.IsTrue(actual);
            Assert.AreEqual(dtExpected, dt);
        }

        /// <summary>
        /// ParseDateTime year 99 - success
        /// </summary>
        [Test()]
        public void ParseDateTimeTest_99()
        {
            int year = 1999;
            int month = 2;
            int day = 1;
            DateTime dtExpected = new DateTime(year, month, day);

            DateTime dt;

            bool actual = CommonFunctions.ParseDateTime(99, month, day, out dt);

            Assert.IsTrue(actual);
            Assert.AreEqual(dtExpected, dt);
        }

        /// <summary>
        /// ParseDateTime year 00 - success
        /// </summary>
        [Test()]
        public void ParseDateTimeTest_00()
        {
            int year = 2000;
            int month = 2;
            int day = 1;
            DateTime dtExpected = new DateTime(year, month, day);

            DateTime dt;

            bool actual = CommonFunctions.ParseDateTime(00, month, day, out dt);

            Assert.IsTrue(actual);
            Assert.AreEqual(dtExpected, dt);
        }
    }
}
