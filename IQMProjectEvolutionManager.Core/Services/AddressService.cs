using System;
using System.Linq;
using IQM.Common.Interfaces;
using IQM.Common.Services;
using IQM.Common.Web.Interfaces;
using PagedList;
using ProjectName.Core.Domain;
using ProjectName.Core.ViewModels;
using ProjectName.Core.ViewModels.SearchViewModels;

namespace ProjectName.Core.Services
{
    /// <summary>
    /// The address service
    /// </summary>
    public class AddressService : GenericService<Address, AddressViewModel, IGenericRepository<Address>>, IGenericService<AddressPagedSearchViewModel, AddressViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownItemService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AddressService(IGenericRepository<Address> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Searches the specified paged search view model.
        /// </summary>
        /// <param name="pagedSearchViewModel">The paged search view model.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public AddressPagedSearchViewModel Search(AddressPagedSearchViewModel pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.Linq;

            // Implement the default filter (used in the search term from the ajax table select)
            if (!String.IsNullOrEmpty(pagedSearchViewModel.DefaultFilter))
            {
                query = query.Where(a => a.AddressName.Contains(pagedSearchViewModel.DefaultFilter));
            }

            var addresses = query.ToList();

            pagedSearchViewModel.Data = new PagedList<Address>(addresses.ToList(), pageNumber, pageSize);

            return pagedSearchViewModel;
        }
    }
}