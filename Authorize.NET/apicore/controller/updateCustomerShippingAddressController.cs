namespace authorizenet.apicore.controller
{
    using System;
    using authorizenet.apicore.contract.v1;
    using authorizenet.apicore.controller.bases;

#pragma warning disable 1591
    public class updateCustomerShippingAddressController : ApiOperationBase<updateCustomerShippingAddressRequest, updateCustomerShippingAddressResponse> {

	    public updateCustomerShippingAddressController(updateCustomerShippingAddressRequest apiRequest) : base(apiRequest) {
	    }

	    override protected void ValidateRequest() {
            var request = GetApiRequest();
		
		    //validate required fields		
		    //if ( 0 == request.SearchType) throw new ArgumentException( "SearchType cannot be null");
		    //if ( null == request.Paging) throw new ArgumentException("Paging cannot be null");
		
		    //validate not-required fields		
	    }
    }
#pragma warning restore 1591
}