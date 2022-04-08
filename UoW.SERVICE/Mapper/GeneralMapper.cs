using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UoW.DOMAIN.Models;
using UoW.SERVICE.Dtos;

namespace UoW.SERVICE.Mapper
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<Rol, RoleDto>()
                .ForMember(x => x.Yetkiler, y => y.MapFrom(t => t.rolYetkis.Select(s => s.Yetki)));
            CreateMap<RoleDto, Rol>();

            CreateMap<Yetki, YetkiDto>();
            CreateMap<YetkiDto, Yetki>();

            CreateMap<CargoDto, Kargo>().ReverseMap();
        }
    }
}
