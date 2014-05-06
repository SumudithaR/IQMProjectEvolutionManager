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
    public interface IReleaseStatusTypeService : IOnTimeDomainBaseService<ReleaseStatusType, ReleaseStatusTypeDomainWrapper, ReleaseStatusTypePagedSearchDomainWrapper>
    {
        bool IsUpdated(ReleaseStatusType releaseStatusType);
        void InsertOrUpdate(ReleaseStatusType releaseStatusType);
    }
}