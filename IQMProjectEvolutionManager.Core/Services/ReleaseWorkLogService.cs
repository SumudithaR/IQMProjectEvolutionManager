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
using System.Text;

namespace IQMProjectEvolutionManager.Core.Services
{
    public class ReleaseWorkLogService : DomainBaseService<ReleaseWorkLog, ReleaseWorkLogDomainWrapper, ReleaseWorkLogPagedSearchDomainWrapper>, IReleaseWorkLogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ReleaseWorkLogService(IGenericRepository<ReleaseWorkLog> repository)
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
        public override ReleaseWorkLogPagedSearchDomainWrapper Search(ReleaseWorkLogPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
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
            pagedSearchViewModel.Data = new PagedList<ReleaseWorkLog>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool InDatabase(ReleaseWorkLog releaseWorkLog)
        {
            if (releaseWorkLog != null)
            {
                return GetAll().Any(rWLog => rWLog.Release.OnTimeId == releaseWorkLog.Release.OnTimeId && rWLog.Staff.OnTimeId == releaseWorkLog.Staff.OnTimeId);
            }
            return false;
        }

        public bool IsModified(ReleaseWorkLog releaseWorkLog)
        {
            var currentReleaseWorkLog = GetAll().SingleOrDefault(rWLog => rWLog.Release.OnTimeId == releaseWorkLog.Release.OnTimeId && rWLog.Staff.OnTimeId == releaseWorkLog.Staff.OnTimeId);

            if (currentReleaseWorkLog != null)
            {
                if (currentReleaseWorkLog.HoursRemainingOnRelease != releaseWorkLog.HoursRemainingOnRelease || currentReleaseWorkLog.HoursWorkedOnRelease != releaseWorkLog.HoursWorkedOnRelease
                    || currentReleaseWorkLog.HoursWorkedOnReleaseInLastWeek != releaseWorkLog.HoursWorkedOnReleaseInLastWeek)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Update(ReleaseWorkLog releaseWorkLog)
        {
            if (InDatabase(releaseWorkLog))
            {
                var currenReleaseWorkLog = GetAll().SingleOrDefault(rWLog => rWLog.Release.OnTimeId == releaseWorkLog.Release.OnTimeId && rWLog.Staff.OnTimeId == releaseWorkLog.Staff.OnTimeId);

                currenReleaseWorkLog.HoursRemainingOnRelease = releaseWorkLog.HoursRemainingOnRelease;
                currenReleaseWorkLog.HoursWorkedOnRelease = releaseWorkLog.HoursWorkedOnRelease;
                currenReleaseWorkLog.HoursWorkedOnReleaseInLastWeek = releaseWorkLog.HoursWorkedOnReleaseInLastWeek;

                Repository.Save(currenReleaseWorkLog);
                return true;
            }
            return false;
        }

        public void Update(IList<ReleaseWorkLog> releaseWorkLogs, Release release)
        {
            foreach (var releaseWorkLog in releaseWorkLogs)
            {
                releaseWorkLog.Release = release;
                Update(releaseWorkLog);
            }
        }

        public ICollection<ReleaseWorkLogDomainWrapper> GetByReleaseId(long releaseId)
        {
            var releaseWorkLogsToReturn = new List<ReleaseWorkLogDomainWrapper>();

            foreach (var releaseWorkLog in GetAll().Where(rWLog => rWLog.Release.ReleaseId == releaseId))
            {
                releaseWorkLogsToReturn.Add(
                    new ReleaseWorkLogDomainWrapper()
                    {
                        Data = releaseWorkLog
                    });
            }

            return releaseWorkLogsToReturn;
        }
    }
}