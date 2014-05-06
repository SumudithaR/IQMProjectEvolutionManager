using IQM.Common.Web.ViewModels;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.ViewModels.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.DomainWrappers
{
    public class SubscriberNotifierTypeDomainWrapper : BaseViewModel<SubscriberNotifierType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriberNotifierPurposeDomainWrapper"/> class.
        /// </summary>
        public SubscriberNotifierTypeDomainWrapper()
            : base(new ApplicationSettings())
        {
        }

        /// <summary>
        /// Gets the name of the domain object.
        /// </summary>
        /// <returns>The friendly name of the data object.</returns>
        public override string GetDomainObjectName()
        {
            return "Subscriber Notifier Type";
        }

        /// <summary>
        /// Gets the data id.
        /// </summary>
        /// <returns>the data objects id.</returns>
        public override long? GetDataId()
        {
            return Data.SubscriberNotifierTypeId;
        }
    }
}
