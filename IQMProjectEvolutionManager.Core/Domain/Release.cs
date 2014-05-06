// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Release.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Defines the Release type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using System;
    using System.Collections.Generic;

    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// The release.
    /// </summary>
    public class Release : IRelease
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Release"/> class.
        /// </summary>
        public Release()
        {
            this.ReleaseProjects = new List<ReleaseProject>();
            this.ReleaseWorkLogs = new List<ReleaseWorkLog>();
        }

        /// <summary>
        /// Gets or sets the release id.
        /// </summary>
        public virtual long ReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the on time id.
        /// </summary>
        public virtual int OnTimeId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        public virtual DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the hours worked.
        /// </summary>
        public virtual float HoursWorked { get; set; }

        /// <summary>
        /// Gets or sets the hours remaining.
        /// </summary>
        public virtual float HoursRemaining { get; set; }

        /// <summary>
        /// Gets or sets the original estimate for all tasks.
        /// </summary>
        /// <value>
        /// The original estimate for all tasks.
        /// </value>
        public virtual float OriginalEstimateForAllTasks { get; set; }

        /// <summary>
        /// Gets or sets the percentage complete.
        /// </summary>
        /// <value>
        /// The percentage complete.
        /// </value>
        public virtual double PercentageComplete { get; set; }

        /// <summary>
        /// Gets or sets the release notes.
        /// </summary>
        public virtual string ReleaseNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the parent release id.
        /// </summary>
        public virtual long ParentReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the release type.
        /// </summary>
        public virtual ReleaseType ReleaseType { get; set; }

        /// <summary>
        /// Gets or sets the release status type.
        /// </summary>
        public virtual ReleaseStatusType ReleaseStatusType { get; set; }

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

        /// <summary>
        /// Gets or sets the release projects.
        /// </summary>
        public virtual ICollection<ReleaseProject> ReleaseProjects { get; set; }

        /// <summary>
        /// Gets or sets the release work logs.
        /// </summary>
        public virtual ICollection<ReleaseWorkLog> ReleaseWorkLogs { get; set; }
    }
}