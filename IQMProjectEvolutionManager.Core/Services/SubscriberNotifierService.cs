// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriberNotifierService.cs" company="IQm Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The subscriber notifier service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Services
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using IQM.Common.Interfaces;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.DomainWrappers;
    using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
    using IQMProjectEvolutionManager.Core.Interfaces.Services;
    using IQMProjectEvolutionManager.Core.Services.BaseServices;

    using PagedList;

    /// <summary>
    /// The subscriber notifier service.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class SubscriberNotifierService : DomainBaseService<SubscriberNotifier, SubscriberNotifierDomainWrapper, SubscriberNotifierPagedSearchDomainWrapper>, ISubscriberNotifierService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriberNotifierService"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public SubscriberNotifierService(IGenericRepository<SubscriberNotifier> repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Searches the specified search view model.
        /// </summary>
        /// <param name="pagedSearchViewModel">The search view model.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The Client Search View Model</returns>
        public override SubscriberNotifierPagedSearchDomainWrapper Search(SubscriberNotifierPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<SubscriberNotifier>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        /// <summary>
        /// The in database.
        /// </summary>
        /// <param name="userNotifier">
        /// The user notifier.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool InDatabase(SubscriberNotifier userNotifier)
        {
            return userNotifier != null && this.GetAll().Any(uNoti => uNoti.SubscriberNotifierId == userNotifier.SubscriberNotifierId);
        }

        /// <summary>
        /// The insert or update.
        /// </summary>
        /// <param name="userNotifier">
        /// The user notifier.
        /// </param>
        public void InsertOrUpdate(SubscriberNotifier userNotifier)
        {
            if (userNotifier == null)
            {
                return;
            }

            if (this.InDatabase(userNotifier))
            {
                var currentUserNotifier = this.GetAll().SingleOrDefault(uNoti => uNoti.SubscriberNotifierId == userNotifier.SubscriberNotifierId);

                if (currentUserNotifier == null)
                {
                    return;
                }

                currentUserNotifier.AccessId = userNotifier.AccessId;

                this.Repository.Save(currentUserNotifier);
            }
            else
            {
                this.Repository.Save(userNotifier);
            }
        }

        /// <summary>
        /// The get notifier.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="purpose">
        /// The purpose.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="SubscriberNotifier"/>.
        /// </returns>
        public SubscriberNotifier GetNotifier(Subscriber user, SubscriberNotifierPurpose purpose, SubscriberNotifierType type)
        {
            if (user != null && purpose != null && type != null)
            {
                return GetAll().SingleOrDefault(uNotif => uNotif.Subscriber.SubscriberId == user.SubscriberId && uNotif.SubscriberNotifierPurpose.SubscriberNotifierPurposeId == purpose.SubscriberNotifierPurposeId
                    && uNotif.SubscriberNotifierType.SubscriberNotifierTypeId == type.SubscriberNotifierTypeId);
            }

            return null;
        }

        /// <summary>
        /// The get notifiers.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<SubscriberNotifier> GetNotifiers(Subscriber user, SubscriberNotifierType type)
        {
            if (user != null && type != null)
            {
                return GetAll().Where(uNotif => uNotif.Subscriber.SubscriberId == user.SubscriberId && uNotif.SubscriberNotifierType.SubscriberNotifierTypeId == type.SubscriberNotifierTypeId).ToList();
            }

            return null;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="userNotifier">
        /// The user notifier.
        /// </param>
        public void Delete(SubscriberNotifier userNotifier)
        {
            if (userNotifier != null)
            {
                Repository.Remove(userNotifier);
            }
        }
    }
}