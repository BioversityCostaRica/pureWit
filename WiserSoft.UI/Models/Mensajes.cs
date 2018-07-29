﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Mensajes
    {
        public int Id_Mensaje { get; set; }
        public String Username { get; set; }
        public String Cuerpo_Mensaje { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione el tipo de mensaje que desea enviar.")]
        public int Id_Tipo_Mensaje { get; set; }
    }
}