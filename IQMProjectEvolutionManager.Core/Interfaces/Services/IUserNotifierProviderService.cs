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
    public interface IUserNotifierProviderService : IDomainBaseService<UserNotifierProvider, UserNotifierProviderDomainWrapper, UserNotifierProviderPagedSearchDomainWrapper>
    {
        bool InDatabase(UserNotifierProvider userNotifierProvider);
        void InsertOrUpdate(UserNotifierProvider userNotifierProvider);
    }
}