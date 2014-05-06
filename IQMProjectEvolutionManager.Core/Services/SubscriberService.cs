// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriberService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the SubscriberService type.
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
    /// The subscriber service.
    /// </summary>
    public class SubscriberService : DomainBaseService<Subscriber, SubscriberDomainWrapper, SubscriberPagedSearchDomainWrapper>, ISubscriberService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriberService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public SubscriberService(IGenericRepository<Subscriber> repository)
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
        public override SubscriberPagedSearchDomainWrapper Search(SubscriberPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<Subscriber>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        /// <summary>
        /// Ins the database.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns> boolean </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        public bool InDatabase(Subscriber user)
        {
            return user != null && this.GetAll().Any(usr => usr.SubscriberId == user.SubscriberId);
        }

        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="user">The user.</param>
        public void InsertOrUpdate(Subscriber user)
        {
            if (user == null)
            {
                return;
            }

            if (this.InDatabase(user))
            {
                var currentUser = this.GetAll().SingleOrDefault(usr => usr.SubscriberId == user.SubscriberId);

                if (currentUser == null)
                {
                    return;
                }

                currentUser.IsCalendarSubscriber = user.IsCalendarSubscriber;
                currentUser.IsSmsSubscriber = user.IsSmsSubscriber;

                this.Repository.Save(currentUser);
            }
            else
            {
                this.Repository.Save(user);
            }
        }

        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        /// <returns> List of Subscribers </returns>
        public IList<Subscriber> GetSubscribers()
        {
            return this.GetAll();
        }

        /// <summary>
        /// Gets the calendar subscribers.
        /// </summary>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns> List of Subscribers </returns>
        public IList<Subscriber> GetCalendarSubscribers(bool onlyActive)
        {
            return this.GetSubscribers().Where(usr => usr.IsCalendarSubscriber).ToList();
        }

        /// <summary>
        /// The get sm subscribers.
        /// </summary>
        /// <param name="onlyActive">
        /// The only active.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public IList<Subscriber> GetSmsSubscribers(bool onlyActive)
        {
            return this.GetSubscribers().Where(usr => usr.IsSmsSubscriber).ToList();
        }

        /// <summary>
        /// The get by user name.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Subscriber"/>.
        /// </returns>
        public Subscriber GetByUserName(string userName)
        {
            return userName != null && !userName.Equals(string.Empty)
                       ? this.GetSubscribers().SingleOrDefault(subs => subs.UserName.Equals(userName))
                       : null;
        }
    }
}