// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReleaseStatusType.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   The release status type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using System;
    using System.Collections.Generic;

    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// The release status type.
    /// </summary>
    public class ReleaseStatusType : IReleaseStatusType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseStatusType"/> class.
        /// </summary>
        public ReleaseStatusType()
        {
            this.Releases = new List<Release>();
        }

        /// <summary>
        /// Gets or sets the release status type id. 
        /// </summary>
        /// <value>
        /// The release status type id. 
        /// </value>
        public virtual long ReleaseStatusTypeId { get; set; }

        /// <summary>
        /// Gets or sets the on time id.
        /// </summary>
        public virtual int OnTimeId { get; set; }

        /// <summary>
        /// Gets or sets the release type id. 
        /// </summary>
        /// <value>
        /// The release type id. 
        /// </value>
        public virtual long ReleaseTypeId { get; set; }

        /// <summary>
        /// Gets or sets the release status type name. 
        /// </summary>
        /// <value>
        /// The release status type name. 
        /// </value>
        public virtual string Name { get; set; }

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
        /// Gets or sets the releases.
        /// </summary>
        public virtual ICollection<Release> Releases { get; set; }
    }
}