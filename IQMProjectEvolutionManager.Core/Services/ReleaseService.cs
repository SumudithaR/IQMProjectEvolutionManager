// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReleaseService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   The release service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using IQM.Common.Interfaces;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.DomainWrappers;
    using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
    using IQMProjectEvolutionManager.Core.Interfaces.Services;
    using IQMProjectEvolutionManager.Core.Services.BaseServices;

    using PagedList;

    /// <summary>
    /// The release service.
    /// </summary>
    public class ReleaseService : OnTimeDomainBaseService<Release, ReleaseDomainWrapper, ReleasePagedSearchDomainWrapper>, IReleaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseService"/> class. 
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public ReleaseService(IGenericRepository<Release> repository)
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
        public override ReleasePagedSearchDomainWrapper Search(ReleasePagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<Release>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        /// <summary>
        /// The is updated.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsUpdated(Release release)
        {
            var currentRelease = GetAll().SingleOrDefault(rele => rele.OnTimeId == release.OnTimeId);

            if (currentRelease != null)
            {
                if (!currentRelease.Name.Equals(release.Name) || currentRelease.HoursRemaining != release.HoursRemaining || currentRelease.HoursWorked != release.HoursWorked
                    || currentRelease.OriginalEstimateForAllTasks != release.OriginalEstimateForAllTasks || currentRelease.PercentageComplete != release.PercentageComplete
                    || !currentRelease.ReleaseNotes.Equals(release.ReleaseNotes) || currentRelease.ReleaseType.OnTimeId != release.ReleaseType.OnTimeId
                    || currentRelease.ReleaseStatusType.OnTimeId != release.ReleaseStatusType.OnTimeId || !currentRelease.DueDate.ToShortDateString().Equals(release.DueDate.ToShortDateString())
                    || currentRelease.IsActive != release.IsActive || currentRelease.ParentReleaseId != release.ParentReleaseId)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The insert or update.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        public void InsertOrUpdate(Release release)
        {
            if (this.InDatabase(release))
            {
                var currentRelease = GetByOnTimeId(release.OnTimeId);

                currentRelease.Name = release.Name;
                currentRelease.HoursRemaining = release.HoursRemaining;
                currentRelease.HoursWorked = release.HoursWorked;
                currentRelease.OriginalEstimateForAllTasks = release.OriginalEstimateForAllTasks;
                currentRelease.PercentageComplete = release.PercentageComplete;
                currentRelease.ReleaseNotes = release.ReleaseNotes;
                currentRelease.DueDate = release.DueDate;
                currentRelease.IsActive = release.IsActive;
                currentRelease.ParentReleaseId = release.ParentReleaseId;

                Repository.Save(currentRelease);
            }
            else
            {
                Repository.Save(release);
            }
        }

        /// <summary>
        /// The get releases.
        /// </summary>
        /// <param name="releaseProjects">
        /// The release projects.
        /// </param>
        /// <param name="onlyActive">
        /// The only active.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        public ICollection<ReleaseDomainWrapper> GetReleases(ICollection<ReleaseProject> releaseProjects, bool onlyActive)
        {
            return this.GetReleases(releaseProjects, null, onlyActive);
        }

        /// <summary>
        /// The get releases.
        /// </summary>
        /// <param name="releaseProjects">
        /// The release projects.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="onlyActive">
        /// The only active.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        public ICollection<ReleaseDomainWrapper> GetReleases(ICollection<ReleaseProject> releaseProjects, Expression<Func<Release, bool>> filter, bool onlyActive)
        {
            var releasesToReturn = new List<ReleaseDomainWrapper>();

            if (releaseProjects != null)
            {
                var releases = (filter != null) ? releaseProjects.Select(rProj => rProj.Release).AsQueryable().Where(filter) : releaseProjects.Select(rProj => rProj.Release);

                if (onlyActive)
                {
                    releases = releases.Where(rele => rele.IsActive);
                }

                releasesToReturn.AddRange(releases.Select(release => new ReleaseDomainWrapper() { Data = release, AssociatedProjectId = releaseProjects.FirstOrDefault().Project.ProjectId }));
            }

            return releasesToReturn;
        }
    }
}