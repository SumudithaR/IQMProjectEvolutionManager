using IQM.Common.Web.ViewModels;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.ViewModels.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.DomainWrappers
{
    /// <summary>
    /// Work Log View Model
    /// </summary>
    public class WorkLogDomainWrapper :BaseViewModel<WorkLog>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkLogDomainWrapper"/> class.
        /// </summary>
        public WorkLogDomainWrapper() : base(new ApplicationSettings())
        {

        }

        /// <summary>
        /// Gets the name of the domain object.
        /// </summary>
        /// <returns>The friendly name of the data object.</returns>
        public override string GetDomainObjectName()
        {
            return "Work Log";
        }

        /// <summary>
        /// Gets the data id.
        /// </summary>
        /// <returns>the data objects id.</returns>
        public override long? GetDataId()
        {
            return Data.WorkLogId;
        }
    }
}
