using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Invoicing.Models;

namespace Invoicing.Controllers
{
    public class BaseController : Controller
    {
        public void TracingJ (string message)
        {
            using (var db = new MyDbContext()) {
                db.Tracings.Add (new Tracing() { Message = message, DateTime=DateTime.Now } );
                db.SaveChanges();
            }
        }

        // GET: Base
        public string GetWebErrorDescription(WebException exception)
        {
            string responseText ="";
            if (exception.Response != null)
            {
                var responseStream = exception.Response.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
            }
            return responseText;
        }
    }
}