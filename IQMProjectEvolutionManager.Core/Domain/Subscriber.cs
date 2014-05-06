// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Subscriber.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   The subscriber.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// The subscriber.
    /// </summary>
    public class Subscriber : ISubscriber
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriber"/> class.
        /// </summary>
        public Subscriber()
        {
            this.IsCalendarSubscriber = false;
            this.IsSmsSubscriber = false;

            this.SmsNotificationPeriod = 5;
        }

        /// <summary>
        /// Gets or sets the subscriber id.
        /// </summary>
        public virtual long SubscriberId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>
        /// The mobile.
        /// </value>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is calendar subscriber.
        /// </summary>
        public virtual bool IsCalendarSubscriber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is sms subscriber.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public virtual bool IsSmsSubscriber { get; set; }

        /// <summary>
        /// Gets or sets the sms notification period.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public virtual int SmsNotificationPeriod { get; set; }

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
        /// Gets or sets the subscriber notifiers.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public virtual ICollection<SubscriberNotifier> SubscriberNotifiers { get; set; }
    }
}