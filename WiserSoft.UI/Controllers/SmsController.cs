using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WiserSoft.UI.Controllers
{
    public class SmsController : Controller
    {
        [HttpPost]
        public ActionResult Index()
        {
            string cuerpoDeRespuesta = Request.Form["Body"];
            string remitente = Request.Form["From"];
            string emisor = Request.Form["To"];

            var logMessage = $"\"{cuerpoDeRespuesta}\", \"{remitente}\", \"{emisor}\"";
            Trace.WriteLine(logMessage);

            return Content("Handled");
        }
    }
}