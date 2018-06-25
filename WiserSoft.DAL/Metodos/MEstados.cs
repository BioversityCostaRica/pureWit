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
    public class MEstados : IEstados
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MEstados()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              SqlServerDialect.Provider);
        }
        public List<Estados> ListarEstados()
        {
            _db = _conexion.Open();
            return _db.Select<Estados>();
        }
    }
}
