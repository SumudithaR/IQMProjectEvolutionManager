using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IQMProjectEvolutionManager.Controllers
{
    public class ReleaseProjectController : Controller
    {
        /// <summary>
        /// Gets or sets the release project service.
        /// </summary>
        public IReleaseProjectService ReleaseProjectService { get; set; }

        public ReleaseProjectController()
        {
            ReleaseProjectService = DependencyResolver.Current.GetService<IReleaseProjectService>();
        }

        /// <summary>
        /// Multiples the project summary.
        /// </summary>
        /// <param name="releaseIdAndProjectName">Name of the release id and project.</param>
        /// <returns></returns>
        public ActionResult ReleaseSummary(long projectId)
        {
            var releaseProjectsToReturn = new List<ReleaseProjectDomainWrapper>();

            foreach (var releaseProject in ReleaseProjectService.GetReleaseProjects(projectId))
            {
                releaseProjectsToReturn.Add(
                    new ReleaseProjectDomainWrapper()
                    {
                        Data = releaseProject
                    });
            }

            return PartialView(releaseProjectsToReturn);
        }
    }
}