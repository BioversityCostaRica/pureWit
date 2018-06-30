using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace WiserSoft.UI.Controllers
{
    public class DifusionController : Controller
    {
        // GET: Difusion
        public ActionResult Index()
        {
            return View();
        }

        /*
        var dateNow = DateTime.Now;
        var dateSend = new DateTime(2018, 06, 29, 11, 37, 00);
        TimeSpan ts;
        ts = dateSend - dateNow;
        Console.WriteLine(ts);
        //waits certan time and run the code
        string[] numeros = new string[2];
        numeros[0] = "13863138548";
        numeros[1] = "13863138548";

        string accountSid = "AC8e9e74867a3d20837dead2c3feb04c27";
        string authToken = "2f872962f965e4ae1068292d127176b0";
        Task.Delay(ts).ContinueWith((x) => enviarMensajes(numeros, "14159431126", accountSid, authToken, "Hola desde la tarea en c# con Twilio."));
        */
        static void enviarMensajes(string[] listaContactos, string UserPhone, string accountSid, string authToken, string mensaje)
        {

            TwilioClient.Init(accountSid, authToken);

            foreach (string remitente in listaContactos)
            {
                var message = MessageResource.Create(
                    body: mensaje,
                    from: new Twilio.Types.PhoneNumber("+" + UserPhone),
                    to: new Twilio.Types.PhoneNumber("+" + remitente)
                );
                Console.WriteLine(message.Sid);
            }
        }
    }
}