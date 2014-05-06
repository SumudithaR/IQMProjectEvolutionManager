using IQM.Common.Web.Interfaces;
using IQM.Common.Web.ViewModels;
using ProjectName.Core.Domain;
using ProjectName.Core.ViewModels.SearchViewModels;

namespace ProjectName.Core.ViewModels
{
    /// <summary>
    /// The address view model.
    /// </summary>
    public class AddressViewModel : BaseViewModel<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressViewModel"/> class.
        /// </summary>
        public AddressViewModel() : base(new ApplicationSettings())
        {
        }

        /// <summary>
        /// Gets the name of the domain object.
        /// </summary>
        /// <returns>The friendly name of the object</returns>
        public override string GetDomainObjectName()
        {
            return "Address";
        }

        /// <summary>
        /// Gets the data id.
        /// </summary>
        /// <returns></returns>
        public override long? GetDataId()
        {
            return Data.AddressId;
        }
    }
}