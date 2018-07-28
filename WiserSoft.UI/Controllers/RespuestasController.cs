using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class RespuestasController : Controller
    {
        IRespuestas Rest;
        IContactos cont;
        // GET: Respuestas
        public RespuestasController()
        {
            Rest = new MRespuestas();
            cont = new MContactos();
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

        [HttpGet]
        public JsonResult ListarMensajes(int Id_Contacto)
        {
            var lista = Rest.ListarComunicaciones().Where(x => x.Id_Contacto == Id_Contacto);
            var listas = Mapper.Map<List<Models.Respuestas>>(lista);
            return Json(listas, JsonRequestBehavior.AllowGet);

        }
    }
}