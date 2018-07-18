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
    
    public class LlamadasStatusController : Controller
    {
        IConfirmaciones con;
        IEstados est;
        public LlamadasStatusController()
        {
            con = new MConfirmaciones();
            est = new MEstados();
        }

        [HttpPost]
        public ActionResult Index()
        {
            // Log the message id and status
            var voiceSid = Request.Form["CallSid"];
            var llamadaStatus = Request.Form["CallStatus"];

            /*
                queued, ringing, in-progress, completed, busy, failed, no-answer
            */

            /*DATA.Estados estado = new DATA.Estados();
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

            con.InsertarConfirmaciones(confirmaciones);*/

            var logMessage = $"\"{voiceSid}\", \"{llamadaStatus}\"";

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