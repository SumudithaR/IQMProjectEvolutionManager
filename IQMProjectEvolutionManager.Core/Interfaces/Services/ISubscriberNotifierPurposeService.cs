﻿using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Services
{
    public interface ISubscriberNotifierPurposeService : IDomainBaseService<SubscriberNotifierPurpose, SubscriberNotifierPurposeDomainWrapper, SubscriberNotifierPurposePagedSearchDomainWrapper>
    {
        bool InDatabase(SubscriberNotifierPurpose userNotifierPurpose);
        void InsertOrUpdate(SubscriberNotifierPurpose userNotifierPurpose);
        SubscriberNotifierPurpose GetByName(string name);
    }
}
