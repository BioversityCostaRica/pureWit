using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;

namespace WiserSoft.UI.Models
{
    public class Listas
    {
        public int Id_Lista { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Descripcion { get; set; }
        [Required]
        public String Username { get; set; }
    }
}