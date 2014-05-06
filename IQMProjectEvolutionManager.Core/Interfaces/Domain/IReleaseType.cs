using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IReleaseType : IDomain, IOnTimeDomain
    {
        /// <summary>
        /// Gets or sets the release type id. 
        /// </summary>
        /// <value>
        /// The release type id. 
        /// </value>
        long ReleaseTypeId { get; set; }

        /// <summary>
        /// Gets or sets the release type name. 
        /// </summary>
        /// <value>
        /// The release type name. 
        /// </value>
        string Name { get; set; }
        
        ICollection<Release> Releases { get; set; }
    }
}