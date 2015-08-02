namespace AuthorizeNet
{
#pragma warning disable 1591
    using System;

    [Serializable]
    public class Merchant {

	    public const string CpVersion = "1.0";	// card present version
	    public const int MaxLoginLength = 20;
	    public const int MaxTransactionKeyLength = 16;

        protected Environment Environment { get; set; }
        protected string Login { get; set; }
        protected string TransactionKey { get; set; }
        protected bool AllowPartialAuth { get; set; }
        protected MarketType MarketType { get; set; }
        protected DeviceType DeviceType { get; set; }
        protected string UserRef { get; set; }
        protected string Md5Value { get; set; }

        private Merchant() 
        {
            Environment = Environment.SANDBOX;
	    }

	    public static Merchant CreateMerchant(Environment environment, string login, string transactionKey) {

		    var merchant = new Merchant {Environment = environment, Login = login, TransactionKey = transactionKey};

	        return merchant;
	    }

	    public bool IsSandboxEnvironment() {
		    return (Environment !=null &&
			    (Environment.SANDBOX == Environment));
	    }
    }
#pragma warning restore 1591
}