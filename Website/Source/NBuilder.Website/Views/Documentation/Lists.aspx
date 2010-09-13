<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Lists</h2>
    
    <p>One of NBuilder's most useful features is its ability to create lists.</p>
    
    <p>NBuilder has two different property namers, a sequential namer and a random namer. The default namer is the sequential one. 
    This is probably the most useful for most scenarios because you know exactly what it is going to produce.</p>
    
    <p>It always starts from one and gives ascending values to the properties for each object it builds.</p>
      
    <h3>Creating a basic list</h3>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/lists/example1.gif" />
    </div>
    
    <p>NBuilder will name all public properties and fields but it won't touch private, protected or internal ones.</p>
    
    <p>It will create a list whose properties are named like this:</p>
    
    <img src="../../Content/images/example-4-output.gif" />
    
    <h3>Using WhereAll()</h3>
    
    <p>You can specify that different parts of the list have certain property values. These are called 'declarations'. The most basic declaration is WhereAll(). 
    When you declare a set of objects, you always follow it with what you want to do to that set of objects. In this case Have() is being used. Have is the same as the 
    single object builder's With() method.</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/lists/example2.gif" />
    </div>
    
    <p>One common use for WhereAll() is if you want to insert a load of objects into the database. Most ORMs will treat an object with an ID of 0 as a new object so 
    to do this you would say <pre>WhereAll().Have(x => x.Id = 0).</pre></p>
    
    <h3>WhereTheFirst(), WhereTheLast()</h3>
    
    <p>There are some methods provided to quickly declare the first or last x objects.</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/lists/example3.gif" />
    </div>
    
    <p>All these methods (apart from WhereAll()) are in fact extension methods. It follows then that you can add your own WhereXXX methods to NBuilder, simply by writing
    an extension method.</p>
    
    <h3>AndTheNext(), AndThePrevious()</h3>
    
    <p>When you use WhereTheFirst() you can follow it with multiple calls to AndTheNext()</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/lists/example4.gif" />
    </div>
    
    <p>As you might expect there's also a WhereTheLast() and AndThePrevious as well</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/lists/example5.gif" />
    </div>
    
    <h3>WhereSection(x, y)</h3>
    
    <p>You can use WhereSection() to apply rules to segments of the list</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/lists/example6.gif" />
    </div>
    
    <h3>Calling a method on the objects of a declaration</h3>
    
    <p>If you want to call a method on a set of objects, there are the HaveDoneToThem() and HasDoneToIt() methods</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/lists/example7.gif" />
    </div>
    
    <h3>Picking random items</h3>
    
    <p>If you want to pick an item or some items at random, and you don't care which they are, you can use the Pick class.</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/lists/example8.gif" />
    </div>
    
    <p>You can also pick a unique random list using the Pick class</p>
    
    <div class="code">
        <p></p>
        <img src="../../Content/images/doc/lists/example9.gif" />
    </div>
            
    <p>&nbsp;</p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
