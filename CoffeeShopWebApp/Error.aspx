<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CoffeeShopWebApp.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="lblError" ForeColor="red" Text="Error:"></asp:Label><br/>
    <asp:Label runat="server" ID="lblMessage" ForeColor="red"></asp:Label>
</asp:Content>
