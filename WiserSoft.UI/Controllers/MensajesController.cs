using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;
using AutoMapper;

namespace WiserSoft.UI.Controllers
{
    public class MensajesController : Controller
    {
        IMensajes mensaj;
        ITipo_Difusiones tip;
        IDifusiones dif;

        public MensajesController()
        {
            mensaj = new MMensajes();
            tip = new MTipo_Difusiones();
            dif = new MDifusiones();
        }

        // GET: Mensajes
        //[OutputCache(Duration = 300)]
        public ActionResult Index()
        {
            
            var listaMensajes = mensaj.ListarMensajes();
            var mensajeListar = Mapper.Map<List<Models.Mensajes>>(listaMensajes.Where(x => x.Username == Session["Username"].ToString()));
            //var mensajeListar = Mapper.Map<List<Models.Mensajes>>(listaMensajes);

            if (Session["Username"] != null)
            {
                
                var listaTipo = tip.ListarTipoDifusiones();
                var TipoListar = Mapper.Map<List<Models.Tipo_Difusiones>>(listaTipo);
          
                List<DATA.Tipo_Difusiones> tipoDifusiones = tip.ListarTipoDifusiones();
                var listaDeTipos = Mapper.Map<List<Models.Tipo_Difusiones>>(tipoDifusiones);


                IEnumerable<SelectListItem> selectTipoDifusion =
                from t in listaDeTipos
                select new SelectListItem
                {
                    Text = t.Descripcion,
                    Value = t.Id.ToString()
                };

                foreach (Models.Mensajes mensaje in mensajeListar)
                {
                    mensaje.Tipo_Difusiones = listaDeTipos.Where(x => x.Id == mensaje.Id_Tipo).FirstOrDefault();
                }

                ViewBag.ListasTipoMensaje = selectTipoDifusion;
                
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Rol = Session["Rol"].ToString();
            return View(mensajeListar);
        }
        [HttpPost]
        public ActionResult Index(Models.Mensajes mensajes)
        {
            try
            {
                    var mensajesInsertar = Mapper.Map<DATA.Mensajes>(mensajes);
                    mensaj.InsertarMensajes(mensajesInsertar);
                    return RedirectToAction("Index", "Home");
                
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido insertar");
            }
            ViewBag.Rol = Session["Rol"].ToString();
            return Index();
        }

      

        // GET: Mensajes/Create
        public ActionResult Create()
        {
            var listaTipo = tip.ListarTipoDifusiones();
            var TipoListar = Mapper.Map<List<Models.Tipo_Difusiones>>(listaTipo);

            List<DATA.Tipo_Difusiones> tipoDifusiones = tip.ListarTipoDifusiones();
            var listaDeTipos = Mapper.Map<List<Models.Tipo_Difusiones>>(tipoDifusiones);


            IEnumerable<SelectListItem> selectTipoDifusion =
            from t in listaDeTipos
            select new SelectListItem
            {
                Text = t.Descripcion,
                Value = t.Id.ToString()
            };

            ViewBag.ListasTipoMensaje = selectTipoDifusion;

            ViewBag.Rol = Session["Rol"].ToString();
            return View();
        }

        // POST: Mensajes/Create
        [HttpPost]
        public ActionResult Create(Models.Mensajes mensajes)
        {

            // TODO: Add insert logic here
            try
            {
                mensajes.Username = Session["Username"].ToString();
                var mensajeInsertar = Mapper.Map<DATA.Mensajes>(mensajes);
                mensaj.InsertarMensajes(mensajeInsertar);
                ViewBag.Rol = Session["Rol"].ToString();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }

        }



        // GET: Mensajes/Edit/5
        public ActionResult Edit(int id_Mensaje)
        {
            var mensaje = mensaj.BuscarMensajes(id_Mensaje);
            var mensajeBuscar = Mapper.Map<Models.Mensajes>(mensaje);

            var listaTipo = tip.ListarTipoDifusiones();
            var TipoListar = Mapper.Map<List<Models.Tipo_Difusiones>>(listaTipo);

            List<DATA.Tipo_Difusiones> tipoDifusiones = tip.ListarTipoDifusiones();
            var listaDeTipos = Mapper.Map<List<Models.Tipo_Difusiones>>(tipoDifusiones);


            IEnumerable<SelectListItem> selectTipoDifusion =
            from t in listaDeTipos
            select new SelectListItem
            {
                Text = t.Descripcion,
                Value = t.Id.ToString()
            };

            ViewBag.ListasTipoMensaje = selectTipoDifusion;

            ViewBag.Rol = Session["Rol"].ToString();
            return View(mensajeBuscar);
        }

        // POST: Mensajes/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Mensajes mensajes)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                // TODO: Add update logic here
                mensajes.Username = Session["Username"].ToString();
                var mensajeEditar = Mapper.Map<DATA.Mensajes>(mensajes);
                mensaj.ActualizaMensajes(mensajeEditar);

                ViewBag.Rol = Session["Rol"].ToString();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mensajes/Delete/5
        public ActionResult Delete(int id_Mensaje)
        {
            ViewBag.Rol = Session["Rol"].ToString();
            mensaj.EliminarMnensajes(id_Mensaje);
            return RedirectToAction("Index");
        }

    }
}
