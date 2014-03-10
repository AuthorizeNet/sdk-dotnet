<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="CoffeeShopWebApp.Order" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        <asp:Image ID=imgProduct runat=server />
    </p>
    <p>
        <%=AuthorizeNet.Helpers.CheckoutFormBuilders.CreditCardForm(true) %>
    </p>
    <div style="clear:both"></div>
    <hr />
    <p>
     <asp:Button 
        ID="btnOrder" runat="server" CssClass="submit" onclick="btnOrder_Click" 
        Text="Order" />   
    </p>

</asp:Content>

<asp:Content ID="ContentBottom" ContentPlaceHolderID="BottomContent" runat="server">

    <p>
        <%=this.SIMFormOpen() %>
        <input type = "submit" class="submit" value = "Order with SIM!" />
        <%=this.SIMFormEnd() %>
    </p>

</asp:Content>
