<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Coffee.aspx.cs" Inherits="CoffeeShopWebApp.Coffee" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <fieldset class="centered">
        <div>
          <label class="orange">
            <img src="/content/images/mug_small.png">
            S
          </label>
          <input type="radio" name="id" value="small" checked="true">
          <br>
          $1.99
        </div>
        <div>
          <label class="orange">
            <img src="/content/images/mug_medium.png">
            M
          </label>
          <input type="radio" name="id" value="medium">
          <br>
          $2.99
        </div>
        <div>
          <label class="orange">
            <img src="/content/images/mug_large.png">
            L
          </label>
          <input type="radio" name="id" value="large">
          <br>
          $3.99
        </div>
      </fieldset>
      <div>
          <asp:Button ID="Button1" runat="server" CssClass="submit" 
              onclick="Button1_Click" Text="Continue" />
      </div>
        
</asp:Content>
