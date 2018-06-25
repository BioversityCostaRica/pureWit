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
    public class MUsuarios : IUsuarios
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MUsuarios()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              SqlServerDialect.Provider);
        }


        public void ActualizaUsuarios(Usuarios usuarios)
        {
            _db = _conexion.Open();
            _db.Update(usuarios);
        }

        public Usuarios BuscarUsuarios(string username)
        {
            _db = _conexion.Open();
            return _db.Select<Usuarios>(x => x.Username == username).FirstOrDefault();
        }

        public bool BuscarUsuarios(string username, string us_password)
        {
            _db = _conexion.Open();
            return (_db.Count <Usuarios>(x => x.Username == username & x.Password == us_password) > 0) ? true : false;
        }

        public void EliminarUsuarios(string username)
        {
            _db = _conexion.Open();
            _db.Delete<Usuarios>(x => x.Username == username);
        }

        public void InsertarUsuarios(Usuarios usuarios)
        {
            _db = _conexion.Open();
            _db.Insert(usuarios);
        }

        public List<Usuarios> ListarUsuarios()
        {
            _db = _conexion.Open();
            return _db.Select<Usuarios>();
        }
    }
}
