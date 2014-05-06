using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace IQMProjectEvolutionManager.Controllers
{
    public class ReleaseController : Controller
    {
        /// <summary>
        /// Gets or sets the release service.
        /// </summary>
        public IReleaseService ReleaseService { get; set; }

        public ReleaseController()
        {
            ReleaseService = DependencyResolver.Current.GetService<IReleaseService>();
        }

        /// <summary>
        /// Multiples the project summary.
        /// </summary>
        /// <param name="releaseIdAndProjectName">Name of the release id and project.</param>
        /// <returns></returns>
        public ActionResult MultipleReleaseSummary(ICollection<ReleaseProjectDomainWrapper> releaseProjects, bool onlyActive)
        {
            var dataItems = releaseProjects.Select(rProj => rProj.Data).ToList();

            Expression<Func<Release, bool>> filter = rele => rele.ReleaseType.Name.Equals(ConfigurationManager.AppSettings["ReleaseType"])
                && rele.ReleaseStatusType.Name.Equals(ConfigurationManager.AppSettings["ReleaseStatusType"]);

            return PartialView(ReleaseService.GetReleases(dataItems, filter, onlyActive));
        }
    }
}
