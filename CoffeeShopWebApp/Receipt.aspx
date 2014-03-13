<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="CoffeeShopWebApp.Receipt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<section class = "centered">	
	
	<p>
	    <h1><asp:Label ID=lblOrder runat=server></asp:Label> </h1>    
    </p>
    <asp:Image ID=imgMug runat=server />
	
	<p>Yummy! Drink up...</p>
    <p>
        <asp:Button ID=btnReturn runat=server CssClass=refund Text="Don't Like It?" 
            onclick="btnReturn_Click"/>
    </p>

</section>

</asp:Content>
