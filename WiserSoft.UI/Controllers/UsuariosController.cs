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
    public class UsuariosController : Controller
    {
        IUsuarios usuar;

        public UsuariosController()
        {
            usuar = new MUsuarios();
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.Usuarios usuarios)
        {
                try
                {
                    if (ModelState.IsValid)
                    {
                        //Encripta el password
                        var passwordEncriptado = Encriptacion.Encriptacion.Encriptar(usuarios.Password);
                        //Asigna la variable encriptada al objeto Password
                        usuarios.Password = passwordEncriptado;

                        var fechaRegistro = DateTime.Today.ToShortDateString();
                        usuarios.Fecha_registro = Convert.ToDateTime(fechaRegistro);

                        var rol = 1;
                        usuarios.Id_rol = Convert.ToInt32(rol);
               
                        var usuariosInsertar = Mapper.Map<DATA.Usuarios>(usuarios);
                        usuar.InsertarUsuarios(usuariosInsertar);
                        return RedirectToAction("Index", "Home");
                }

                }
                catch (Exception)
                {
                    ModelState.AddModelError("error", "No se ha podido insertar");
                }
            return View();
        }
           
            
        
    }
}