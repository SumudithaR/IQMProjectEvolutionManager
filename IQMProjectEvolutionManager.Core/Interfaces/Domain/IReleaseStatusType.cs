using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IReleaseStatusType : IDomain, IOnTimeDomain
    {
        /// <summary>
        /// Gets or sets the release status type id. 
        /// </summary>
        /// <value>
        /// The release status type id. 
        /// </value>
        long ReleaseStatusTypeId { get; set; }

        /// <summary>
        /// Gets or sets the release type id. 
        /// </summary>
        /// <value>
        /// The release type id. 
        /// </value>
        long ReleaseTypeId { get; set; }

        /// <summary>
        /// Gets or sets the release status type name. 
        /// </summary>
        /// <value>
        /// The release status type name. 
        /// </value>
        string Name { get; set; }

        ICollection<Release> Releases { get; set; }
    }
}