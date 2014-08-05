using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Net;

namespace AuthorizeNETtest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BaseTest
    {
        protected static WebRequestCreateLocal LocalRequestObject = new WebRequestCreateLocal();
        protected string ApiLogin = "";
        protected string TransactionKey = "";
        protected string ApiLoginCP = "";
        protected string TransactionKeyCP = "";

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
            ApiLogin = AuthorizeNet.Test.UnitTestData.GetPropertyFromNames(AuthorizeNet.Util.Constants.ENV_API_LOGINID, AuthorizeNet.Util.Constants.PROP_API_LOGINID);
            TransactionKey = AuthorizeNet.Test.UnitTestData.GetPropertyFromNames(AuthorizeNet.Util.Constants.ENV_TRANSACTION_KEY, AuthorizeNet.Util.Constants.PROP_TRANSACTION_KEY);
            ApiLoginCP = AuthorizeNet.Test.UnitTestData.GetPropertyFromNames(AuthorizeNet.Util.Constants.ENV_CP_API_LOGINID, AuthorizeNet.Util.Constants.PROP_CP_API_LOGINID);
            TransactionKeyCP = AuthorizeNet.Test.UnitTestData.GetPropertyFromNames(AuthorizeNet.Util.Constants.ENV_CP_TRANSACTION_KEY, AuthorizeNet.Util.Constants.PROP_CP_TRANSACTION_KEY);

            string sRet = "";
            if ((string.IsNullOrEmpty(ApiLogin)) || (ApiLogin.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKey)) || (TransactionKey.Trim().Length == 0)
                || (string.IsNullOrEmpty(ApiLoginCP)) || (ApiLoginCP.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKeyCP)) || (TransactionKeyCP.Trim().Length == 0))
            {
                LoadLoginTranskey();
            }

            if ((string.IsNullOrEmpty(ApiLogin)) || (ApiLogin.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKey)) || (TransactionKey.Trim().Length == 0)
                || (string.IsNullOrEmpty(ApiLoginCP)) || (ApiLoginCP.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKeyCP)) || (TransactionKeyCP.Trim().Length == 0))
            {
                sRet = "Invalid Login / Password: blank \n";
            }

#if !USELOCAL
            if ((ApiLogin == "ApiLogin") || (TransactionKey == "TransactionKey")
                || (ApiLoginCP == "ApiLoginCP") || (TransactionKeyCP == "TransactionKeyCP"))
            {
                sRet += "Invalid Login / Password \n";
            }
#endif
            return sRet;
        }

        protected void LoadLoginTranskey()
        {
            ApiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            TransactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            ApiLoginCP = ConfigurationManager.AppSettings["ApiLoginCP"];
            TransactionKeyCP = ConfigurationManager.AppSettings["TransactionKeyCP"];
        }
    }
}
