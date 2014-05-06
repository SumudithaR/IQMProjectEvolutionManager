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
    public class WorkLogService : DataDomainBaseService<WorkLog, WorkLogDomainWrapper, WorkLogPagedSearchDomainWrapper>, IWorkLogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public WorkLogService(IGenericRepository<WorkLog> repository)
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
        public override WorkLogPagedSearchDomainWrapper Search(WorkLogPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
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
            pagedSearchViewModel.Data = new PagedList<WorkLog>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool IsUpdated(WorkLog workLog)
        {
            var currentWorkLog = GetAll().SingleOrDefault(wLog => wLog.OnTimeId == workLog.OnTimeId);

            if (currentWorkLog != null)
            {
                if (!(currentWorkLog.HoursWorked == workLog.HoursWorked || currentWorkLog.StaffMember.OnTimeId == workLog.StaffMember.OnTimeId
                    || currentWorkLog.Task.OnTimeId == workLog.Task.OnTimeId))
                {
                    return true;
                }
            }

            return false;
        }

        public void InsertOrUpdate(WorkLog workLog)
        {
            if (InDatabase(workLog))
            {
                if (IsUpdated(workLog))
                {
                    Repository.Remove(GetByOnTimeId(workLog.OnTimeId));
                    Repository.Save(workLog);
                }
            }
            else
            {
                Repository.Save(workLog);
            }
        }
    }
}