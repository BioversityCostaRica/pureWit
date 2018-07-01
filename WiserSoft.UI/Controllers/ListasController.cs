using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class ListasController : Controller
    {
        IListas list;
        IUsuarios usa;

        public ListasController()
        {
            list = new MListas();
            usa = new MUsuarios();
        }
        // GET: Listas
        public ActionResult Index()
        {
            ViewBag.userId = Session["Username"];

            var lista = list.ListarListas().Where(x => x.Username == Session["Username"].ToString());
            var listas = Mapper.Map<List<Models.Listas>>(lista);
            return View(listas);
        }

        public ActionResult Create()
        {
            ViewBag.userId = Session["Username"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Listas listas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.userId = Session["Username"];
                    listas.Username = ViewBag.userId;


                    var listaInsertar = Mapper.Map<DATA.Listas>(listas);
                    list.InsertarListas(listaInsertar);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido insertar");
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id_lista)
        {
            var lista = list.BuscarListas(id_lista);
            var listaBuscar = Mapper.Map<Models.Listas>(lista);
            return View(listaBuscar);
        }

        [HttpPost]
        public ActionResult Edit(Models.Listas lista)
        {
            try
            {
                ViewBag.userId = Session["Username"];
                lista.Username = ViewBag.userId;

                var listaEditar = Mapper.Map<DATA.Listas>(lista);
                list.ActualizarLista(listaEditar);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido actualizar");
                return RedirectToAction("Index");
            }
           
        }

        public ActionResult Delete(int id_lista)
        {
            try
            {
                list.EliminarLista(id_lista);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "No se ha podido eliminar");
                return RedirectToAction("Index");
            }
            
        }
    }
}