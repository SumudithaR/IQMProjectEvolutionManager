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
    public class UserNotifierProviderService : DomainBaseService<UserNotifierProvider, UserNotifierProviderDomainWrapper, UserNotifierProviderPagedSearchDomainWrapper>, IUserNotifierProviderService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserNotifierProviderService(IGenericRepository<UserNotifierProvider> repository)
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
        public override UserNotifierProviderPagedSearchDomainWrapper Search(UserNotifierProviderPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<UserNotifierProvider>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool InDatabase(UserNotifierProvider userNotifierProvider)
        {
            if (userNotifierProvider != null)
            {
                return GetAll().Any(uNPro => uNPro.UserNotifierProviderId == uNPro.UserNotifierProviderId);
            }
            return false;
        }

        public void InsertOrUpdate(UserNotifierProvider userNotifierProvider)
        {
            if (userNotifierProvider != null)
            {
                if (InDatabase(userNotifierProvider))
                {
                    var currentUserNotifierProvider = GetAll().SingleOrDefault(uNPro => uNPro.UserNotifierProviderId == uNPro.UserNotifierProviderId);

                    currentUserNotifierProvider.Name = userNotifierProvider.Name;

                    Repository.Save(currentUserNotifierProvider);
                }
                else
                {
                    Repository.Save(userNotifierProvider);
                }
            }
        }
    }
}