using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Mensajes
    {
        public int Id_Mensaje { get; set; }
        public String Username { get; set; }
        public String Cuerpo_Mensaje { get; set; }
        public int Id_Tipo { get; set; }
    }
}