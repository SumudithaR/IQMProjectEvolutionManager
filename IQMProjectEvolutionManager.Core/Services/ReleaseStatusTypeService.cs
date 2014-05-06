using IQM.Common.Interfaces;
using IQM.Common.Services;
using IQM.Common.Web.Interfaces;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManager.Core.Services.BaseServices;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Services
{
    public class ReleaseStatusTypeService : OnTimeDomainBaseService<ReleaseStatusType, ReleaseStatusTypeDomainWrapper, ReleaseStatusTypePagedSearchDomainWrapper>, IReleaseStatusTypeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseTypeService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ReleaseStatusTypeService(IGenericRepository<ReleaseStatusType> repository)
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
        public override ReleaseStatusTypePagedSearchDomainWrapper Search(ReleaseStatusTypePagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
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
            pagedSearchViewModel.Data = new PagedList<ReleaseStatusType>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool IsUpdated(ReleaseStatusType releaseStatusType)
        {
            var currentReleaseStatusType = GetAll().SingleOrDefault(rSType => rSType.OnTimeId == releaseStatusType.OnTimeId);

            if (currentReleaseStatusType != null)
            {
                if (!currentReleaseStatusType.Name.Equals(releaseStatusType.Name) || currentReleaseStatusType.ReleaseTypeId != releaseStatusType.ReleaseTypeId)
                {
                    return true;
                }
            }

            return false;
        }

        public void InsertOrUpdate(ReleaseStatusType releaseStatusType)
        {
            if (InDatabase(releaseStatusType))
            {
                var currentReleaseStatusType = GetByOnTimeId(releaseStatusType.OnTimeId);

                currentReleaseStatusType.Name = releaseStatusType.Name;
                currentReleaseStatusType.ReleaseTypeId = releaseStatusType.ReleaseTypeId;

                Repository.Save(currentReleaseStatusType);
            }
            else
            {
                Repository.Save(releaseStatusType);
            }
        }
    }
}