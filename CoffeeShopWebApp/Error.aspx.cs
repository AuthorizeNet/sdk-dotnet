using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoffeeShopWebApp.Model;

namespace CoffeeShopWebApp {
    public partial class Error : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ((CoffeeOrder) Session["order"]).OrderMessage;
        }
    }
}
