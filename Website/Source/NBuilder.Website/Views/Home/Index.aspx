<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>What is it?</h2>
        
        <p>Through a fluent, extensible interface, NBuilder allows you to rapidly create test data, automatically assigning values to properties and public fields that are of type of the built in .NET data types (e.g. ints and strings). NBuilder allows you to override for properties you are interested in using lambda expressions.</p>
        
        <p>NBuilder is an open source project, hosted on <a href="http://code.google.com/p/nbuilder">google code</a></p>
        
        <h2>Some quick examples</h2>
        
        <div class="example">        
            <h3>Example #1</h3>
            <div class="code">
            <p><img src="/Content/images/example-1-code.gif" /></p>
            </div>
            <h3>Would give you something like this:</h3>
            <p><img src="/Content/images/example-1-output.gif" width="569" height="241" /></p>
        </div>
        
        <div class="example">        
            <h3>Example #2</h3>
            <p><img src="/Content/images/example-2-code.gif" width="778" height="164" /></p>
            <h3>Would give you something like this:</h3>
            <p><img src="/Content/images/example-2-output.gif" width="569" height="241" /></p>
        </div>
        
        <div class="example">        
            <h3>Example #3</h3>
            <p><img src="/Content/images/example-3-code.gif" width="777" height="251" /></p>
            <h3>Would give you something like this:</h3>
            <p><img src="/Content/images/example-3-output.gif" width="569" height="241" /></p>
        </div>
</asp:Content>
