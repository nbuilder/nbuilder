using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using NBuilderWebsite.Models;
using xVal.ServerSide;

namespace NBuilderWebsite.Controllers
{
    internal static class DataAnnotationsValidationRunner
    {
        public static IEnumerable<ErrorInfo> GetErrors(object instance)
        {
            return from prop in TypeDescriptor.GetProperties(instance).Cast<PropertyDescriptor>()
                   from attribute in prop.Attributes.OfType<ValidationAttribute>()
                   where !attribute.IsValid(prop.GetValue(instance))
                   select new ErrorInfo(prop.Name, attribute.FormatErrorMessage(string.Empty), instance);
        }
    }

    public class ContactController : Controller
    {
        //
        // GET: /Contact/
        public ActionResult Index()
        {
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(ContactEntry entry)
        {
            try
            {
                var errors = DataAnnotationsValidationRunner.GetErrors(entry).ToList();
                if (errors.Any())
                    throw new RulesException(errors);
            }
            catch (RulesException e)
            {
                e.AddModelStateErrors(ModelState, "entry");
                return View(entry);
            }

            var mailMessage = new MailMessage("website@nbuilder.org", "g@garethdown.com")
                                  {
                                      Subject = "Website contact"
                                  };

            var builder = new StringBuilder();
            builder.AppendFormat("Name: {0}\n", entry.Name);
            builder.AppendFormat("Email address: {0}\n\n", entry.EmailAddress);
            builder.AppendFormat("Message: {0}", entry.Message);

            mailMessage.Body = builder.ToString();

            var client = new SmtpClient();
            client.Send(mailMessage);

            return View("Thanks");
        }
    }
}