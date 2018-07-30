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
    public class HomeController : Controller
    {
        IUsuarios usu;
        IContactos cont;
        IDifusiones difu;

        public HomeController()
        {
            usu = new MUsuarios();
            cont = new MContactos();
            difu = new MDifusiones();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.Usuarios usuario)
        {
            var passwordEncripted = Encriptacion.Encriptacion.Encriptar(usuario.Password);
            var loginResultUsers = usu.BuscarUsuarios(usuario.Username, passwordEncripted);
            Console.WriteLine(passwordEncripted);
            if (loginResultUsers)
            { //Si es nulo no existe
                DATA.Usuarios datos = usu.BuscarUsuarios(usuario.Username);
                Session["Username"] = datos.Username;
                Session["Rol"] = datos.Id_rol;

                if (datos.Id_rol == 1)
                {
                    return RedirectToAction("UserDashboard", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Administrador");
                }

            }
            else
            {
                ModelState.AddModelError("errorLogin", "Usuario y/o contraseña incorrectos.");
                return View("Index");
            }

            //return View("Index");
        }

        public ActionResult UserDashboard()
        {
            int contactos = cont.ListarContactos().Where(x => x.Username == Session["Username"].ToString()).Count();
            ViewBag.totalContactos = contactos;

            int sms = difu.ListarDifusines().Where(x => x.Username == Session["Username"].ToString() && x.Id_Tipo_Mensaje == 1).Count();
            ViewBag.totalSMS = sms;

            int llamadas = difu.ListarDifusines().Where(x => x.Username == Session["Username"].ToString() && x.Id_Tipo_Mensaje == 2).Count();
            ViewBag.totalLlamadas = llamadas;

            int correos = difu.ListarDifusines().Where(x => x.Username == Session["Username"].ToString() && x.Id_Tipo_Mensaje == 3).Count();
            ViewBag.totalLCorreos = correos;


            var listaDifusiones = difu.CantidadDifusiones();
            Console.WriteLine(listaDifusiones);
            var dif = Mapper.Map<List<Models.Difusiones>>(listaDifusiones);

        
            foreach (Models.Difusiones a in dif)
             {
                  Console.WriteLine("Estado:"+a.Descripcion +" Cantidad:"+a.Id_Estado);
                
            }

           

            //Grafico Pie Difusiones por estado *prueba Fer / Pri
            /* List<DATA.Difusiones> listadifusiones = difu.ListarDifusines();
             var difusiones = listadifusiones.Select(x => x.Id_Estado).Distinct();

             List<int> listaDifusion = new List<int>();
             foreach (var item in difusiones)
             {
                 listaDifusion.Add(listadifusiones.Count(x => x.Id_Estado == item));
             }

          foreach (var a in listaDifusion)
          {
              Console.WriteLine(a);
          }

          var rep = listaDifusion;
             ViewBag.Tipos = difusiones;
             ViewBag.Contenido_Tipos = listaDifusion.ToList();
             */


            /*if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                ViewBag.UserId = Session["UserID"];


                //Grafico Pie Tipos de Productos 
                List<DATA.Productos> listaProductos = prod.ListarProductos();
                var producto = listaProductos.Select(x => x.Pdt_tipo).Distinct();

                List<int> listaProducto = new List<int>();
                foreach (var item in producto)
                {
                    listaProducto.Add(listaProductos.Count(x => x.Pdt_tipo == item));
                }

                var rep = listaProducto;
                ViewBag.Tipos = producto;
                ViewBag.Contenido_Tipos = listaProducto.ToList();

                // Gráfico de Pie Productos Por agricultor
                List<DATA.Productos_Por_Agricultor> listaProductosPorAgricultor = prod_x_agr.ListarProductos_Por_Agricultor();
                var producto_x_agricultor = listaProductosPorAgricultor.Select(x => x.Ppa_Id_Agricultor).Distinct();

                List<int> listaProductoPorAgricultor = new List<int>();
                foreach (var item in producto_x_agricultor)
                {
                    listaProductoPorAgricultor.Add(listaProductosPorAgricultor.Count(x => x.Ppa_Id_Agricultor == item));
                }

                var rep2 = listaProductoPorAgricultor;
                ViewBag.Agricultores = producto_x_agricultor;
                ViewBag.Contenido_Agricultores = listaProductoPorAgricultor.ToList();

                // Gráfico de Pie Canastas por Usuario
                List<DATA.Canastas> listaCanastas = can.ListarCanastas();
                var canasta = listaCanastas.Select(x => x.Can_usuario).Distinct();

                List<int> listaCanasta = new List<int>();
                foreach (var item in canasta)
                {
                    listaCanasta.Add(listaCanastas.Count(x => x.Can_usuario == item));
                }

                var rep3 = listaCanasta;
                ViewBag.Usuarios = canasta;
                ViewBag.Contenido_Canasta = listaCanasta.ToList();

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }*/

            return View();
        }

        public ActionResult ClientDashboard()
        {
            return View();
            /*if (Session["UserID"] != null && Session["Type"].Equals("cliente"))
            {
                ViewBag.UserId = Session["UserID"];

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }*/
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            //Session.Abandon();
            //Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        
    }
}