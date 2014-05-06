// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Staff.cs" company="IQm Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Defines the Staff type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using System;
    using System.Collections.Generic;

    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// The staff.
    /// </summary>
    public class Staff : IStaff
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Staff"/> class.
        /// </summary>
        public Staff()
        {
            this.ReleaseWorkLogs = new List<ReleaseWorkLog>();
        }

        /// <summary>
        /// Gets or sets the staff id.
        /// </summary>
        public virtual long StaffId { get; set; }

        /// <summary>
        /// Gets or sets the on time id.
        /// </summary>
        public virtual int OnTimeId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public virtual bool IsActive { get; set; }

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
        /// Gets or sets the release work logs.
        /// </summary>
        public virtual ICollection<ReleaseWorkLog> ReleaseWorkLogs { get; set; }
    }
}