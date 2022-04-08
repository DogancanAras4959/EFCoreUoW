using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;
using UoW.SERVICE.Dtos;
using UoW.SERVICE.Generics;

namespace UoW.SERVICE.Contracts
{
    public interface IRoleService : ICrudService<Rol,RoleDto>
    {
    }
}
