<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactEntry>" %>
<%@ Import Namespace="NBuilderWebsite.Models"%>
<%@ Import Namespace="xVal.Html"%>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="../../Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../Scripts/xVal.jquery.validate.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function() 
        {
            $('#myform').validate().settings.errorPlacement = function(error, element) 
            {
                element.before(error);
                offset = element.offset();
                error.css('left', offset.left);
                error.css('top', offset.top - element.outerHeight());
            };

//            $('.textbox').not('.input-validation-error').focus(function() 
//            {
//                $(this).css('background-color', '#D7E3FF');
//                $(this).css('border-color', '#00287D');
//            });

//            $('.textbox').not('.input-validation-error').blur(function()
//            {
//                $(this).css('background-color', '#fff');
//                $(this).css('border-color', '#ccc');
//            });
        });
    </script>

</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Contact</h2>
    
    <form method="post" id="myform">
        
        <p>Please get in touch</p>
        
        <div class="form-item">
            <div class="label"><label for="entry.name">Name:</label></div>
            <div class="field">
                <%= Html.ValidationMessage("entry.Name")%>
                <%= Html.TextBox("entry.Name")%>
            </div>
        </div>
        
        <div class="form-item">
            <div class="label"><label for="entry.EmailAddress">Email address:</label></div>
            <div class="field">
                <%= Html.ValidationMessage("entry.EmailAddress")%>
                <%= Html.TextBox("entry.EmailAddress") %>
            </div>
        </div>
        
        <div class="form-item">
            <div class="label"><label for="entry.message">Message:</label></div>
            <div class="field">
                <%= Html.ValidationMessage("entry.Message")%>
                <%= Html.TextArea("entry.Message")%>
            </div>
        </div>
        
        <input type="submit" value="Send" />
    
    </form>
    
    <%= Html.ClientSideValidation<ContactEntry>("entry") %>

</asp:Content>
