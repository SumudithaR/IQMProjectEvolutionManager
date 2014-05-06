using IQM.Common.Web.ViewModels;
using ProjectName.Core.Domain;
using ProjectName.Core.ViewModels.SearchViewModels;

namespace ProjectName.Core.ViewModels
{
    /// <summary>
    /// Client View Model
    /// </summary>
    public class ClientViewModel : BaseViewModel<Client>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientViewModel"/> class.
        /// </summary>
        public ClientViewModel() : base(new ApplicationSettings())
        {
        }

        /// <summary>
        /// Gets the name of the domain object.
        /// </summary>
        /// <returns>The friendly name of the data object.</returns>
        public override string GetDomainObjectName()
        {
            return "Client";
        }

        /// <summary>
        /// Gets the data id.
        /// </summary>
        /// <returns>the data objects id.</returns>
        public override long? GetDataId()
        {
            return Data.ClientId;
        }
    }
}
