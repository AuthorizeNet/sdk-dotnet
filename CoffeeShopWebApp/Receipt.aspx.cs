using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AuthorizeNet;
using System.Configuration;

namespace CoffeeShopWebApp {
    public partial class Receipt : System.Web.UI.Page {

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
        
        protected void Page_Load(object sender, EventArgs e) {
            var order = (CoffeeShopWebApp.Model.CoffeeOrder)Session["order"];
            if (order == null) {
                Response.Redirect("/coffee.aspx");
            }
            imgMug.ImageUrl = "/content/images/mug_" + order.Slug + ".png";
            lblOrder.Text = order.OrderMessage;
        }

        protected void btnReturn_Click(object sender, EventArgs e) {
            var order = (CoffeeShopWebApp.Model.CoffeeOrder)Session["order"];

            if (order == null)
                Response.Redirect("/");

            //this is a return, or a Void
            //just need the transaction ID
            var gate = OpenGateway();

            //void it
            var request = new VoidRequest(order.TransactionID);
            var response = gate.Send(request);

            if (response.Approved) {
                order.AuthCode = response.AuthorizationCode;
                order.OrderMessage = "Your order was refunded - we've put a fresh pot on";

                //reset it
                Session["order"] = order;
                lblOrder.Text = order.OrderMessage;
            } else {
                //error... oops. Reload the page
                order.OrderMessage = response.Message;
            }

        }
    }
}
