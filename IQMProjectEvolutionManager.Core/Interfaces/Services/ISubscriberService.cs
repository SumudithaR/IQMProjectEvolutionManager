// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISubscriberService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014. 
// </copyright>
// <summary>
//   The SubscriberService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Interfaces.Services
{
    using System.Collections.Generic;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.DomainWrappers;
    using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
    using IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices;

    /// <summary>
    /// The SubscriberService interface.
    /// </summary>
    public interface ISubscriberService : IDomainBaseService<Subscriber, SubscriberDomainWrapper, SubscriberPagedSearchDomainWrapper>
    {
        /// <summary>
        /// Ins the database.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns> boolean </returns>
        bool InDatabase(Subscriber user);

        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="user">The user.</param>
        void InsertOrUpdate(Subscriber user);

        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        /// <returns> List of Subscribers </returns>
        IList<Subscriber> GetSubscribers();

        /// <summary>
        /// Gets the calendar subscribers.
        /// </summary>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns> List of Subscribers </returns>
        IList<Subscriber> GetCalendarSubscribers(bool onlyActive);

        /// <summary>
        /// The get sm subscribers.
        /// </summary>
        /// <param name="onlyActive">
        /// The only active.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<Subscriber> GetSmsSubscribers(bool onlyActive);

        /// <summary>
        /// The get by user name.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Subscriber"/>.
        /// </returns>
        Subscriber GetByUserName(string userName);
    }
}