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
    public interface ISubscriberNotifierLogService : IDomainBaseService<SubscriberNotifierLog, SubscriberNotifierLogDomainWrapper, SubscriberNotifierLogPagedSearchDomainWrapper>
    {
        bool InDatabase(SubscriberNotifierLog userNotifierLog);
        bool InDatabaseByTransactionId(SubscriberNotifierLog userNotifierLog);
        bool IsModified(SubscriberNotifierLog subscriberNotifierLog);
        void Insert(SubscriberNotifierLog userNotifierLog);
        void Delete(SubscriberNotifierLog userNotifierLog);
        SubscriberNotifierLog GetByRegisteredForId(SubscriberNotifier userNotifier, long registeredForId);
        int LogsCount();
    }
}