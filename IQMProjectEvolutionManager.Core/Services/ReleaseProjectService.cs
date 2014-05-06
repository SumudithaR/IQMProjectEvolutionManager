using IQM.Common.Interfaces;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManager.Core.Services.BaseServices;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Services
{
    public class ReleaseProjectService : DomainBaseService<ReleaseProject, ReleaseProjectDomainWrapper, ReleaseProjectPagedSearchDomainWrapper>, IReleaseProjectService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ReleaseProjectService(IGenericRepository<ReleaseProject> repository)
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
        public override ReleaseProjectPagedSearchDomainWrapper Search(ReleaseProjectPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
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
            pagedSearchViewModel.Data = new PagedList<ReleaseProject>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public ReleaseProject GetByProjectAndReleaseIds(long releaseId, long projectId)
        {
            if (releaseId <= 0 || projectId <= 0)
            {
                return null;
            }
            Expression<Func<ReleaseProject, bool>> clause = rProj => rProj.Project.ProjectId == projectId && rProj.Release.ReleaseId == releaseId;
            return this.GetAll(clause).SingleOrDefault();
        }

        public bool InDatabaseByOnTimeId(ReleaseProject releaseProject)
        {
            if (releaseProject != null)
            {
                return GetAll().Any(rProj => rProj.Release.OnTimeId == releaseProject.Release.OnTimeId && rProj.Project.OnTimeId == rProj.Project.OnTimeId);
            }
            return false;
        }

        public bool InDatabase(ReleaseProject releaseProject)
        {
            if (releaseProject != null)
            {
                return GetAll().Any(rProj => rProj.Release.ReleaseId == releaseProject.Release.ReleaseId && rProj.Project.ProjectId == rProj.Project.ProjectId);
            }
            return false;
        }

        public bool Insert(ReleaseProject releaseProject)
        {
            if (!InDatabase(releaseProject))
            {
                Repository.Save(releaseProject);
                return true;
            }

            return false;
        }

        public ICollection<ReleaseProject> GetReleaseProjects(long projectId)
        {
            return GetAll().Where(rProj => rProj.Project.ProjectId == projectId).ToList();
        }
    }
}