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
    public class SubscriberNotifierTypeService : DomainBaseService<SubscriberNotifierType, SubscriberNotifierTypeDomainWrapper, SubscriberNotifierTypePagedSearchDomainWrapper>, ISubscriberNotifierTypeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public SubscriberNotifierTypeService(IGenericRepository<SubscriberNotifierType> repository)
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
        public override SubscriberNotifierTypePagedSearchDomainWrapper Search(SubscriberNotifierTypePagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<SubscriberNotifierType>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool InDatabase(SubscriberNotifierType userNotifierType)
        {
            if (userNotifierType != null)
            {
                return GetAll().Any(uNType => uNType.SubscriberNotifierTypeId == userNotifierType.SubscriberNotifierTypeId);
            }
            return false;
        }

        public void InsertOrUpdate(SubscriberNotifierType userNotifierType)
        {
            if (userNotifierType != null)
            {
                if (InDatabase(userNotifierType))
                {
                    var currentUserNotifierType = GetAll().SingleOrDefault(uNType => uNType.SubscriberNotifierTypeId == userNotifierType.SubscriberNotifierTypeId);

                    currentUserNotifierType.Name = userNotifierType.Name;
                    currentUserNotifierType.Edited = DateTime.Now;

                    Repository.Save(currentUserNotifierType);
                }
                else
                {
                    Repository.Save(userNotifierType);
                }
            }
        }

        public SubscriberNotifierType GetByName(string name)
        {
            if (!name.Equals(string.Empty))
            {
                name = name.Trim();

                return GetAll().SingleOrDefault(uNType => uNType.Name == name);
            }

            return null;
        }
    }
}