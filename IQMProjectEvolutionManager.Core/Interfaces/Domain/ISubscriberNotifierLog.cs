using IQM.Common.Interfaces;
using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface ISubscriberNotifierLog : IDomain
    {
        long SubscriberNotifierLogId { get; set; }
        string Subject { get; set; }
        string Message { get; set; }
        string Location { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        bool SentSuccess { get; set; }
        string TransactionId { get; set; }
        long RegisteredForId { get; set; }
        SubscriberNotifier SubscriberNotifier { get; set; }
    }
}