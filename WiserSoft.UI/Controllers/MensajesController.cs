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

        public MensajesController()
        {
            mensaj = new MMensajes();
        }

        // GET: Mensajes
        //[OutputCache(Duration = 300)]
        public ActionResult Index()
        {
            var listaMensajes = mensaj.ListarMensajes();
            var mensajeListar = Mapper.Map<List<Models.Mensajes>>(listaMensajes);
            return View(mensajeListar);
        }

        [HttpPost]
        public ActionResult Index(Models.Mensajes mensajes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mensajesInsertar = Mapper.Map<DATA.Mensajes>(mensajes);
                    mensaj.InsertarMensajes(mensajesInsertar);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido insertar");
            }

            return View();
        }

        // GET: Mensajes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mensajes/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Mensajes/Create
        [HttpPost]
        public ActionResult Create(Models.Mensajes mensajes)
        {

            // TODO: Add insert logic here
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var mensajeInsertar = Mapper.Map<DATA.Mensajes>(mensajes);
                mensaj.InsertarMensajes(mensajeInsertar);
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
                var mensajeEditar = Mapper.Map<DATA.Mensajes>(mensajes);
                mensaj.ActualizaMensajes(mensajeEditar);
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
            mensaj.EliminarMnensajes(id_Mensaje);
            return RedirectToAction("Index");
        }

        // POST: Mensajes/Delete/5
        /*[HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}
