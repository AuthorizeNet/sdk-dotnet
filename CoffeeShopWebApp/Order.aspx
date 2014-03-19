<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="CoffeeShopWebApp.Order" %>
<%@ Import Namespace="CoffeeShopWebApp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        <asp:Image ID=imgProduct runat=server />
    </p>
    <p>
        <%=CheckoutFormBuilders.CreditCardForm(true) %>
    </p>
    <div style="clear:both"></div>
    <p>
     <asp:Button 
        ID="btnOrder" runat="server" CssClass="submit" onclick="btnOrder_Click" 
        Text="Order" />   
    </p>
    <hr />
    
</asp:Content>

<asp:Content ID="ContentBottom" ContentPlaceHolderID="BottomContent" runat="server">

    <p>
        <%=this.SIMFormOpen() %>
        <input type = "submit" class="submit" value = "Order with SIM!" />
        <%=this.SIMFormEnd() %>
    </p>
    <hr />
    
    <div style="clear:both"></div>
    <p>
        <%=this.DPMFormOpen() %>		
        <input type="hidden" name="order_id" value="<% =OrderID %>"/>
        <p>
		    <div style = 'float:left;width:250px;'>
			    <label>Credit Card Number</label>
			    <div id = 'CreditCardNumber'>
				    <input type = 'text' size = '28' name = 'x_card_num' value = '4111111111111111' id = 'x_card_num'/>
			    </div>
		    </div>	
		    <div style = 'float:left;width:70px;'>
			    <label>Exp.</label>
			    <div id = 'CreditCardExpiration'>
				    <input type = 'text' size = '5' maxlength = '5' name = 'x_exp_date' value = '0116' id = 'x_exp_date'/>
			    </div>
		    </div>
		    <div style = 'float:left;width:70px;'>
			    <label>CCV</label>
			    <div id = 'CCV'>
				    <input type = 'text' size = '5' maxlength = '5' name = 'x_card_code' id = 'x_card_code' value = '123' />
			    </div>
		    </div>
		</p>
        <div style="clear:both"></div>
        <p>
		    <input type = "submit" class="submit" value = "Order with DPM!" />
        </p>
        <%=this.DPMFormEnd() %>
    </p>
    <hr />
        
</asp:Content>
