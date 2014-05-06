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
    public interface ISubscriberNotifierService : IDomainBaseService<SubscriberNotifier, SubscriberNotifierDomainWrapper, SubscriberNotifierPagedSearchDomainWrapper>
    {
        bool InDatabase(SubscriberNotifier userNotifier);
        void InsertOrUpdate(SubscriberNotifier userNotifier);
        SubscriberNotifier GetNotifier(Subscriber user, SubscriberNotifierPurpose purpose, SubscriberNotifierType type);
        void Delete(SubscriberNotifier userNotifier);
    }
}