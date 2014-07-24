namespace AuthorizeNet.BAD
{
#pragma warning disable 1591

    // DeviceType is used for Card Present transactions.
    public enum DeviceType {
	    UNKNOWN = 1,
	    UNATTENDED = 2,
	    SELF_SERVICE_TERMINAL = 3,
	    ELECTRONIC_CASH_REGISTER = 4,
	    PERSONAL_COMPUTER_BASED_TERMINAL = 5,
	    AIRPAY = 6,
	    WIRELESS_POS = 7,
	    WEBSITE = 8,
	    DIAL_TERMINAL = 9,
	    VIRTUAL_TERMINAL = 10,
    }
#pragma warning restore 1591

}