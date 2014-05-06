// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriberNotifier.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Defines the SubscriberNotifier type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// The subscriber notifier.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class SubscriberNotifier : ISubscriberNotifier
    {
        /// <summary>
        /// Gets or sets the subscriber notifier id.
        /// </summary>
        public virtual long SubscriberNotifierId { get; set; }

        /// <summary>
        /// Gets or sets the subscriber.
        /// </summary>
        public virtual Subscriber Subscriber { get; set; }

        /// <summary>
        /// Gets or sets the access id.
        /// </summary>
        public virtual string AccessId { get; set; }

        /// <summary>
        /// Gets or sets the subscriber notifier type.
        /// </summary>
        public virtual SubscriberNotifierType SubscriberNotifierType { get; set; }

        /// <summary>
        /// Gets or sets the subscriber notifier purpose.
        /// </summary>
        public virtual SubscriberNotifierPurpose SubscriberNotifierPurpose { get; set; }

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
        /// Gets or sets the subscriber notifier logs.
        /// </summary>
        public virtual ICollection<SubscriberNotifierLog> SubscriberNotifierLogs { get; set; }
    }
}