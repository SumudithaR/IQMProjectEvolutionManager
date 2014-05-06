using IQM.Common.Web.Utility;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManager.Core.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace IQMProjectEvolutionManager.Controllers
{
    [TocAuthorize]
    public class ProjectController : Controller
    {
        /// <summary>
        /// Gets or sets the project service.
        /// </summary>
        public IProjectService ProjectService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        public ProjectController()
        {
            ProjectService = DependencyResolver.Current.GetService<IProjectService>();

        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View(ProjectService.GetProjects(true));
        }
    }
}