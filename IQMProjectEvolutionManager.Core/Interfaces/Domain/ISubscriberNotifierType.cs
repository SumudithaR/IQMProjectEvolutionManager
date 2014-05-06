using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface ISubscriberNotifierType : IDomain
    {
        long SubscriberNotifierTypeId { get; set; }
        string Name { get; set; }
        ICollection<SubscriberNotifier> SubscriberNotifiers { get; set; }
    }
}