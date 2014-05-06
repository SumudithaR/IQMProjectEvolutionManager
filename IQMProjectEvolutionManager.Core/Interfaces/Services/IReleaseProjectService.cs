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
    public interface IReleaseProjectService : IDomainBaseService<ReleaseProject, ReleaseProjectDomainWrapper, ReleaseProjectPagedSearchDomainWrapper>
    {
        ReleaseProject GetByProjectAndReleaseIds(long releaseId, long projectId);
        bool InDatabaseByOnTimeId(ReleaseProject releaseProject);
        bool InDatabase(ReleaseProject releaseProject);
        bool Insert(ReleaseProject releaseProject);
        ICollection<ReleaseProject> GetReleaseProjects(long projectId);
    }
}