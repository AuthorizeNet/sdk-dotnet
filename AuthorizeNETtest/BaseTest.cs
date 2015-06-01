using NUnit.Framework;
using System;
using System.Configuration;

namespace AuthorizeNETtest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class BaseTest
    {
        protected static readonly WebRequestCreateLocal LocalRequestObject = new WebRequestCreateLocal();
        protected string ApiLogin;
        protected string TransactionKey;

        public BaseTest()
        {
#if USELOCAL
            WebRequest.RegisterPrefix("https://", LocalRequestObject);
#endif
        }

        /// <summary>
        /// CheckLoginPassword - make sure that we are not using the default invalid login and password.
        /// </summary>
        protected string CheckLoginPassword()
        {
            ApiLogin = AuthorizeNet.Test.UnitTestData.GetPropertyFromNames(AuthorizeNet.Util.Constants.EnvApiLoginid, AuthorizeNet.Util.Constants.PropApiLoginid);
            TransactionKey = AuthorizeNet.Test.UnitTestData.GetPropertyFromNames(AuthorizeNet.Util.Constants.EnvTransactionKey, AuthorizeNet.Util.Constants.PropTransactionKey);

            string sRet = "";
            if ((string.IsNullOrEmpty(ApiLogin)) || (ApiLogin.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKey)) || (TransactionKey.Trim().Length == 0))
            {
                LoadLoginTranskey();
            }

            if ((string.IsNullOrEmpty(ApiLogin)) || (ApiLogin.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKey)) || (TransactionKey.Trim().Length == 0))
            {
                sRet = "Invalid Login / Password: blank \n";
            }

#if !USELOCAL
            if ((ApiLogin == "ApiLogin") || (TransactionKey == "TransactionKey"))
            {
                sRet += "Invalid Login / Password \n";
            }
#endif
            return sRet;
        }

        private void LoadLoginTranskey()
        {
            ApiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            TransactionKey = ConfigurationManager.AppSettings["TransactionKey"];
        }

        protected decimal getValidAmount()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            return (decimal)rnd.Next(9999) / 100;
        }
    }
}
