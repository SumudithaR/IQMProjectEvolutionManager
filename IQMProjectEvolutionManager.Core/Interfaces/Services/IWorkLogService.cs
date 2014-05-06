using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices;
using IQMProjectEvolutionManager.Core.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Services
{
    public interface IWorkLogService : IDataDomainBaseService<WorkLog, WorkLogDomainWrapper, WorkLogPagedSearchDomainWrapper>
    {
        /// <summary>
        /// Searches the specified search view model.
        /// </summary>
        /// <param name="pagedSearchViewModel">The search view model.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The Client Search View Model</returns>
        WorkLogPagedSearchDomainWrapper Search(WorkLogPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize);
        bool IsUpdated(WorkLog workLog);
        void InsertOrUpdate(WorkLog workLog);
    }
}