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
    public class SubscriberNotifierPurposeService : DomainBaseService<SubscriberNotifierPurpose, SubscriberNotifierPurposeDomainWrapper, SubscriberNotifierPurposePagedSearchDomainWrapper>, ISubscriberNotifierPurposeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public SubscriberNotifierPurposeService(IGenericRepository<SubscriberNotifierPurpose> repository)
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
        public override SubscriberNotifierPurposePagedSearchDomainWrapper Search(SubscriberNotifierPurposePagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<SubscriberNotifierPurpose>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool InDatabase(SubscriberNotifierPurpose userNotifierPurpose)
        {
            if (userNotifierPurpose != null)
            {
                return GetAll().Any(uNPurp => uNPurp.SubscriberNotifierPurposeId == userNotifierPurpose.SubscriberNotifierPurposeId);
            }
            return false;
        }

        public void InsertOrUpdate(SubscriberNotifierPurpose userNotifierPurpose)
        {
            if (userNotifierPurpose != null)
            {
                if (InDatabase(userNotifierPurpose))
                {
                    var currentUserNotifierPurpose = GetAll().SingleOrDefault(uNPurp => uNPurp.SubscriberNotifierPurposeId == userNotifierPurpose.SubscriberNotifierPurposeId);

                    currentUserNotifierPurpose.Name = userNotifierPurpose.Name;
                    currentUserNotifierPurpose.Edited = DateTime.Now;

                    Repository.Save(currentUserNotifierPurpose);
                }
                else
                {
                    Repository.Save(userNotifierPurpose);
                }
            }
        }

        public SubscriberNotifierPurpose GetByName(string name)
        {
            if (!name.Equals(string.Empty))
            {
                name = name.Trim();
                return GetAll().SingleOrDefault(uNPurp => uNPurp.Name == name);
            }
            return null;
        }
    }
}