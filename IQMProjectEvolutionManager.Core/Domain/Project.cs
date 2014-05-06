// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Project in IQMProjectEvolutionManager DB
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using System;
    using System.Collections.Generic;

    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// Project in IQMProjectEvolutionManager DB
    /// </summary>
    public class Project : IProject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            this.ReleaseProjects = new List<ReleaseProject>();
        }

        /// <summary>
        /// Gets or sets the project id. 
        /// </summary>
        /// <value>
        /// The project id. 
        /// </value>
        public virtual long ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the on time id.
        /// </summary>
        public virtual int OnTimeId { get; set; }

        /// <summary>
        /// Gets or sets the project name. 
        /// </summary>
        /// <value>
        /// The project name. 
        /// </value>
        public virtual string Name { get; set; }

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
        /// Gets or sets the release projects.
        /// </summary>
        public virtual ICollection<ReleaseProject> ReleaseProjects { get; set; }
    }
}