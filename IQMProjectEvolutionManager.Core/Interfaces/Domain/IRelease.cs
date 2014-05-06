using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IRelease : IDomain, IOnTimeDomain
    {
        long ReleaseId { get; set; }
        string Name { get; set; }
        DateTime DueDate { get; set; }
        float HoursWorked { get; set; }
        float HoursRemaining { get; set; }
        /// <summary>
        /// Gets or sets the original estimate for all tasks.
        /// </summary>
        /// <value>
        /// The original estimate for all tasks.
        /// </value>
        float OriginalEstimateForAllTasks { get; set; }
        /// <summary>
        /// Gets or sets the percentage complete.
        /// </summary>
        /// <value>
        /// The percentage complete.
        /// </value>
        double PercentageComplete { get; set; }
        string ReleaseNotes { get; set; }
        bool IsActive { get; set; }
        long ParentReleaseId { get; set; }
        ReleaseType ReleaseType { get; set; }
        ReleaseStatusType ReleaseStatusType { get; set; }
        
        ICollection<ReleaseProject> ReleaseProjects { get; set; }
        ICollection<ReleaseWorkLog> ReleaseWorkLogs { get; set; }
    }
}