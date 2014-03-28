using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoffeeShopWebApp.Model;

namespace CoffeeShopWebApp {
    public partial class Coffee : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void Button1_Click(object sender, EventArgs e) {

            if (Request.Form["id"] == null) {
                Response.Redirect("/Coffee.aspx");
            }
            var order = new CoffeeOrder(Request.Form["id"].ToString());

            Session["order"] = order;
            Response.Redirect("Order.aspx");
        }

    }
}
