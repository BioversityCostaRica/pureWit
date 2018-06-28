using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WiserSoft.UI.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.Users, DATA.Users>();
                cfg.CreateMap<DATA.Users, Models.Users>();
                cfg.CreateMap<Models.Contactos, DATA.Contactos>();
                cfg.CreateMap<DATA.Contactos, Models.Contactos>();
                cfg.CreateMap<Models.Usuarios, DATA.Usuarios>();
                cfg.CreateMap<DATA.Usuarios, Models.Usuarios>();

            });
        }
    }
}