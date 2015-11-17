using System;
using AuthorizeNet;
using System.Configuration;
using CoffeeShopWebApp.Model;
using System.Globalization;

namespace CoffeeShopWebApp {
    public partial class Order : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            var order = (CoffeeOrder)Session["order"];
            OrderID = order.OrderID.ToString();
            imgProduct.ImageUrl = "/content/images/mug_" + order.Slug + ".png";
        }
        //pretend this is injected with IoC
        IGateway OpenGateway() {
            //we used the form builder so we can now just load it up
            //using the form reader
            var login = ConfigurationManager.AppSettings["ApiLogin"];
            var transactionKey = ConfigurationManager.AppSettings["TransactionKey"];

            //this is set to test mode - change as needed.
            var gate = new Gateway(login, transactionKey, true);
            return gate;
        }
        protected void btnOrder_Click(object sender, EventArgs e) {
            var order = (CoffeeOrder)Session["order"];
            //pull from the store
            var gate = OpenGateway();

            //build the request from the Form post
            var apiRequest = CheckoutFormReaders.BuildAuthAndCaptureFromPost();
            
            //set the amount - you can also set this from the page itself
            //you have to have a field named "x_amount"
            apiRequest.Queue(ApiFields.Amount, order.Price.ToString());
            //You can set your solution ID here.
            apiRequest.SolutionID = "AAA100302";

            //send to Auth.NET
            var response = gate.Send(apiRequest);




            //be sure the amount paid is the amount required
            if (response.Amount < order.Price) {
                order.OrderMessage = "The amount paid for is less than the amount of the order. Something's fishy...";
                Session["order"] = order;
                Response.Redirect("Error.aspx");

            }

            if (response.Approved) {
                order.AuthCode = response.AuthorizationCode;
                order.TransactionID = response.TransactionID;
                order.OrderMessage = string.Format("Thank you! Order approved: {0}", response.AuthorizationCode);
                Session["order"] = order;
                //record the order, send to the receipt page
                Response.Redirect("Receipt.aspx");

            } else {

                //error... oops. Reload the page
                order.OrderMessage = response.Message;
                Session["order"] = order;
                Response.Redirect("Error.aspx");
            }
        }

        protected string SIMFormOpen()
        {
            var order = (CoffeeOrder)Session["order"];
            return SIMFormGenerator.OpenForm(ConfigurationManager.AppSettings["ApiLogin"],
                                             ConfigurationManager.AppSettings["TransactionKey"],
                                             order.Price, "", true);
        }

        protected string SIMFormEnd()
        {
            return SIMFormGenerator.EndForm();
        }


        protected string OrderID = "";
        protected string DPMFormOpen()
        {
            var order = (CoffeeOrder)Session["order"];
            return DPMFormGenerator.OpenForm(ConfigurationManager.AppSettings["ApiLogin"],
                                             ConfigurationManager.AppSettings["TransactionKey"],
                                             order.Price, "", true);
        }

        protected string DPMFormEnd()
        {
            return DPMFormGenerator.EndForm();
        }
    }
}
