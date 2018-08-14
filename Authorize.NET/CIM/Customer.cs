using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthorizeNet {

    public enum ValidationMode {
        None,
        TestMode,
        LiveMode,
    }

    /// <summary>
    /// This is an abstraction for use with the CIM API. It's a partial class so you can combine it with your class as needed.
    /// </summary>

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For CIM, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/CustomerProfiles
    [Obsolete("AuthorizeNetCIM is deprecated, use AuthorizeNet::API instead. For CIM, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/CustomerProfiles.", false)]
    public partial class Customer {


        public Customer() {
            this.ID = "MerchantCustomerID"; 
            this.ShippingAddresses = new List<Address>();
            this.PaymentProfiles = new List<PaymentProfile>();
        }

        public string ID { get; set; }
        public string ProfileID { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public Address BillingAddress { get; set; }
        public IList<Address> ShippingAddresses { get; set; }
        public IList<PaymentProfile> PaymentProfiles { get; set; }

        internal static AuthorizeNet.APICore.validationModeEnum ToValidationMode(ValidationMode mode) {
            switch (mode) {
                case ValidationMode.None: return AuthorizeNet.APICore.validationModeEnum.none;
                case ValidationMode.TestMode: return AuthorizeNet.APICore.validationModeEnum.testMode;
                case ValidationMode.LiveMode: return AuthorizeNet.APICore.validationModeEnum.liveMode;
                default: return (AuthorizeNet.APICore.validationModeEnum)mode;
            }
        }
    }
}
