using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Services
{
    public interface IReleaseWorkLogService : IDomainBaseService<ReleaseWorkLog, ReleaseWorkLogDomainWrapper, ReleaseWorkLogPagedSearchDomainWrapper>
    {
        bool InDatabase(ReleaseWorkLog releaseWorkLog);
        bool IsModified(ReleaseWorkLog releaseWorkLog);
        bool Update(ReleaseWorkLog releaseWorkLog);
        void Update(IList<ReleaseWorkLog> releaseWorkLogs, Release release);
        ICollection<ReleaseWorkLogDomainWrapper> GetByReleaseId(long releaseId);
    }
}