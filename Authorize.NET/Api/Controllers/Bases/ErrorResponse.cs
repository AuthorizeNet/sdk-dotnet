namespace AuthorizeNet.Api.Controllers.Bases
{
    using System.Text;
    using AuthorizeNet.Api.Contracts.V1;

    //@XmlRootElement(name = "ErrorResponse")
    /**
     * Since JAXB does not generate the class for this element, custom coding it
     * @author ramittal
     *
     */
    abstract class ErrorResponse : ANetApiResponse {

	    public new string ToString() {
		     var builder = new StringBuilder();
		     builder.Append("ErrorResponse: ");
		     builder.Append(base.ToString());
		     builder.Append(", Id: ").Append( refId);
		     builder.Append(", SessionToken: ").Append(sessionToken);
		     var messagesType = messages;
		     builder.Append(", MessagesType: ");
		     if ( null != messagesType)
		     {
			     builder.Append(", ResultCode:").Append(messagesType.resultCode);
                 var resultMessages = messagesType.message;
			     if ( null != resultMessages) {
                     foreach (var message in resultMessages)
                     {
					     builder.Append(", Message-> ");
					     builder.Append(", Code: ").Append(message.code);
					     builder.Append(", Text: ").Append(message.text);
				     }
			     }
		     }
		    
	         return builder.ToString();
       }
    }
}