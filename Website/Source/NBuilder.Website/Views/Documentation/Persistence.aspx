<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Persistence</h2>
    
    <p>NBuilder also has another method called Persist(). It does the same thing as Build() but once the objects have been built it will call a predefined 
    method to persist the objects somewhere.</p>
    
    <p>The first thing you need to do is tell NBuilder how to persist objects of a certain type. You do this with the BuilderSetup class. (You can read more about 
    BuilderSetup in the <a href="/Documentation/Configuration">configuration section</a>).</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/persistence/example1.gif" />
    </div>
    
    <p>You only need to do this once per test run. Once this is done you simply call Persist() instead.</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/persistence/example2.gif" />
    </div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
