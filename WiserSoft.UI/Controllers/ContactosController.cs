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
    public class ContactosController : Controller
    {

        IContactos cont;
        IUsuarios usa;

        public ContactosController()
        {
            cont = new MContactos();
            usa = new  MUsuarios();
        }

        // GET: Contactos
        public ActionResult Index()
        {
            ViewBag.userId = Session["Username"];

           

            var lista = cont.ListarContactos();
            var contactos = Mapper.Map<List<Models.Contactos>>(lista);
            return View(contactos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Contactos contactos)
        {
            var usuarios = usa.ListarUsuarios();
            var listaUsuarios = new SelectList(usuarios, "Username", "Username");

            ViewData["usuarios"] = listaUsuarios;

            var contactoInsertar = Mapper.Map<DATA.Contactos>(contactos);
            cont.InsertarContactos(contactoInsertar);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int numero)
        {
            var contacto = cont.BuscarContactos(numero);
            var contactoBuscar = Mapper.Map<Models.Contactos>(contacto);
            return View(contactoBuscar);
        }

        [HttpPost]
        public ActionResult Edit(Models.Contactos cliente)
        {
            var contactoEditar = Mapper.Map<DATA.Contactos>(cliente);
            cont.ActualizaContactos(contactoEditar);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int numero)
        {
            cont.EliminarContactos(numero);
            return RedirectToAction("Index");
        }

   

    }
}