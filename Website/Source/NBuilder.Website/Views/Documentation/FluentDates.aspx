<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Fluent Dates</h2>
    
    <p>In keeping with NBuilder's philosphy of the readable, fluent interface, it also allows you to specify dates using a nice syntax.</p>
    
    <h3>Examples</h3>
    
    <p>There's no point going into detail about each one, so here's an idea of how to use it. The only thing to mention is that if you don't specify the year
    explicitly then it will use whatever the current year is.</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/dates/example1.gif" />
    </div>
    
    <h3>Using fluent dates and generators</h3>
    
    <p>Needless to say you can also use the dates when building objects. Here's an example of using the list builder and a generator.</p>    
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/dates/example2.gif" />
    </div>
    
    <%--<p>The <a href="/Generators">generators section</a> explains more about the different generators.</p>--%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
