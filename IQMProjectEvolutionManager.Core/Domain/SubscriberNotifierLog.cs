// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriberNotifierLog.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Defines the SubscriberNotifierLog type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// The subscriber notifier log.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class SubscriberNotifierLog : ISubscriberNotifierLog
    {
        /// <summary>
        /// Gets or sets the subscriber notifier log id.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public virtual long SubscriberNotifierLogId { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public virtual string Subject { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public virtual string Location { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sent success.
        /// </summary>
        public virtual bool SentSuccess { get; set; }

        /// <summary>
        /// Gets or sets the transaction id.
        /// </summary>
        public virtual string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the registered for id.
        /// </summary>
        public virtual long RegisteredForId { get; set; }

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
        /// Gets or sets the subscriber notifier.
        /// </summary>
        public virtual SubscriberNotifier SubscriberNotifier { get; set; }
    }
}