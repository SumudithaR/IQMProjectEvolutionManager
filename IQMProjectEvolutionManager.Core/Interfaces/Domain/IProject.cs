using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IProject : IDomain, IOnTimeDomain
    {
        /// <summary>
        /// Gets or sets the project id. 
        /// </summary>
        /// <value>
        /// The project id. 
        /// </value>
        long ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project name. 
        /// </summary>
        /// <value>
        /// The project name. 
        /// </value>
        string Name { get; set; }
        bool IsActive { get; set; }
        
        ICollection<ReleaseProject> ReleaseProjects { get; set; }
    }
}
