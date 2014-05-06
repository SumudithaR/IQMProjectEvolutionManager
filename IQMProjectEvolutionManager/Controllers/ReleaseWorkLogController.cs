using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IQMProjectEvolutionManager.Controllers
{
    public class ReleaseWorkLogController : Controller
    {
        public IReleaseWorkLogService ReleaseWorkLogService { get; set; }

        public ReleaseWorkLogController()
        {
            ReleaseWorkLogService = DependencyResolver.Current.GetService<IReleaseWorkLogService>();
        }

        public ActionResult MultipleReleaseSummaryStaffStatistics(long releaseId, long projectId)
        {
            ViewData["releaseId"] = releaseId;
            ViewData["projectId"] = projectId;
            return PartialView(ReleaseWorkLogService.GetByReleaseId(releaseId));
        }
    }
}