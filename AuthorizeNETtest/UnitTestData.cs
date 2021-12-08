using System;

using AuthorizeNet.Util;

namespace AuthorizeNet.Test
{
	public abstract class UnitTestData
	{
		protected static string ApiLoginId;
		protected static string TransactionKey;
		protected static string MerchantMd5Key;

		private static readonly Log Logger = LogFactory.GetLog(typeof(UnitTestData));

		/**
         * Default static constructor
         */
		static UnitTestData()
		{
			//getPropertyFromNames get the value from properties file or environment
			ApiLoginId = GetPropertyFromNames(Constants.EnvApiLoginid, Constants.PropApiLoginid);
			TransactionKey = GetPropertyFromNames(Constants.EnvTransactionKey, Constants.PropTransactionKey);
			MerchantMd5Key = GetPropertyFromNames(Constants.EnvMd5Hashkey, Constants.PropMd5Hashkey);

			if (null == ApiLoginId || null == TransactionKey)
			{
				throw new ArgumentException("LoginId and/or TransactionKey have not been set.");
			}
			else
			{
				LogHelper.Info(Logger, "PropertyValues: ApiLoginId:'{0}', TransactionKey:'{1}', MD5Key:'{2}' ", ApiLoginId, TransactionKey, MerchantMd5Key);
			}
		}

		public static string GetPropertyFromNames(string pFirstName, string pSecondName)
		{
			var value = Environment.GetProperty(pFirstName) ??
									Environment.GetProperty(pSecondName);

			return value;
		}
	}
}