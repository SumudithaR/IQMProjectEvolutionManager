using IQM.Common.Interfaces;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManager.Core.Services.BaseServices;
using PagedList;
using System.Collections.Generic;
using System.Linq;

namespace IQMProjectEvolutionManager.Core.Services
{
    public class ProjectService : OnTimeDomainBaseService<Project, ProjectDomainWrapper, ProjectPagedSearchDomainWrapper>, IProjectService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProjectService(IGenericRepository<Project> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Searches the specified search view model.
        /// </summary>
        /// <param name="pagedSearchViewModel">The search view model.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The Client Search View Model</returns>
        public override ProjectPagedSearchDomainWrapper Search(ProjectPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            /*
             * Implement the criteria here.
             */
            //if (!string.IsNullOrEmpty(pagedSearchViewModel.CompanyName))
            //{
            //    query = query.Where(client => client.CompanyName.Contains(pagedSearchViewModel.CompanyName));
            //}

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<Project>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool IsModified(Project project)
        {
            var currentProject = GetAll().SingleOrDefault(proj => proj.OnTimeId == project.OnTimeId);

            if (currentProject == null) return false;
            return !currentProject.Name.Equals(project.Name) || currentProject.IsActive != project.IsActive;
        }

        public bool Update(Project project)
        {
            if (!InDatabase(project)) return false;
            var currentProject = GetByOnTimeId(project.OnTimeId);

            currentProject.Name = project.Name;
            currentProject.IsActive = project.IsActive;

            Repository.Save(currentProject);

            return true;
        }

        public ICollection<ProjectDomainWrapper> GetProjects(bool onlyActive)
        {
            var projects = (onlyActive) ? GetAll().Where(proj => proj.IsActive) : GetAll();

            return (from project in projects
                let releasesByDueDate = project.ReleaseProjects.OrderBy(rele => rele.Release.DueDate)
                select new ProjectDomainWrapper
                {
                    Data = project, NextActiveRelease = releasesByDueDate.Where(rProj => rProj.Release.IsActive).Select(rProj => rProj.Release).FirstOrDefault(), NextInActiveRelease = releasesByDueDate.Where(rProj => !rProj.Release.IsActive).Select(rProj => rProj.Release).FirstOrDefault()
                }).ToList();
        }
    }
}