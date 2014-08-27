namespace AuthorizeNet.Api.Controllers.Test
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Util;

    [TestClass]
    public class CreateTransactionTest : ApiCoreTestBase
    {

        [ClassInitialize]
        public new static void SetUpBeforeClass(TestContext context)
        {
            ApiCoreTestBase.SetUpBeforeClass(context);
        }

        [ClassCleanup]
        public new static void TearDownAfterClass()
        {
            ApiCoreTestBase.TearDownAfterClass();
        }

        [TestInitialize]
        public new void SetUp()
        {
            base.SetUp();
        }

        [TestCleanup]
        public new void TearDown()
        {
            base.TearDown();
        }
    }
}
