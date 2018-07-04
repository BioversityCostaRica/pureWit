using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class DifusionController : Controller
    {
        IListas list;
        ITipo_Difusiones tipDif;
        IMensajes mens;
        IDifusiones dif;
        ITelefonos tel;
        IContactos_Por_Lista conList;
        IContactos con;
        IHistoriales his;
        public DifusionController()
        {
            list    = new MListas();
            tipDif  = new MTipo_Difusiones();
            mens    = new MMensajes();
            dif     = new MDifusiones();
            tel     = new MTelefonos();
            conList = new MContactos_Por_Lista();
            con     = new MContactos();
            his     = new MHistoriales();
        }
        public ActionResult Index()
        {
            /*********************************CREAR*ENVIOS***********************************************/
            List<DATA.Listas> listas = list.ListarListas();
            List<DATA.Tipo_Difusiones> tipoDifusiones = tipDif.ListarTipoDifusiones();
            List<DATA.Mensajes> mensajes = mens.ListarMensajes();
            //var listasDelUsuario = Mapper.Map<List<Models.Listas>>(listas.Where(x => x.Username == Session["Username"].ToString()));
            var listasDelUsuario = Mapper.Map<List<Models.Listas>>(listas.Where(x => x.Username == "b.madriz"));
            var listaDeTipos = Mapper.Map<List<Models.Tipo_Difusiones>>(tipoDifusiones);
            //var listaMensajes = Mapper.Map<List<Models.Mensajes>>(mensajes.Where(x => x.Username == Session["Username"].ToString()));
            var listaMensajes = Mapper.Map<List<Models.Mensajes>>(mensajes.Where(x => x.Username == "b.madriz"));

            IEnumerable<SelectListItem> selectListas =
            from l in listasDelUsuario
            select new SelectListItem
            {
                Text = l.Nombre,
                Value = l.Id_Lista.ToString()
            };

            ViewBag.Listas = selectListas;

            IEnumerable<SelectListItem> selectTipoDifusion =
            from t in listaDeTipos
            select new SelectListItem
            {
                Text = t.Descripcion,
                Value = t.Id.ToString()
            };

            ViewBag.ListasTipoDifusion = selectTipoDifusion;

            IEnumerable<SelectListItem> selectMensajes =
            from m in listaMensajes
            select new SelectListItem
            {
                Text = m.Cuerpo_Mensaje,
                Value = m.Id_Mensaje.ToString()
            };

            ViewBag.ListaMensajes = selectMensajes;

            /*********************************MOSTRAR*ENVIOS***********************************************/
            var listaDeDifusiones = dif.ListarDifusines().Where(x => x.Username == "b.madriz");
            var difusiones = Mapper.Map<List<Models.Difusiones>>(listaDeDifusiones);
            ViewBag.ListaDeDifusiones = difusiones;

            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.Difusiones difusion)
        {
            bool permitirEnvio = false;
            DateTime dateSend;
            var dateNow = DateTime.Now;
            difusion.Fecha = dateNow;
            TimeSpan ts;
            ts = dateNow - dateNow;

            string tipoEnvio = Request.Form["tipoEnvio"];
            //difusion.Username = Session["Username"].ToString();
            difusion.Username = "b.madriz";

            if (tipoEnvio == "inmediato")
            {
                ts = dateNow - dateNow;
                difusion.Fecha_Activacion = dateNow;
                permitirEnvio = true;
            }
            else
            {
                try
                {
                    string fechaPro = Request.Form["fechaPro"];
                    string[] datos = fechaPro.Split('-');
                    string horaPro = Request.Form["horaPro"];
                    string[] datos2 = horaPro.Split(':');
                    dateSend = new DateTime(Int32.Parse(datos[0]), Int32.Parse(datos[1]), Int32.Parse(datos[2]), Int32.Parse(datos2[0]), Int32.Parse(datos2[1]), 00);

                    if (dateSend > dateNow)
                    {
                        ts = dateSend - dateNow;
                        permitirEnvio = true;
                    }else
                    {
                        Debug.WriteLine("No puede programar un envio para una fecha ya pasada.");
                    }

                    difusion.Fecha_Activacion = dateSend;
                }catch(Exception e)
                {
                    Debug.WriteLine("Algo quedo mal con la fecha y ahora xq se cae.");
                }
            }

            if(permitirEnvio)
            {
                var difusionInsertar = Mapper.Map<DATA.Difusiones>(difusion);
                dif.InsertarDifusiones(difusionInsertar);

                int maxId = dif.ListarDifusines().Where(x => x.Username == "b.madriz").Max(x => x.Id_Difusion);
                DATA.Telefonos telefonoDelUsuario = tel.ListarTelefonos().Where(x => x.Username == "b.madriz").First();
                DATA.Mensajes mensaje = mens.ListarMensajes().Where(x => x.Id_Mensaje == difusion.Id_Mensaje).First();
                DATA.Difusiones difusion2 = dif.BuscarDifusiones(maxId);

                Task.Delay(ts).ContinueWith((x) => enviarMensajes(telefonoDelUsuario.Numero, telefonoDelUsuario.Account_Id, telefonoDelUsuario.Authtoken, mensaje.Cuerpo_Mensaje, difusion.Id_Lista, difusion2));
            }
            

            return Index();
        }

            
        public void enviarMensajes(string UserPhone, string accountSid, string authToken, string mensaje, int idLista, DATA.Difusiones difusion)
        {
            difusion.Id_Estado = 2;
            dif.ActualizarDifusiones(difusion);

            TwilioClient.Init(accountSid, authToken);
            var contactos = conList.Listar().Where(x => x.Id_Lista == idLista);
            foreach (DATA.Contactos_Por_Listas infoContacto in contactos)
            {
                DATA.Contactos contacto = con.BuscarContactos(infoContacto.Id_contacto);
                Debug.WriteLine(contacto.Numero);
                var message = MessageResource.Create(
                    body: mensaje,
                    from: new Twilio.Types.PhoneNumber("+" + UserPhone),
                    statusCallback: new Uri("http://03eda526.ngrok.io/MessageStatus"),
                    to: new Twilio.Types.PhoneNumber("+" + contacto.Numero)
                );

                DATA.Historiales historial = new DATA.Historiales();
                historial.Id_Difusion = difusion.Id_Difusion;
                historial.Id_Contacto = contacto.Id_Contacto;
                historial.Id_Message  = message.Sid;
                historial.Estado      = 6;
                his.InsertarHistoriales(historial);
            }

            difusion.Id_Estado = 3;
            dif.ActualizarDifusiones(difusion);
        }
    }
}