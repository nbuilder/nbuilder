<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Configuration</h2>
    
    <p>NBuilder allows you to change its default behaviour using the <strong>BuilderSetup</strong> class.</p>
    
    <h3>Specifying a custom persistence service</h3>
    
    <p>You can completely replace the default persistence service with your own. All you need to do is inherit from IPersistenceService and specify it using BuilderSetup.SetPersistenceService()</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/configuration/example1.gif" />
    </div>
    
    <h3>Specifying a custom property namer</h3>
    
    <p>You can specify a different property namer. Out of the box the ones available are the default SequentialPropertyNamer and the RandomValuePropertyNamer</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/configuration/example4.gif" />
    </div>
    
    <h3>Setting a property namer for a specific type</h3>
    
    <p>If you need to override the property naming for a particular type you can use the SetPropertyNamerFor&lt;T&gt; method.</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/configuration/example2.gif" />
    </div>
    
    <h3>Disabling property naming for an individual property</h3>
    
    <p>You can switch off property naming using the DisablePropertyNamingFor() method</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/configuration/example3.gif" />
    </div>
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
