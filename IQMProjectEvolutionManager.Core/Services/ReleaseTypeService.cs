// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReleaseTypeService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   The release type service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using IQM.Common.Interfaces;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.DomainWrappers;
    using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
    using IQMProjectEvolutionManager.Core.Interfaces.Services;
    using IQMProjectEvolutionManager.Core.Services.BaseServices;

    using PagedList;

    /// <summary>
    /// The release type service.
    /// </summary>
    public class ReleaseTypeService : OnTimeDomainBaseService<ReleaseType, ReleaseTypeDomainWrapper, ReleaseTypePagedSearchDomainWrapper>, IReleaseTypeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseTypeService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ReleaseTypeService(IGenericRepository<ReleaseType> repository)
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
        public override ReleaseTypePagedSearchDomainWrapper Search(ReleaseTypePagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<ReleaseType>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        /// <summary>
        /// The is updated.
        /// </summary>
        /// <param name="releaseType">
        /// The release type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsUpdated(ReleaseType releaseType)
        {
            var currentReleaseType = GetAll().SingleOrDefault(rType => rType.OnTimeId == releaseType.OnTimeId);

            if (currentReleaseType != null)
            {
                if (!currentReleaseType.Name.Equals(releaseType.Name))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The insert or update.
        /// </summary>
        /// <param name="releaseType">
        /// The release type.
        /// </param>
        public void InsertOrUpdate(ReleaseType releaseType)
        {
            if (this.InDatabase(releaseType))
            {
                var currentReleaseType = GetByOnTimeId(releaseType.OnTimeId);

                currentReleaseType.Name = releaseType.Name;
                currentReleaseType.Edited = DateTime.Now;

                Repository.Save(currentReleaseType);
            }
            else
            {
                Repository.Save(releaseType);
            }
        }

        public ReleaseType GetByName(string releaseTypeName)
        {
            if (releaseTypeName.Equals(string.Empty)) return null;
            releaseTypeName = releaseTypeName.Trim();
            return GetAll().SingleOrDefault(rType => rType.Name.Equals(releaseTypeName));
        }

        /// <summary>
        /// The get older by days.
        /// </summary>
        /// <param name="days">
        /// The days.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        public ICollection<ReleaseType> GetOlderByDays(int days)
        {
            return
                this.GetAll()
                    .Where(rele => rele.Edited != null && (DateTime)rele.Edited <= DateTime.Now.AddDays(days))
                    .ToList();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="releaseTypes">
        /// The release types.
        /// </param>
        public void Delete(ICollection<ReleaseType> releaseTypes)
        {
            foreach (var releaseType in releaseTypes.Where(releaseType => releaseType != null))
            {
                releaseType.DeleteOn = DateTime.Now;
                this.Repository.Remove(releaseType);
            }
        }
    }
}