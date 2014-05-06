using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices;
using IQMProjectEvolutionManager.Core.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Services
{
    public interface IReleaseService : IOnTimeDomainBaseService<Release, ReleaseDomainWrapper, ReleasePagedSearchDomainWrapper>
    {
        bool IsUpdated(Release release);
        void InsertOrUpdate(Release release);
        ICollection<ReleaseDomainWrapper> GetReleases(ICollection<ReleaseProject> releaseProjects, bool onlyActive);
        ICollection<ReleaseDomainWrapper> GetReleases(ICollection<ReleaseProject> releaseProjects, Expression<Func<Release, bool>> filter, bool onlyActive);
    }
}