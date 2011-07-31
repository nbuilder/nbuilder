using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Dapper;
using NBuilderWebsite.Models;
//using xVal.ServerSide;

namespace NBuilderWebsite.Controllers
{
    //internal static class DataAnnotationsValidationRunner
    //{
    //    public static IEnumerable<ErrorInfo> GetErrors(object instance)
    //    {
    //        return from prop in TypeDescriptor.GetProperties(instance).Cast<PropertyDescriptor>()
    //               from attribute in prop.Attributes.OfType<ValidationAttribute>()
    //               where !attribute.IsValid(prop.GetValue(instance))
    //               select new ErrorInfo(prop.Name, attribute.FormatErrorMessage(string.Empty), instance);
    //    }
    //}

    public class ContactController : Controller
    {
        public ContactController()
        {
            
        }

        //
        // GET: /Contact/
        public ActionResult Index()
        {
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(ContactEntry entry)
        {
            //try
            //{
            //    var errors = DataAnnotationsValidationRunner.GetErrors(entry).ToList();
            //    if (errors.Any())
            //        throw new RulesException(errors);
            //}
            //catch (RulesException e)
            //{
            //    e.AddModelStateErrors(ModelState, "entry");
            //    return View(entry);
            //}

            SaveToDatabase(entry);
            SendEmail(entry);

            return View("Thanks");
        }

        public ActionResult ViewAll()
        {
            using (SqlConnection cnn = CreateConnection())
            {
                cnn.Open();
                var results = cnn.Query<ContactEntry>("select * from ContactEntry");

                return View(results);
            }
        }

        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        }

        private void SaveToDatabase(ContactEntry entry)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                cnn.Open();
                cnn.Execute("INSERT ContactEntry(Name, EmailAddress, Message) VALUES (@name, @emailaddress, @message)", new[] { new { name = entry.Name, emailaddress = entry.EmailAddress, message = entry.Message } });
            }
        }

        private static void SendEmail(ContactEntry entry)
        {
            var mailMessage = new MailMessage("website@nbuilder.org", "g@garethdown.com")
                                  {
                                      Subject = "Website contact"
                                  };

            var builder = new StringBuilder();
            builder.AppendFormat("Name: {0}\n", entry.Name);
            builder.AppendFormat("Email address: {0}\n\n", entry.EmailAddress);
            builder.AppendFormat("Message: {0}", entry.Message);

            mailMessage.Body = builder.ToString();

            try
            {
                var client = new SmtpClient();
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}