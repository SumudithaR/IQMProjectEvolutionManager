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
    public interface IProjectService : IOnTimeDomainBaseService<Project, ProjectDomainWrapper, ProjectPagedSearchDomainWrapper>
    {
        bool IsModified(Project project);
        bool Update(Project project);
        ICollection<ProjectDomainWrapper> GetProjects(bool onlyActive);
    }
}