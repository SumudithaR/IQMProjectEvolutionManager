using IQM.Common.Interfaces;
using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface ISubscriberNotifier : IDomain
    {
        long SubscriberNotifierId { get; set; }
        Subscriber Subscriber { get; set; }
        string AccessId { get; set; }
        SubscriberNotifierType SubscriberNotifierType { get; set; }
        SubscriberNotifierPurpose SubscriberNotifierPurpose { get; set; }
    }
}
