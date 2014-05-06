using IQM.Common.Interfaces;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManager.Core.Services.BaseServices;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Services
{
    public class SubscriberNotifierLogService : DomainBaseService<SubscriberNotifierLog, SubscriberNotifierLogDomainWrapper, SubscriberNotifierLogPagedSearchDomainWrapper>, ISubscriberNotifierLogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriberNotifierLogService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public SubscriberNotifierLogService(IGenericRepository<SubscriberNotifierLog> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Searches the specified search view model.
        /// </summary>
        /// <param name="pagedSearchViewModel">The search view model.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The Client Search View Model</returns>
        public override SubscriberNotifierLogPagedSearchDomainWrapper Search(SubscriberNotifierLogPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<SubscriberNotifierLog>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool InDatabase(SubscriberNotifierLog userNotifierLog)
        {
            if (userNotifierLog != null)
            {
                return GetAll().Any(uNLog => uNLog.SubscriberNotifierLogId == userNotifierLog.SubscriberNotifierLogId);
            }
            return false;
        }

        public void InsertOrUpdate(SubscriberNotifierLog userNotifierLog)
        {
            if (userNotifierLog != null)
            {
                if (InDatabase(userNotifierLog))
                {
                    var currentUserNotifierLog = GetAll().SingleOrDefault(uNLog => uNLog.SubscriberNotifierLogId == userNotifierLog.SubscriberNotifierLogId);

                    currentUserNotifierLog.EndDate = userNotifierLog.EndDate;
                    currentUserNotifierLog.Location = userNotifierLog.Location;
                    currentUserNotifierLog.Message = userNotifierLog.Message;
                    currentUserNotifierLog.SentSuccess = userNotifierLog.SentSuccess;
                    currentUserNotifierLog.StartDate = userNotifierLog.StartDate;
                    currentUserNotifierLog.Subject = userNotifierLog.Subject;
                    currentUserNotifierLog.RegisteredForId = userNotifierLog.RegisteredForId;
                    currentUserNotifierLog.TransactionId = userNotifierLog.TransactionId;

                    Repository.Save(currentUserNotifierLog);
                }
                else
                {
                    Repository.Save(userNotifierLog);
                }
            }
        }

        public void Delete(SubscriberNotifierLog userNotifierLog)
        {
            if (userNotifierLog != null)
            {
                Repository.Remove(userNotifierLog);
            }
        }

        public SubscriberNotifierLog GetByRegisteredForId(SubscriberNotifier userNotifier, long registeredForId)
        {
            return GetAll().SingleOrDefault(uNLog => uNLog.RegisteredForId == registeredForId && uNLog.SubscriberNotifier.SubscriberNotifierId == userNotifier.SubscriberNotifierId);
        }

        public int LogsCount()
        {
            return GetAll().Count;
        }
    }
}