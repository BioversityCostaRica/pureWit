using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    
    public class MessageStatusController : Controller
    {
        IConfirmaciones con;
        IEstados est;
        public MessageStatusController()
        {
            con = new MConfirmaciones();
            est = new MEstados();
        }

        [HttpPost]
        public ActionResult Index()
        {
            // Log the message id and status
            var smsSid = Request.Form["SmsSid"];
            var messageStatus = Request.Form["MessageStatus"];

            DATA.Estados estado = new DATA.Estados();
            if (messageStatus == "sent")
            {
                estado = est.ListarEstados().Where(x => x.Descripcion == "Enviado").First();
            } else
            {
                if (messageStatus == "delivered")
                {
                    estado = est.ListarEstados().Where(x => x.Descripcion == "Recibido").First();
                }
            }

            DATA.Confirmaciones confirmaciones = new DATA.Confirmaciones();
            confirmaciones.Estado = estado.Id;
            confirmaciones.Message_id = smsSid;

            con.InsertarConfirmaciones(confirmaciones);

            var logMessage = $"\"{estado.Id}\", \"{smsSid}\", \"{messageStatus}\"";

            /*
                Undelivered
                Failed
                Queued
                Accepted
                Sending
            */


            Trace.WriteLine(logMessage);
            return Content("Handled");
        }
    }
}