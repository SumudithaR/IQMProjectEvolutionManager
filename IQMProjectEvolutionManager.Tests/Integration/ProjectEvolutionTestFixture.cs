using System;
using System.Linq.Expressions;
using IQM.Common.Repositories;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Services;
using NUnit.Framework;

namespace IQMProjectEvolutionManager.Tests.Integration
{
    [TestFixture]
    public class ProjectEvolutionTestFixture
    {
        /// <summary>
        /// Determines whether the evolution summary can be retrieved by combining the Project, ReleaseProject and Release Services.
        /// </summary>
        [TestCase]
        public void CanGetTheEvolutionSummary()
        {
            var projects = new ProjectService(new BaseRepository<Project>()).GetProjects(true);

            foreach (var project in projects)
            {
                var releaseProjects = new ReleaseProjectService(new BaseRepository<ReleaseProject>()).GetReleaseProjects(project.Data.ProjectId);

                // Filter the releases by their release type and release status type. 
                Expression<Func<Release, bool>> filter = rele => rele.ReleaseType.Name.Equals("Milestone") && rele.ReleaseStatusType.Name.Equals("In Progress");
                var releases = new ReleaseService(new BaseRepository<Release>()).GetReleases(releaseProjects, filter, true);

                // This shouldn't be null since sample data is added by the SetupDataFixture class.
                Assert.IsNotNull(releases);
            }
        }
    }
}