using IQMProjectEvolutionManager.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IQMProjectEvolutionManager.Controllers
{
    public class StaffController : Controller
    {
        public IStaffService StaffService { get; set; }

        public StaffController()
        {
            StaffService = DependencyResolver.Current.GetService<IStaffService>();
        }
    }
}
