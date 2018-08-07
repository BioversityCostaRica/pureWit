using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class RespuestasController : Controller
    {
        IRespuestas Rest;
        IContactos cont;
        IComunicaciones com;
        ITelefonos tel;
        // GET: Respuestas
        public RespuestasController()
        {
            Rest = new MRespuestas();
            cont = new MContactos();
            com  = new MComunicaciones();
            tel  = new MTelefonos();
        }

        public ActionResult Index()
        {
            ViewBag.userId = Session["Username"];

            var lista = cont.ListarContactos().Where(x => x.Username == Session["Username"].ToString());
            var contactos = Mapper.Map<List<Models.Contactos>>(lista);

           

            return View(contactos);
        }

        public ActionResult Mensajes(int Id_Contacto)
        {
            ViewBag.Contacto = Id_Contacto;
            ViewBag.prueba = Session["Username"];
            return View();
        }

        [HttpPost]
        public ActionResult Mensajes(Models.Comunicaciones comunicaciones)
        {
            DATA.Telefonos telefono = new DATA.Telefonos();
            telefono = tel.ListarTelefonos().Where(x => x.Username == Session["Username"].ToString()).FirstOrDefault();
            var dateNow = DateTime.Now;
            comunicaciones.Fecha = dateNow;
            comunicaciones.Estado = 4;
            comunicaciones.Numero_Twilio = telefono.Numero;
            DATA.Contactos contacto = new DATA.Contactos();
            contacto = cont.BuscarContactos(comunicaciones.Id_Contacto);

            TwilioClient.Init(telefono.Account_Id, telefono.Authtoken);
            var message = MessageResource.Create(
                body: comunicaciones.Mensaje,
                from: new Twilio.Types.PhoneNumber("+" + telefono.Numero),
                to: new Twilio.Types.PhoneNumber("+" + contacto.Numero)
            );

            com.InsertarComunicaciones(Mapper.Map<DATA.Comunicaciones>(comunicaciones));

            return RedirectToAction("Mensajes",new { Id_Contacto = comunicaciones.Id_Contacto });
        }

        [HttpGet]
        public JsonResult ListarMensajes(int Id_Contacto)
        {
            var lista = Rest.ListarComunicaciones().Where(x => x.Id_Contacto == Id_Contacto);
            var listas = Mapper.Map<List<Models.Respuestas>>(lista);
            return Json(listas, JsonRequestBehavior.AllowGet);

        }
    }
}