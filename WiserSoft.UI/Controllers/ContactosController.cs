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
        ILista_Negra list_negra;

        public ContactosController()
        {
            cont = new MContactos();
            usa = new  MUsuarios();
            list_negra = new MLista_Negra();
        }

        // GET: Contactos
        public ActionResult Index()
        {
            ViewBag.userId = Session["Username"];
            
            var lista = cont.ListarContactos().Where(x => x.Username == Session["Username"].ToString());
            var contactos = Mapper.Map<List<Models.Contactos>>(lista);

            List<DATA.Lista_Negra> listaNegra = list_negra.ListarListaNegra();
            var lista_Negra = Mapper.Map<List<Models.Lista_Negra>>(listaNegra);

            foreach (Models.Contactos a in contactos)
            {
                var varas = lista_Negra.Where(x => x.Id_Contacto == a.Id_Contacto).FirstOrDefault();
                if (varas != null)
                {
                    a.lista_negra = "Si";
                }
                else
                {
                    a.lista_negra = "No";
                }
                
            }
            
           

            return View(contactos);
        }
        
        public ActionResult Create()
        {
            ViewBag.userId = Session["Username"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Contactos contactos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.userId = Session["Username"];
                    contactos.Username = ViewBag.userId;

                    var contactoInsertar = Mapper.Map<DATA.Contactos>(contactos);
                    cont.InsertarContactos(contactoInsertar);
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

        
        public ActionResult Edit(int Id_Contacto)
        {
           
            var contacto = cont.BuscarContactos(Id_Contacto);
            var contactoBuscar = Mapper.Map<Models.Contactos>(contacto);
            return View(contactoBuscar);
        }

        [HttpPost]
        public ActionResult Edit(Models.Contactos cliente)
        {
            ViewBag.userId = Session["Username"];
            cliente.Username = ViewBag.userId;
            var contactoEditar = Mapper.Map<DATA.Contactos>(cliente);
            cont.ActualizaContactos(contactoEditar);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id_contacto)
        {
            cont.EliminarContactos(id_contacto);
            return RedirectToAction("Index");
        }


        public ActionResult EditListaNegra(int Id_Contacto)
        {
            try
            {
                var contacto = cont.BuscarContactos(Id_Contacto);
                var contactoBuscar = Mapper.Map<Models.Contactos>(contacto);
                int idContacto = contactoBuscar.Id_Contacto;

                var listaNegra = list_negra.BuscarListaNegra(Id_Contacto);
                var listaNegraBuscar = Mapper.Map<Models.Lista_Negra>(listaNegra);

                if (listaNegraBuscar == null)
                {
                    var contactos = new Models.Lista_Negra();
                    var contactoListaNegra = Mapper.Map<DATA.Lista_Negra>(contactos);

                    contactoListaNegra.Id_Contacto = idContacto;
                    list_negra.InsertarListaNegra(contactoListaNegra);
                }
                else
                {
                    int idContactoListaNegra = listaNegraBuscar.Id_Contacto;
                    if (idContacto == idContactoListaNegra)
                    {
                        list_negra.EliminaListaNegra(idContacto);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("error", "Ha ocurrido un error");
                return RedirectToAction("Index");
            }
            
           
            return RedirectToAction("Index");


        }

      


    }
}
