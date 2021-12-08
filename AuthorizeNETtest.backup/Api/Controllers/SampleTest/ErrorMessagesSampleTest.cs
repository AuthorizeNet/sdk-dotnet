using AuthorizeNet.Utility;

namespace AuthorizeNet.Api.Controllers.SampleTest
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class ErrorMessagesSampleTest : ApiCoreTestBase
    {

        [TestFixtureSetUp]
        public new static void SetUpBeforeClass()
        {
            ApiCoreTestBase.SetUpBeforeClass();
        }

        [TestFixtureTearDown]
        public new static void TearDownAfterClass()
        {
            ApiCoreTestBase.TearDownAfterClass();
        }

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();
        }

        [TearDown]
        public new void TearDown()
        {
            base.TearDown();
        }

        [Test]
        public void TestErrorMessages_ARB_ExpiredCard()
        {
            var rnd = new AnetRandom(DateTime.Now.Millisecond);
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;


            //create a subscription with an invalid (expired) credit card in payment.
            var subscriptionDef = new ARBSubscriptionType
            {


                paymentSchedule = new paymentScheduleType
                {
                    interval = new paymentScheduleTypeInterval
                    {
                        length = 7,
                        unit = ARBSubscriptionUnitEnum.days
                    },
                    startDate = DateTime.UtcNow,
                    totalOccurrences = 2,
                },


                amount = SetValidSubscriptionAmount(Counter),
                billTo = new nameAndAddressType
                {
                    address = "1234 Elm St NE",
                    city = "Bellevue",
                    state = "WA",
                    zip = "98007",
                    firstName = "First",
                    lastName = "Last"
                },

                payment = new paymentType
                {
                    Item = new creditCardType
                                 {
                                     cardCode = "655",
                                     cardNumber = "4111111111111111",
                                     expirationDate = "122013", //deliberatly set payment to use expired CC
                                 }
                },

                customer = new customerType { email = "somecustomer@test.org", id = "5", },

                order = new orderType { description = string.Format("member monthly {0}", rnd.Next(99999)) },
            };

            var arbRequest = new ARBCreateSubscriptionRequest { subscription = subscriptionDef };
            var arbController = new ARBCreateSubscriptionController(arbRequest);
            arbController.Execute();

            var arbCreateResponse = arbController.GetApiResponse();

            //If request responds with an error, walk the messages and get code and text for each message.
            if (arbController.GetResultCode() == messageTypeEnum.Error)
            {
                foreach (var msg in arbCreateResponse.messages.message)
                {
                    Console.WriteLine("Error Num = {0}, Message = {1}", msg.code, msg.text);
                }
            }
        }
    }
}
