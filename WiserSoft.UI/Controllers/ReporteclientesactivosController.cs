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
    public class dataChart
    {
        public int value;
        public string name;

        public dataChart(int _value, string _name)
        {
            value = _value;
            name = _name;
        }
    }
    public class ReporteclientesactivosController : Controller
    {
        IReporteclientesactivos rep;
        IContactos cont;
        public ReporteclientesactivosController()
        {
            rep = new MReporteclientesactivos();
            cont = new MContactos();
        }

        // GET: Reporteclientesactivos
        public ActionResult Index()
        {
            
                var lista = rep.ListarReporteclientesactivos(Session["Username"].ToString());
                var reporte = Mapper.Map<List<Models.Reporteclientesactivos>>(lista.Where(x => x.Username == Session["Username"].ToString()));
            

                foreach (Models.Reporteclientesactivos a in reporte)
                {
                    Console.WriteLine("Cliente:" + a.Nombre + " Cantidad:" + a.Cantidad);

                }
                
                List<DATA.Reporteclientesactivos> listareportes = rep.ListarReporteclientesactivos(Session["Username"].ToString());
                var reporteslistados = listareportes.Select(x => x.Id_Contacto).Distinct();

                List<dataChart> listaReporte = new List<dataChart>();
                foreach (var item in reporteslistados)
                {
                    listaReporte.Add(new dataChart(rep.ListarReporteclientesactivos(Session["Username"].ToString()).Where(x => x.Id_Contacto == item).Select(x => x.Cantidad).First(),
                        cont.ListarContactos().Where(x => x.Id_Contacto == item).Select(x => x.Nombre).First().ToString()));
                }

                ViewBag.group = cont.ListarContactos().Select(x => x.Nombre).Distinct();
                ViewBag.data = listaReporte.ToList();

            
                return View(reporte);
            
        }
    }
}
