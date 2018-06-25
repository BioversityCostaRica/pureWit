using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;
using WiserSoft.DAL.Interfaces;
using ServiceStack.OrmLite;
using System.Data;

namespace WiserSoft.DAL.Metodos
{
    public class MContactos : IContactos
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MContactos()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              SqlServerDialect.Provider);
        }

        public void ActualizaContactos(Contactos contactos)
        {
            _db = _conexion.Open();
            _db.Update(contactos);
        }

        public Contactos BuscarContactos(int numero)
        {
            _db = _conexion.Open();
            return _db.Select<Contactos>(x => x.Numero == numero).FirstOrDefault();
        }

        public void EliminarContactos(int numero)
        {
            _db = _conexion.Open();
            _db.Delete<Contactos>(x => x.Numero == numero);
        }

        public void InsertarContactos(Contactos contactos)
        {
            _db = _conexion.Open();
            _db.Insert(contactos);
        }

        public List<Contactos> ListarContactos()
        {
            _db = _conexion.Open();
            return _db.Select<Contactos>();
        }
    }
}
