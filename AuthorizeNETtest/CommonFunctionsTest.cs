using AuthorizeNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for CommonFunctionsTest and is intended to contain all CommonFunctions Unit Tests
    /// </summary>
    [TestClass()]
    public class CommonFunctionsTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        /// ParseDateTime - success
        /// </summary>
        [TestMethod()]
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
        [TestMethod()]
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

    }
}
