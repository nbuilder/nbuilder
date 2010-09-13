using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace NBuilderWebsite.Controllers
{
    public class IssuesController : Controller
    {
        //
        // GET: /Issues/

        public ActionResult Index()
        {
            return View();
        }

    }
}
