using System.Linq;
using IQM.Common.Services;
using IQM.Common.Interfaces;
using IQM.Common.Web.Interfaces;
using PagedList;
using ProjectName.Core.Domain;
using ProjectName.Core.ViewModels;
using ProjectName.Core.ViewModels.SearchViewModels;

namespace ProjectName.Core.Services
{
    /// <summary>
    /// ClientService
    /// </summary>
    public class ClientService : GenericService<Client, ClientViewModel, IGenericRepository<Client>>, IGenericService<ClientPagedSearchViewModel, ClientViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ClientService(IGenericRepository<Client> repository)
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
        public ClientPagedSearchViewModel Search(ClientPagedSearchViewModel pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.Linq;

            /*
             * Implement the criteria here.
             */
            if (!string.IsNullOrEmpty(pagedSearchViewModel.CompanyName))
            {
                query = query.Where(client => client.CompanyName.Contains(pagedSearchViewModel.CompanyName));
            }

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<Client>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="address">The address.</param>
        public bool AddAddress(long id, Address address)
        {
            var result = false;
            var client = GetById(id).Data;
            if (client != null)
            {
                client.Addresses.Add(address);
                Save(client);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns>Returns the view model</returns>
        public override ClientViewModel GetViewModel()
        {
            var vm = base.GetViewModel();
            vm.Data.Addresses.Add(new Address());
            return vm;
        }
    }
}
