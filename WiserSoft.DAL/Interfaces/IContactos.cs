using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;


namespace WiserSoft.DAL.Interfaces
{
    interface IContactos
    {
        List<Contactos> ListarContactos();
        Contactos BuscarContactos(int numero);
        void InsertarContactos(Contactos contactos);
        void ActualizaContactos(Contactos contactos);
        void EliminarContactos(int numero);
    }
}
