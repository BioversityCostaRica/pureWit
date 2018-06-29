﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Contactos
    {
        [Required]
        public int Numero { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Detalle { get; set; }
        [Required]
        [EmailAddress]
        public String Correo { get; set; }
        public String Username { get; set; }
    }
}
