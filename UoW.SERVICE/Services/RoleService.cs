using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UoW.CORE.Interface;
using UoW.DATA;
using UoW.DOMAIN.Models;
using UoW.SERVICE.Contracts;
using UoW.SERVICE.Dtos;
using UoW.SERVICE.Generics;

namespace UoW.SERVICE.Services
{
    public class RoleService : CrudService<Rol, RoleDto>, IRoleService
    {
        public RoleService(IRepository<Rol> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override List<RoleDto> GetAll()
        {
            var entities = _repository.Include("rolYetkis.Yetki").ToList();
            var entityDtos = _mapper.Map<IList<Rol>, List<RoleDto>>(entities);
            return entityDtos;
        }
    }
}
