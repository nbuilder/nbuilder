<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Download</h2>
    
    <h3>Latest release: 2.1.9 (beta)</h3>
    
     <a class="download" href="http://nbuilder.googlecode.com/files/FizzWare.NBuilder-2.1.9-beta.zip"><img src="../../Content/images/download.gif" border="0" /> Download it now</a>
    
    <!-- p><strong>Fixed in this release</strong></p>
    
    <ul>
        <li><a href="http://code.google.com/p/nbuilder/issues/detail?id=14&can=1">Issue 14</a> - Using an enum with a different underlying type to int causes exception</li>
        <li><a href="http://code.google.com/p/nbuilder/issues/detail?id=15&can=1">Issue 15</a> - Multiple WhereRandoms will conflict with each other and take items from each other's declarations</li>
    </ul -->
    
</asp:Content>