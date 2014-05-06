// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISubscriber.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the ISubscriber type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManager.Core.Domain;

    /// <summary>
    /// The Subscriber interface.
    /// </summary>
    public interface ISubscriber : IDomain
    {
        /// <summary>
        /// Gets or sets the subscriber id.
        /// </summary>
        long SubscriberId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is calendar subscriber.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is calendar subscriber; otherwise, <c>false</c>.
        /// </value>
        bool IsCalendarSubscriber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is SMS subscriber.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is SMS subscriber; otherwise, <c>false</c>.
        /// </value>
        bool IsSmsSubscriber { get; set; }

        /// <summary>
        /// Gets or sets the SMS notification period.
        /// </summary>
        /// <value>
        /// The SMS notification period.
        /// </value>
        int SmsNotificationPeriod { get; set; }

        /// <summary>
        /// Gets or sets the subscriber notifiers.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        ICollection<SubscriberNotifier> SubscriberNotifiers { get; set; }
    }
}