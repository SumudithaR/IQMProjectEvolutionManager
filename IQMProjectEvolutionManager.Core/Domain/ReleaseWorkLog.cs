// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReleaseWorkLog.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Defines the ReleaseWorkLog type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using System;

    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// The release work log.
    /// </summary>
    public class ReleaseWorkLog : IReleaseWorkLog
    {
        /// <summary>
        /// Gets or sets the release work log id.
        /// </summary>
        public virtual long ReleaseWorkLogId { get; set; }

        /// <summary>
        /// Gets or sets the release.
        /// </summary>
        public virtual Release Release { get; set; }

        /// <summary>
        /// Gets or sets the staff.
        /// </summary>
        public virtual Staff Staff { get; set; }

        /// <summary>
        /// Gets or sets the hours worked on release.
        /// </summary>
        /// <value>
        /// The hours worked on release.
        /// </value>
        public virtual float HoursWorkedOnRelease { get; set; }

        /// <summary>
        /// Gets or sets the hours remaining on release.
        /// </summary>
        /// <value>
        /// The hours remaining on release.
        /// </value>
        public virtual float HoursRemainingOnRelease { get; set; }

        /// <summary>
        /// Gets or sets the hours remaining on release in last week.
        /// </summary>
        /// <value>
        /// The hours remaining on release in last week.
        /// </value>
        public virtual float HoursWorkedOnReleaseInLastWeek { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        public virtual DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the edited.
        /// </summary>
        public virtual DateTime? Edited { get; set; }

        /// <summary>
        /// Gets or sets the edited by.
        /// </summary>
        public virtual string EditedBy { get; set; }

        /// <summary>
        /// Gets or sets the delete by.
        /// </summary>
        public virtual string DeleteBy { get; set; }

        /// <summary>
        /// Gets or sets the delete on.
        /// </summary>
        public virtual DateTime? DeleteOn { get; set; }

        /// <summary>
        /// Gets or sets the delete reason.
        /// </summary>
        public virtual string DeleteReason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether deleted.
        /// </summary>
        public virtual bool Deleted { get; set; }
    }
}