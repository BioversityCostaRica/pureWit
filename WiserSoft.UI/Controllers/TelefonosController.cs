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
            ViewBag.Rol = Session["Rol"].ToString();
            try
            {
                var listaTelefonos = telef.ListarTelefonos();
                var telefonosListar = Mapper.Map<List<Models.Telefonos>>(listaTelefonos);
                return View(telefonosListar);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            
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
                    ViewBag.Rol = Session["Rol"].ToString();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido insertar");
            }
            return View();
        }

      

        // GET: Telefonos/Create
        public ActionResult Create()
        {

            ViewBag.Rol = Session["Rol"].ToString();
            try
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
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            
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

                ViewBag.Rol = Session["Rol"].ToString();
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
            ViewBag.Rol = Session["Rol"].ToString();
            try
            {
                var telefono = telef.BuscarTelefonos(numero);
                var telefonoBuscar = Mapper.Map<Models.Telefonos>(telefono);
                
                return View(telefonoBuscar);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
           
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
                ViewBag.Rol = Session["Rol"].ToString();
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
            ViewBag.Rol = Session["Rol"].ToString();

            try
            {
                telef.EliminarTelefonos(numero);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return RedirectToAction("Index");
            }

        }
        
    }
}
