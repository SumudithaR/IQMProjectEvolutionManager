// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReleaseProject.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Defines the ReleaseProject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using System;

    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// The release project.
    /// </summary>
    public class ReleaseProject : IReleaseProject
    {
        /// <summary>
        /// Gets or sets the release project id.
        /// </summary>
        public virtual long ReleaseProjectId { get; set; }

        /// <summary>
        /// Gets or sets the release.
        /// </summary>
        public virtual Release Release { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        public virtual Project Project { get; set; }

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
