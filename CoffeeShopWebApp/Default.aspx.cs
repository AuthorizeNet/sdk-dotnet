using System;
using System.Configuration;
using CoffeeShopWebApp.Model;

namespace CoffeeShopWebApp {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];

            if ((string.IsNullOrEmpty(apiLogin)) || (apiLogin.Trim().Length == 0) || (apiLogin == "ApiLogin") 
                || (string.IsNullOrEmpty(transactionKey)) || (transactionKey.Trim().Length == 0) || (transactionKey == "TransactionKey"))
            {
                CoffeeOrder order = new CoffeeOrder("");
                order.OrderMessage = "Please replace the ApiLogin and TransactionKey values in Web.config file with your Authorize.Net account!";
                Session["order"] = order;
                Response.Redirect("Error.aspx");
            }
        }
    }
}
