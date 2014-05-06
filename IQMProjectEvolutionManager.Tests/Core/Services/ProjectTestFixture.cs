using IQM.Common.Repositories;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Services;
using NUnit.Framework;

namespace IQMProjectEvolutionManager.Tests.Core.Services
{
    [TestFixture]
    public class ProjectTestFixture
    {
        /// <summary>
        /// Determines whether all active projects can be retrieved. The projects must only be active.
        /// </summary>
        [TestCase]
        public void CanGetAllActiveProjects()
        {
            var activeProjectsFromService = new ProjectService(new BaseRepository<Project>()).GetProjects(true);
            foreach (var project in activeProjectsFromService)
            {
                // If for some reason the project is inactive then there is an issue in the GetProjects method of the service
                Assert.IsTrue(project.Data.IsActive);
            }
        }
    }
}