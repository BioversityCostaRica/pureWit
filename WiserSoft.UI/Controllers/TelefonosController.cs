﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;
using AutoMapper;

namespace WiserSoft.UI.Controllers
{
    public class TelefonosController : Controller
    {
        ITelefonos telef;
        IUsuarios usua;

        public TelefonosController()
        {
            telef = new MTelefonos();
            usua = new MUsuarios();
        }
        // GET: Telefonos
        public ActionResult Index()
        {
            var listaTelefonos = telef.ListarTelefonos();
            var telefonosListar = Mapper.Map<List<Models.Telefonos>>(listaTelefonos);
            return View(telefonosListar);
        }

        [HttpPost]
        public ActionResult Index(Models.Telefonos telefonos)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var telefonosInsertar = Mapper.Map<DATA.Telefonos>(telefonos);
                    telef.InsertarTelefonos(telefonosInsertar);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido insertar");
            }
            return View();
        }

        // GET: Telefonos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Telefonos/Create
        public ActionResult Create()
        {
            var listaUsua = usua.ListarUsuarios();

            var listaTelf = telef.ListarTelefonos();
            var TelefonosListar = Mapper.Map<List<Models.Usuarios>>(listaUsua.Where(x => x.Username != listaTelf.Select(t => t.Username).ToString()));
           

            IEnumerable<SelectListItem> selectUsuario =
            from t in TelefonosListar
            select new SelectListItem
            {
                Text = t.Username,
                Value = t.Username.ToString()
            };

            ViewBag.ListasUsername = selectUsuario;

            return View();
        }

        // POST: Telefonos/Create
        [HttpPost]
        public ActionResult Create(Models.Telefonos telefonos)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View();
                }
                telefonos.Username = Session["Username"].ToString();
                var telefonosInsertar = Mapper.Map<DATA.Telefonos>(telefonos);
                telef.InsertarTelefonos(telefonosInsertar);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Telefonos/Edit/5
        public ActionResult Edit(string numero)
        {
            var telefono = telef.BuscarTelefonos(numero);

            var telefonoBuscar = Mapper.Map<Models.Telefonos>(telefono);
            return View(telefonoBuscar);
        }

        // POST: Telefonos/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Telefonos telefonos)
        {
            try
            {
                // TODO: Add update logic here
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var telefonoEditar = Mapper.Map<DATA.Telefonos>(telefonos);
                //telefonos.Username = Session["Username"].ToString();
                telef.ActualizaTelefonos(telefonoEditar);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Telefonos/Delete/5
        public ActionResult Delete(string numero)
        {
            telef.EliminarTelefonos(numero);
            return RedirectToAction("Index");
        }

        // POST: Telefonos/Delete/5
        /* [HttpPost]
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
