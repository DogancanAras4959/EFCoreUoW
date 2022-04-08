using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.SERVICE.Contracts;

namespace UoW.ADMIN.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleService _roleServices;

        public RolesController(IRoleService roleServices)
        {
            _roleServices = roleServices;
        }
       
        public JsonResult RoleList()
        {           
            return Json(_roleServices.GetAll());
        }
    }
}
