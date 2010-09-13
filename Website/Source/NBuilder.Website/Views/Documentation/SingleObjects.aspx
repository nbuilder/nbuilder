<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Single Objects</h2>
    
    <h3>Creating a single object</h3>
    <p>This will build an object with default values for all the properties that NBuilder is able to set.</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/singobj-1.gif" />
    </div>
    
    
    <h3>Setting the value of a property</h3>
    <p>This will assign default values to everything apart from the description to which it will assign &quot;A custom description here&quot;</p>    
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/singobj-2.gif" />
    </div>
    
    
    <h3>Setting more than one property</h3>
    <p>You can set any number of properties on the object. And() internally is in fact exactly the same as With(). It is provided as an option for improved readability.</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/singobj-3.gif" />
    </div>
    
    
    <h3>Supplying constructor parameters</h3>
    <p>Given you have a type that has a constructor:</p>
    
    <div class="code">
        <p></p>
    <img src="../../Content/images/doc/singobj-4.gif" />
    </div>
    
    <p>You can supply constructor args using the WithConstructorArgs() method:</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/singobj-5.gif" />
    </div>
    
    <h3>Calling a method on an object</h3>
    
    <p>You can use Do() to call a method on an object</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/singobj-6.gif" />
    </div>
    
    <p>Do(), And()</p>    
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/singobj-7.gif" />
    </div>
    
    <h3>Using &quot;multi functions&quot;</h3>
    
    <p>If you want to call the same method for each item of a list, you can use DoForAll()</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/singobj-8.gif" />
    </div>
    
    <p>&nbsp;</p>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>