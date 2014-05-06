using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IReleaseWorkLog : IDomain 
    {
        Release Release { get; set; }

        Staff Staff { get; set; }

        /// <summary>
        /// Gets or sets the hours worked on release.
        /// </summary>
        /// <value>
        /// The hours worked on release.
        /// </value>
        float HoursWorkedOnRelease { get; set; }

        /// <summary>
        /// Gets or sets the hours remaining on release.
        /// </summary>
        /// <value>
        /// The hours remaining on release.
        /// </value>
        float HoursRemainingOnRelease { get; set; }

        /// <summary>
        /// Gets or sets the hours remaining on release in last week.
        /// </summary>
        /// <value>
        /// The hours remaining on release in last week.
        /// </value>
        float HoursWorkedOnReleaseInLastWeek { get; set; }
    }
}